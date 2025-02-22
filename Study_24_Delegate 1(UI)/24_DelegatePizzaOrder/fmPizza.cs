using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _24_DelegatePizzaOrder
{
    public partial class fmPizza : Form
    {
        public delegate int delPizzaComplete(string strResult, int iTime); //델리게이트의 타입의 함수이름
        public event delPizzaComplete eventdelPizzaComplete; //event의 타입을 델리게이터로, delegate event 선언
                                                             //public event EventHandler
        private bool bOrderComplete = false; //제작이 완료 됬는지 확인 하는 flag

        public bool BOrderComplete { get => bOrderComplete; set => bOrderComplete = value; }

        public fmPizza()
        {
            InitializeComponent();
        }

        //public fmPizza(Dictionary<string, int> dPizzOrder)
        //{
        //    InitializeComponent();
        //}

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   
        internal void fPizzrCheck(Dictionary<string, int> dPizzOrder)
        {

            BOrderComplete = false;

            int iTotalTime = 0;

            foreach (KeyValuePair<string, int> oOrder in dPizzOrder)
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
                        iTime = iNowTime * iCount;
                        

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


                    // 토핑
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
                iTotalTime = iTotalTime * iTime;
                lboxMake.Items.Add(string.Format("{0}) {1} : {2}초 ({3}초, {4}개)", strType, oOrder.Key
                            , iTime, iNowTime, iCount));

                this.Refresh();
                Thread.Sleep(1000);
            }

            int iRet = eventdelPizzaComplete("Pizza가 완성 되었습니다.", iTotalTime);

            BOrderComplete = true;

            if (iRet == 0)
            {
                lboxMake.Items.Add("-------------------");
                lboxMake.Items.Add("주문 완료 확인!!");
            }
            else
            {
                lboxMake.Items.Add("제작 시간 초과로 고객 클레임 고객 반품!!");
            }
        }
    }
}
