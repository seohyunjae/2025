using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _24_DelegatePizzaOrder
{
    public partial class frmPizza : Form
    {
        public delegate int delPizzComplete(string strResult, int iTime);
        public event delPizzComplete eventdelPizzaComplete; //delegate event 선언


        public frmPizza()
        {
            InitializeComponent();
        }
    


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        internal void fPizzrCheck(Dictionary<string, int> dPizzaOrder)
        {
            int iTotaltime = 0;
            foreach (KeyValuePair<string, int> oOrder in dPizzaOrder)
            {
                int iNowTime = 0;
                string strType = string.Empty;
                int iTime = 0;
                int iCount = oOrder.Value;


                switch (oOrder.Key)
                {
                    // 도우
                    case "오리지널":
                        iNowTime = 3000;
                        strType = "도우";
                  
                        break;
                    case "씬":
                        iNowTime = 3500;
                        strType = "도우";
                        


                        break;

                    // 엣지
                    case "리치골드":
                        iNowTime = 500;
                        strType = "엣지";

                        break;
                    case "치즈크러스터":
                        iNowTime = 400;
                        strType = "엣지";

                        break;

                    //토핑
                    case "소세지":
                        iNowTime = 32;
                        strType = "토핑";

                        break;
                    case "감자":
                        iNowTime = 17;
                        strType = "토핑";

                        break;
                    case "치즈":
                        iNowTime = 48;
                        strType = "토핑";

                        break;



                    default:
                        break;
                }
                iTime = iNowTime * iCount;
                lboxMake.Items.Add(string.Format("{0} ) {1} : {2}초 ({3}초, {4}개)", strType, oOrder.Key,
                     iTime, iNowTime, oOrder.Value));

                Refresh();
                Thread.Sleep(1000);
            }

            int i = 0;
            while (i < 3)
            {
                i++;
                lboxMake.Items.Add(i.ToString());

                this.Refresh();
                Thread.Sleep(300);
            }

            eventdelPizzaComplete("Pizza가 완료되었습니다.", 1000);

        }
    }
}
