using System;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNET.WebControls
{
    /// <summary>
    /// 日历选择控件
    /// </summary>
    [ToolboxData("<{0}:WebCalendar runat=server></{0}:WebCalendar>")]
    public class WebCalendar : TextBox
    {

        #region 构造函数
        public WebCalendar()
        {
            m_CalendarType = ASPNET.WebControls.CalendarType.Default;
            m_CalendarLanguage = ASPNET.WebControls.CalendarLanguage.zh_CN;
            BeginYear = 1990;
            EndYear = 2050;
        }
        #endregion

        #region 重写虚函数
        protected override void Render(HtmlTextWriter writer)
        {
            base.Attributes.Add("readonly", "readonly");
            base.Attributes.Add("onclick", string.Format("new Calendar({0}, {1}, {2}).show(this);", BeginYear, EndYear, (int)CalendarLanguage));
            string script = string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", Page.ClientScript.GetWebResourceUrl(GetType(), "ASPNET.WebControls.Calendar.calendar.js"));
            if (!Page.ClientScript.IsStartupScriptRegistered(base.GetType(), "WebCalendarScript"))
            {
                Page.ClientScript.RegisterStartupScript(base.GetType(), "WebCalendarScript", script, false);
            }
            base.Render(writer);
        }
        #endregion

        #region 属性
        public int BeginYear { get; set; }

        CalendarLanguage m_CalendarLanguage = CalendarLanguage.zh_TW;
        public CalendarLanguage CalendarLanguage
        {
            get
            {
                return m_CalendarLanguage;
            }
            set
            {
                m_CalendarLanguage = value;
            }
        }

        CalendarType m_CalendarType = CalendarType.Default;
        public CalendarType CalendarType
        {
            get { return m_CalendarType; }
            set { m_CalendarType = value; }
        }

        public int EndYear { get; set; }

        public DateTime? SelectedDate
        {
            get
            {


                if (!string.IsNullOrEmpty(Text))
                {

                    string fulltimestr = "";

                    switch (m_CalendarType)
                    {
                        case WebControls.CalendarType.StartDate:
                            {
                                fulltimestr = string.Format("{0} 00:00:00", Text);
                                break;
                            }
                        case WebControls.CalendarType.EndDate:
                            {
                                fulltimestr = string.Format("{0} 23:59:59", Text);
                                break;
                            }
                        default:
                            {
                                fulltimestr = Text;
                                break;
                            }

                    }

                    DateTime selectedDate;
                    if (DateTime.TryParse(fulltimestr, out selectedDate))
                    {
                        return new DateTime?(selectedDate);
                    }

                }

                return null;

            }
            set
            {

                if (value.HasValue)
                {
                    Text = value.Value.ToString("yyyy-MM-dd");
                }

            }

        }
        #endregion


    }


}

