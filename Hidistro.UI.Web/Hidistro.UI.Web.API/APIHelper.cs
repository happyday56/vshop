using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;
using System.Text;
namespace Hidistro.UI.Web.API
{
	public class APIHelper
	{
		public static string BuildSign(System.Collections.Generic.Dictionary<string, string> dicArray, string key, string sign_type, string _input_charset)
		{
			return APIHelper.Sign(APIHelper.CreateLinkstring(dicArray) + key, sign_type, _input_charset);
		}
		public static bool CheckSign(SortedDictionary<string, string> tmpParas, string keycode, string sign)
		{
			return APIHelper.BuildSign(APIHelper.Parameterfilter(tmpParas), keycode, "MD5", "utf-8") == sign;
		}
		public static string CreateLinkstring(System.Collections.Generic.Dictionary<string, string> dicArray)
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			foreach (System.Collections.Generic.KeyValuePair<string, string> pair in dicArray)
			{
				builder.Append(pair.Key + "=" + pair.Value + "&");
			}
			int length = builder.Length;
			builder.Remove(length - 1, 1);
			return builder.ToString();
		}
		public static System.Collections.Generic.Dictionary<string, string> Parameterfilter(SortedDictionary<string, string> dicArrayPre)
		{
			System.Collections.Generic.Dictionary<string, string> dictionary = new System.Collections.Generic.Dictionary<string, string>();
			foreach (System.Collections.Generic.KeyValuePair<string, string> pair in dicArrayPre)
			{
				if (pair.Key.ToLower() != "sign" && pair.Key.ToLower() != "sign_type" && pair.Value != "" && pair.Value != null)
				{
					string key = pair.Key.ToLower();
					dictionary.Add(key, pair.Value);
				}
			}
			return dictionary;
		}
		public static string PostData(string url, string postData)
		{
			string str = string.Empty;
			string result;
			try
			{
				Uri requestUri = new Uri(url);
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
				byte[] bytes = System.Text.Encoding.UTF8.GetBytes(postData);
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = (long)bytes.Length;
				using (System.IO.Stream stream = request.GetRequestStream())
				{
					stream.Write(bytes, 0, bytes.Length);
				}
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				{
					using (System.IO.Stream stream2 = response.GetResponseStream())
					{
						System.Text.Encoding encoding = System.Text.Encoding.UTF8;
						System.IO.Stream stream3 = stream2;
						if (response.ContentEncoding.ToLower() == "gzip")
						{
							stream3 = new GZipStream(stream2, CompressionMode.Decompress);
						}
						else
						{
							if (response.ContentEncoding.ToLower() == "deflate")
							{
								stream3 = new DeflateStream(stream2, CompressionMode.Decompress);
							}
						}
						using (System.IO.StreamReader reader = new System.IO.StreamReader(stream3, encoding))
						{
							result = reader.ReadToEnd();
							return result;
						}
					}
				}
			}
			catch (System.Exception)
			{
				str = string.Empty;
			}
			result = str;
			return result;
		}
		public static string Sign(string prestr, string sign_type, string _input_charset)
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder(32);
			if (sign_type.ToUpper() == "MD5")
			{
				byte[] buffer = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.Encoding.GetEncoding(_input_charset).GetBytes(prestr));
				for (int i = 0; i < buffer.Length; i++)
				{
					builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
				}
			}
			return builder.ToString();
		}
	}
}
