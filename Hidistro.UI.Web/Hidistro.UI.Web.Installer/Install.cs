using Hidistro.Core;
using Hidistro.Core.Configuration;
using Hidistro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
namespace Hidistro.UI.Web.Installer
{
	public class Install : System.Web.UI.Page
	{
		private string action;
		protected System.Web.UI.WebControls.Button btnInstall;
		protected System.Web.UI.WebControls.CheckBox chkIsAddDemo;
		private string dbName;
		private string dbPassword;
		private string dbServer;
		private string dbUsername;
		private string email;
		private System.Collections.Generic.IList<string> errorMsgs;
		protected System.Web.UI.HtmlControls.HtmlForm form1;
		private bool isAddDemo;
		protected System.Web.UI.WebControls.Label lblErrMessage;
		protected System.Web.UI.WebControls.Label litSetpErrorMessage;
		private string password;
		private string password2;
		//private string siteDescription;
		//private string siteName;
		private bool testSuccessed;
		protected System.Web.UI.WebControls.TextBox txtDbName;
		protected System.Web.UI.WebControls.TextBox txtDbPassword;
		protected System.Web.UI.WebControls.TextBox txtDbServer;
		protected System.Web.UI.WebControls.TextBox txtDbUsername;
		protected System.Web.UI.WebControls.TextBox txtEmail;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.TextBox txtPassword2;
		protected System.Web.UI.WebControls.TextBox txtUsername;
		private string username;
		private bool AddDemoData(out string errorMsg)
		{
			string path = base.Request.MapPath("SqlScripts/SiteDemo.zh-CN.sql");
			bool result;
			if (!System.IO.File.Exists(path))
			{
				errorMsg = "没有找到演示数据文件-SiteDemo.Sql";
				result = false;
			}
			else
			{
				result = this.ExecuteScriptFile(path, out errorMsg);
			}
			return result;
		}
		private bool AddInitData(out string errorMsg)
		{
			string path = base.Request.MapPath("SqlScripts/SiteInitData.zh-CN.Sql");
			bool result;
			if (!System.IO.File.Exists(path))
			{
				errorMsg = "没有找到初始化数据文件-SiteInitData.Sql";
				result = false;
			}
			else
			{
				result = this.ExecuteScriptFile(path, out errorMsg);
			}
			return result;
		}
		private void btnInstall_Click(object sender, System.EventArgs e)
		{
			string msg = string.Empty;
			if (!this.ValidateUser(out msg))
			{
				this.ShowMsg(msg, false);
			}
			else
			{
				if (!this.testSuccessed && !this.ExecuteTest())
				{
					this.ShowMsg("数据库链接信息有误", false);
				}
				else
				{
					if (!this.CreateDataSchema(out msg))
					{
						this.ShowMsg(msg, false);
					}
					else
					{
						if (!this.CreateAdministrator(out msg))
						{
							this.ShowMsg(msg, false);
						}
						else
						{
							if (!this.AddInitData(out msg))
							{
								this.ShowMsg(msg, false);
							}
							else
							{
								if (!this.isAddDemo || this.AddDemoData(out msg))
								{
									if (!this.SaveSiteSettings(out msg))
									{
										this.ShowMsg(msg, false);
									}
									else
									{
										if (!this.SaveConfig(out msg))
										{
											this.ShowMsg(msg, false);
										}
										else
										{
											this.Context.Response.Redirect("Succeed.aspx", true);
										}
									}
								}
							}
						}
					}
				}
			}
		}
		private bool CreateAdministrator(out string errorMsg)
		{
			System.Data.Common.DbConnection connection = new System.Data.SqlClient.SqlConnection(this.GetConnectionString());
			connection.Open();
			System.Data.Common.DbCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandType = System.Data.CommandType.Text;
			command.CommandText = "INSERT INTO aspnet_Roles(RoleName) VALUES('超级管理员'); SELECT @@IDENTITY";
			int num = System.Convert.ToInt32(command.ExecuteScalar());
			command.CommandText = "INSERT INTO aspnet_Managers(RoleId, UserName, Password, Email, CreateDate) VALUES (@RoleId, @UserName, @Password, @Email, getdate())";
			command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RoleId", num));
			command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Username", this.username));
			command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Password", HiCryptographer.Md5Encrypt(this.password)));
			command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", this.email));
			command.ExecuteNonQuery();
			connection.Close();
			errorMsg = null;
			return true;
		}
		private bool CreateDataSchema(out string errorMsg)
		{
			string path = base.Request.MapPath("SqlScripts/Schema.sql");
			bool result;
			if (!System.IO.File.Exists(path))
			{
				errorMsg = "没有找到数据库架构文件-Schema.sql";
				result = false;
			}
			else
			{
				result = this.ExecuteScriptFile(path, out errorMsg);
			}
			return result;
		}
		private static string CreateKey(int len)
		{
			byte[] data = new byte[len];
			new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(data);
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				builder.Append(string.Format("{0:X2}", data[i]));
			}
			return builder.ToString();
		}
		private bool ExecuteScriptFile(string pathToScriptFile, out string errorMsg)
		{
			System.IO.StreamReader reader = null;
			System.Data.SqlClient.SqlConnection connection = null;
			bool result;
			try
			{
				string applicationPath = Globals.ApplicationPath;
				System.IO.StreamReader streamReader;
				reader = (streamReader = new System.IO.StreamReader(pathToScriptFile));
				try
				{
					System.Data.SqlClient.SqlConnection sqlConnection;
					connection = (sqlConnection = new System.Data.SqlClient.SqlConnection(this.GetConnectionString()));
					try
					{
						System.Data.SqlClient.SqlCommand command2 = new System.Data.SqlClient.SqlCommand
						{
							Connection = connection,
							CommandType = System.Data.CommandType.Text,
							CommandTimeout = 60
						};
						System.Data.Common.DbCommand command3 = command2;
						connection.Open();
						while (!reader.EndOfStream)
						{
							string str = Install.NextSqlFromStream(reader);
							if (!string.IsNullOrEmpty(str))
							{
								command3.CommandText = str.Replace("$VirsualPath$", applicationPath);
								command3.ExecuteNonQuery();
							}
						}
						connection.Close();
					}
					finally
					{
						if (sqlConnection != null)
						{
							((System.IDisposable)sqlConnection).Dispose();
						}
					}
					reader.Close();
				}
				finally
				{
					if (streamReader != null)
					{
						((System.IDisposable)streamReader).Dispose();
					}
				}
				errorMsg = null;
				result = true;
			}
			catch (System.Data.SqlClient.SqlException exception)
			{
				errorMsg = exception.Message;
				if (connection != null && connection.State != System.Data.ConnectionState.Closed)
				{
					connection.Close();
					connection.Dispose();
				}
				if (reader != null)
				{
					reader.Close();
					reader.Dispose();
				}
				result = false;
			}
			return result;
		}
		private bool ExecuteTest()
		{
			this.errorMsgs = new System.Collections.Generic.List<string>();
			System.Data.Common.DbTransaction transaction = null;
			System.Data.Common.DbConnection connection = null;
			string str;
			try
			{
				if (this.ValidateConnectionStrings(out str))
				{
					System.Data.Common.DbConnection dbConnection;
					connection = (dbConnection = new System.Data.SqlClient.SqlConnection(this.GetConnectionString()));
					try
					{
						connection.Open();
						System.Data.Common.DbCommand command = connection.CreateCommand();
						transaction = connection.BeginTransaction();
						command.Connection = connection;
                        this.ShowMsg(connection.ToString(), false);
						command.Transaction = transaction;
						command.CommandText = "CREATE TABLE installTest(Test bit NULL)";
						command.ExecuteNonQuery();
						command.CommandText = "DROP TABLE installTest";
						command.ExecuteNonQuery();
						transaction.Commit();
						connection.Close();
						goto IL_129;
					}
					finally
					{
						if (dbConnection != null)
						{
							((System.IDisposable)dbConnection).Dispose();
						}
					}
				}
				this.errorMsgs.Add(str);
			}
			catch (System.Exception exception)
			{
				this.errorMsgs.Add(exception.Message);
				if (transaction != null)
				{
					try
					{
						transaction.Rollback();
					}
					catch (System.Exception exception2)
					{
						this.errorMsgs.Add(exception2.Message);
					}
				}
				if (connection != null && connection.State != System.Data.ConnectionState.Closed)
				{
					connection.Close();
					connection.Dispose();
				}
			}
			IL_129:
			if (!Install.TestFolder(base.Request.MapPath(Globals.ApplicationPath + "/config/test.txt"), out str))
			{
				this.errorMsgs.Add(str);
			}
			try
			{
				Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(base.Request.ApplicationPath);
				if (configuration.ConnectionStrings.ConnectionStrings["KingWooSqlServer"].ConnectionString == "none")
				{
					configuration.ConnectionStrings.ConnectionStrings["KingWooSqlServer"].ConnectionString = "required";
				}
				else
				{
					configuration.ConnectionStrings.ConnectionStrings["KingWooSqlServer"].ConnectionString = "none";
				}
				configuration.Save();
			}
			catch (System.Exception exception3)
			{
				this.errorMsgs.Add(exception3.Message);
			}
			if (!Install.TestFolder(base.Request.MapPath(Globals.ApplicationPath + "/storage/test.txt"), out str))
			{
				this.errorMsgs.Add(str);
			}
			return this.errorMsgs.Count == 0;
		}
		private string GetConnectionString()
		{
            //return string.Format("server={0};uid={1};pwd={2};Trusted_Connection=no;database={3}", new object[]
            //{
            //    this.dbServer,
            //    this.dbUsername,
            //    this.dbPassword,
            //    this.dbName
            //});
            return string.Format("server={0};uid={1};pwd={2};database={3};", new object[]
			{
				this.dbServer,
				this.dbUsername,
				this.dbPassword,
				this.dbName
			});
		}
		private System.Security.Cryptography.RijndaelManaged GetCryptographer()
		{
			System.Security.Cryptography.RijndaelManaged managed = new System.Security.Cryptography.RijndaelManaged
			{
				KeySize = 128
			};
			managed.GenerateIV();
			managed.GenerateKey();
			return managed;
		}
		private void LoadParameters()
		{
			if (!string.IsNullOrEmpty(base.Request["isCallback"]) && base.Request["isCallback"] == "true")
			{
				this.action = base.Request["action"];
				this.dbServer = base.Request["DBServer"];
				this.dbName = base.Request["DBName"];
				this.dbUsername = base.Request["DBUsername"];
				this.dbPassword = base.Request["DBPassword"];
				this.username = base.Request["Username"];
				this.email = base.Request["Email"];
				this.password = base.Request["Password"];
				this.password2 = base.Request["Password2"];
				this.isAddDemo = (!string.IsNullOrEmpty(base.Request["IsAddDemo"]) && base.Request["IsAddDemo"] == "true");
				this.testSuccessed = (!string.IsNullOrEmpty(base.Request["TestSuccessed"]) && base.Request["TestSuccessed"] == "true");
			}
			else
			{
				this.dbServer = this.txtDbServer.Text;
				this.dbName = this.txtDbName.Text;
				this.dbUsername = this.txtDbUsername.Text;
				this.dbPassword = this.txtDbPassword.Text;
				this.username = this.txtUsername.Text;
				this.email = this.txtEmail.Text;
				this.password = this.txtPassword.Text;
				this.password2 = this.txtPassword2.Text;
				this.isAddDemo = this.chkIsAddDemo.Checked;
			}
		}
		private static string NextSqlFromStream(System.IO.StreamReader reader)
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			string strA = reader.ReadLine().Trim();
			while (!reader.EndOfStream && string.Compare(strA, "GO", true, System.Globalization.CultureInfo.InvariantCulture) != 0)
			{
				builder.Append(strA + System.Environment.NewLine);
				strA = reader.ReadLine();
			}
			if (string.Compare(strA, "GO", true, System.Globalization.CultureInfo.InvariantCulture) != 0)
			{
				builder.Append(strA + System.Environment.NewLine);
			}
			return builder.ToString();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.LoadParameters();
			this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
			if (!string.IsNullOrEmpty(base.Request["isCallback"]) && base.Request["isCallback"] == "true")
			{
				string str = "无效的操作类型：" + this.action;
				bool flag2 = false;
				if (this.action == "Test")
				{
					flag2 = this.ExecuteTest();
				}
				base.Response.Clear();
				base.Response.ContentType = "application/json";
				if (flag2)
				{
					base.Response.Write("{\"Status\":\"OK\"}");
				}
				else
				{
					string str2 = "";
					if (this.errorMsgs != null && this.errorMsgs.Count > 0)
					{
						foreach (string str3 in this.errorMsgs)
						{
							str2 = str2 + "{\"Text\":\"" + str3 + "\"},";
						}
						str2 = str2.Substring(0, str2.Length - 1);
						this.errorMsgs.Clear();
					}
					else
					{
						str2 = "{\"Text\":\"" + str + "\"}";
					}
					base.Response.Write(string.Format("{{\"Status\":\"Fail\",\"ErrorMsgs\":[{0}]}}", str2));
				}
				base.Response.End();
			}
			else
			{
				if (!this.Page.IsPostBack && base.Request.UrlReferrer != null)
				{
					base.Request.UrlReferrer.OriginalString.IndexOf("Activation.aspx");
				}
			}
		}
		private bool SaveConfig(out string errorMsg)
		{
			bool result;
			try
			{
				Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(base.Request.ApplicationPath);
				using (System.Security.Cryptography.RijndaelManaged managed = this.GetCryptographer())
				{
					configuration.AppSettings.Settings["IV"].Value = System.Convert.ToBase64String(managed.IV);
					configuration.AppSettings.Settings["Key"].Value = System.Convert.ToBase64String(managed.Key);
				}
				System.Web.Configuration.MachineKeySection section = (System.Web.Configuration.MachineKeySection)configuration.GetSection("system.web/machineKey");
				section.ValidationKey = Install.CreateKey(20);
				section.DecryptionKey = Install.CreateKey(24);
				section.Validation = System.Web.Configuration.MachineKeyValidation.SHA1;
				section.Decryption = "3DES";
				configuration.ConnectionStrings.ConnectionStrings["KingWooSqlServer"].ConnectionString = this.GetConnectionString();
				configuration.ConnectionStrings.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
				configuration.Save();
				errorMsg = null;
				result = true;
			}
			catch (System.Exception exception)
			{
				errorMsg = exception.Message;
				result = false;
			}
			return result;
		}
		private bool SaveSiteSettings(out string errorMsg)
		{
			errorMsg = null;
			bool result;
			try
			{
				string filename = base.Request.MapPath(Globals.ApplicationPath + "/config/SiteSettings.config");
				XmlDocument doc = new XmlDocument();
				SiteSettings settings = new SiteSettings(base.Request.Url.Host);
				doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + System.Environment.NewLine + "<Settings></Settings>");
				settings.CheckCode = Install.CreateKey(20);
				settings.ApplicationDescription = "<img src=\"/Storage/master/gallery/201501/20150114091939_5651.jpg\" title=\"20150114091939_5651\" alt=\"20150114091939_5651\" border=\"0\" />";
				settings.SaleService += "<div class=\"sale-service clearfix\">";
				settings.SaleService += "<a href=\"http://wpa.b.qq.com/cgi/wpa.php?ln=2&amp;uin=123456789\" class=\"col-sm-4\" target=\"_blank\">";
				settings.SaleService += "  <img src=\"/Storage/master/gallery/201501/20150112160440_7779.png\" title=\"QQ_service\" alt=\"QQ_service\" border=\"0\" /></a>   ";
				settings.SaleService += "<a href=\"http://wpa.b.qq.com/cgi/wpa.php?ln=2&amp;uin=450235251\" target=\"_blank\" class=\"col-sm-4\"><img src=\"/Storage/master/gallery/201501/20150112160517_9570.png\" title=\"QQ_service\" alt=\"QQ_service\" border=\"0\" /></a>   ";
				settings.SaleService += " <a href=\"\" http:=\"\" wpa.qq.com=\"\" msgrd?v=\"3&amp;uin=&amp;site=qq&amp;menu=yes\" \"=\"\" class=\"col-sm-4\"><img src=\"/Storage/master/gallery/201501/20150112160440_7779.png\" /></a> </div> ";
				settings.SaleService += "<a type=\"button\" class=\"btn btn-danger btn-block\" href=\"tel:18211111111\">服务电话:18211111111</a>";
				settings.DistributorBackgroundPic = "/Storage/data/DistributorBackgroundPic/default.jpg|";
				settings.MentionNowMoney = "1";
				settings.DistributorDescription += "<p>微信分销的功课：</p>";
				settings.DistributorDescription += "<p>1.研究兴趣取向：通过观察和研究他们发布的朋友圈，了解他们可能会被我们产品的那一方面打动，整合我们产品的特点，激发他们的兴趣。&nbsp;</p>";
				settings.DistributorDescription += "<p>2.推送信息:根据他们的兴趣特点来发布信息；字数不宜超过20字；要留悬念，让他们主动发问；图文并茂；</p>";
				settings.DistributorDescription += "<p>3.每天不要超过三条，更不要同时发送，选择的热度时间不同微信不一样，方法是观察朋友圈在哪段时间大家发微信比较勤奋，比他们的时间早5分钟左右发布。</p>";
				settings.DistributorDescription += "<p><span style=\"line-height:1.5;\">4.因为一般人的习惯是先看后发。最初,要学会烘托气氛，当我们开始做朋友圈营销的时候，一定不能做的像一个新手一样，发图片，然后写一些介绍。</span></p>";
				settings.DistributorDescription += "<p>微信分销的五要点&nbsp;</p>";
				settings.DistributorDescription += "<p>第一建立一个信任度；</p>";
				settings.DistributorDescription += "<p>第二呢第一时间让对方感兴趣；</p>";
				settings.DistributorDescription += "<p>第三点呢就是顾客的二次营销；&nbsp;</p>";
				settings.DistributorDescription += "<p>第四点呢互动；";
				settings.DistributorDescription += "第五个呢老顾客的忠诚和信息的推广；</p>";
				settings.OpenManyService = false;
				settings.WriteToXml(doc);
				doc.Save(filename);
				result = true;
			}
			catch (System.Exception exception)
			{
				errorMsg = exception.Message;
				result = false;
			}
			return result;
		}
		private void ShowMsg(string errorMsg, bool seccess)
		{
			this.lblErrMessage.Text = errorMsg;
		}
		private static bool TestFolder(string folderPath, out string errorMsg)
		{
			bool result;
			try
			{
				System.IO.File.WriteAllText(folderPath, "Hi");
				System.IO.File.AppendAllText(folderPath, ",This is a test file.");
				System.IO.File.Delete(folderPath);
				errorMsg = null;
				result = true;
			}
			catch (System.Exception exception)
			{
				errorMsg = exception.Message;
				result = false;
			}
			return result;
		}
		private bool ValidateConnectionStrings(out string msg)
		{
			msg = null;
			bool result;
			if (!string.IsNullOrEmpty(this.dbServer) && !string.IsNullOrEmpty(this.dbName) && !string.IsNullOrEmpty(this.dbUsername))
			{
				result = true;
			}
			else
			{
				msg = "数据库连接信息不完整";
				result = false;
			}
			return result;
		}
		private bool ValidateUser(out string msg)
		{
			msg = null;
			bool result;
			if (string.IsNullOrEmpty(this.username) || string.IsNullOrEmpty(this.email) || string.IsNullOrEmpty(this.password) || string.IsNullOrEmpty(this.password2))
			{
				msg = "管理员账号信息不完整";
				result = false;
			}
			else
			{
				HiConfiguration config = HiConfiguration.GetConfig();
				if (this.username.Length > config.UsernameMaxLength || this.username.Length < config.UsernameMinLength)
				{
					msg = string.Format("管理员用户名的长度只能在{0}和{1}个字符之间", config.UsernameMinLength, config.UsernameMaxLength);
					result = false;
				}
				else
				{
					if (string.Compare(this.username, "anonymous", true) == 0)
					{
						msg = "不能使用anonymous作为管理员用户名";
						result = false;
					}
					else
					{
						if (!Regex.IsMatch(this.username, config.UsernameRegex))
						{
							msg = "管理员用户名的格式不符合要求，用户名一般由字母、数字、下划线和汉字组成，且必须以汉字或字母开头";
							result = false;
						}
						else
						{
							if (this.email.Length > 256)
							{
								msg = "电子邮件的长度必须小于256个字符";
								result = false;
							}
							else
							{
								if (!Regex.IsMatch(this.email, config.EmailRegex))
								{
									msg = "电子邮件的格式错误";
									result = false;
								}
								else
								{
									if (this.password != this.password2)
									{
										msg = "管理员登录密码两次输入不一致";
										result = false;
									}
									else
									{
										if (this.password.Length >= System.Web.Security.Membership.Provider.MinRequiredPasswordLength && this.password.Length <= config.PasswordMaxLength)
										{
											result = true;
										}
										else
										{
											msg = string.Format("管理员登录密码的长度只能在{0}和{1}个字符之间", System.Web.Security.Membership.Provider.MinRequiredPasswordLength, config.PasswordMaxLength);
											result = false;
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}
	}
}
