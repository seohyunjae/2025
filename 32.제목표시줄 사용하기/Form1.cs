using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace _32.제목표시줄_사용하기
{
    public partial class Form1 : Form
    {
        static string strVersion = "1.0.0.0";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Version oVersion = Assembly.GetEntryAssembly().GetName().Version;
            //string dd = Assembly.GetEntryAssembly().GetName().Name;

            this.Text = "Title" + oVersion;
            this.Text = string.Format("{0} Ver.{1}.{2} / Build Time({3}) - {4}", "Title 사용하기",
                oVersion.Major, oVersion.Minor, GetBuildDateTime(oVersion), "프로그램상태");

            GetBuildDateTime(oVersion);
        }

        public DateTime GetBuildDateTime(Version oVersion)
        {
            string strVerstion = oVersion.ToString();

            // 날짜 등록
            int iDays = Convert.ToInt32(strVerstion.Split('.')[2]);
            DateTime refData = new DateTime(2000, 1, 1);
            DateTime dtBuildDate = refData.AddDays(iDays);

            // 초 등록
            int iSeconds = Convert.ToInt32(strVerstion.Split('.')[3]);
            iSeconds = iSeconds * 2;
            dtBuildDate = dtBuildDate.AddSeconds(iSeconds);

            // 시차 조정
            DaylightTime daylighttime = TimeZone.CurrentTimeZone.GetDaylightChanges(dtBuildDate.Year);

            if (TimeZone.IsDaylightSavingTime(dtBuildDate, daylighttime))
            {
                dtBuildDate = dtBuildDate.Add(daylighttime.Delta);
            }


            return dtBuildDate;
        }

        // Dictionary 예제
        Dictionary<string, Stack<CSize>> oDic = new Dictionary<string, Stack<CSize>>();

        private void btnSizeCheck_Click(object sender, EventArgs e)
        {
            ControlSizeRead();
            ControlSizeWrite();
        }

        private void ControlSizeWrite()
        {
            // DIc에서 Button정보 가져옴
            Stack<CSize> sButton = oDic["BUTTON"];
            foreach (CSize item in sButton)
            {
                string strResult = string.Format("Control : Button, Name : {0}, Size ({1} , {2})", item.Name, item.Width, item.Height);
                lboxResult.Items.Add(strResult);
            }

            Stack<CSize> sLabel = oDic["LABEL"];
            foreach (CSize item in sLabel)
            {
                string strResult = string.Format("Control : Label, Name : {0}, Size ({1} , {2})", item.Name, item.Width, item.Height);
                lboxResult.Items.Add(strResult);
            }
        }

        private void ControlSizeRead()
        {
            oDic.Clear();

            //Button 등록

            Stack<CSize> sButton = new Stack<CSize>();
            foreach (var item in gboxControl.Controls)
            {
                if (item is Button)
                {
                    Button obtn = item as Button;

                    CSize oSize = new CSize();
                    oSize.Name = obtn.Name;
                    oSize.Width  = obtn.Width;      
                    oSize.Height = obtn.Height;

                    sButton.Push(oSize);
                }
            }
            oDic.Add("BUTTON", sButton);

            Stack<CSize> sLabel = new Stack<CSize>();
            foreach (var item in gboxControl.Controls)
            {
                if (item is Label)
                {
                    Label obtn = item as Label;

                    CSize oSize = new CSize();
                    oSize.Name = obtn.Name;
                    oSize.Width = obtn.Width;
                    oSize.Height = obtn.Height;

                    sLabel.Push(oSize);
                }
            }
            oDic.Add("LABEL", sLabel);

        }
    }    
}
