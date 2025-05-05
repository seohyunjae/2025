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

namespace _26_Thread
{
    public partial class Play : Form
    {
        string _strPlayerName = string.Empty;

        Thread _thread = null;
        public Play()
        {
            InitializeComponent();
        }
        public Play(string strPlayerName)
        {
            InitializeComponent();

            lblPlayerName.Text = _strPlayerName = strPlayerName;
        }

        public void fThreadStart()
        {
            //_thread = new Thread(new ThreadStart(Run)); // ThreadStart 델리게이트 타입 객체를 생성후 함수를 넘김

            _thread = new Thread(Run); // 컴파일러 델리게이트 객체를 추론해서 생성 후 함수를 넘김 (new ThreadStart 생략)

            //_thread = new Thread(delegate () { Run(); }); // 익명세서드를 사용하여 생성 후 함수를 넘김

            _thread.Start();
        }

        private void Run()
        {
            // UI Control이 자신이 만들어진 Thread가 아닌 다른 Thread에서 접근할 경우 Cross Thread가 발생
            //CheckForIllegalCrossThreadCalls = false; // Thread 충돌에 대한 예외 처리를 무시 (Cross Thread 무시)
                                
            int ivar = 0;

            Random rd = new Random();

            while (pbarPlayer.Value < 100)
            {
                this.Invoke(new Action(delegate ()
                {   
                    //함수값 


                }));

                ivar = rd.Next(1, 11);
                if (pbarPlayer.Value + ivar > 100)
                {
                    pbarPlayer.Value = 100;
                }
                else
                {
                    pbarPlayer.Value = pbarPlayer.Value + ivar;
                }
                lblProcess.Text = string.Format("진행 상황 표시 : {0}%", pbarPlayer.Value);

                this.Refresh();
                Thread.Sleep(300);
            }
        }
    }
}
