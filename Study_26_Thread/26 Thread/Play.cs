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
        public delegate int delMessage(object sender, string strResult);  // delgate 선언
        public event delMessage eventdelMessage;


        string _strPlayerName = string.Empty;
        public string StrPlayerName { get => _strPlayerName; set => _strPlayerName = value; }

        Thread _thread = null;

        

        public Play()
        {
            InitializeComponent();
        }

        public Play(string strPlayerName)
        {
            InitializeComponent();

            lblPlayerName.Text = StrPlayerName = strPlayerName;
        }

        public void fThreadStart()
        {
            //_thread = new Thread(new ThreadStart(Run));   // ThreadStart 델리게이트 타입 객체를 생성 후 함수를 넘김

            _thread = new Thread(Run);   // 컴파일러에서 델리게이트 객체를 추론해서 생성 후 생성 후 함수를 넘김 (new ThreadStart 생략)

            _thread = new Thread(delegate () { Run(); });   // 익명메서드를 사용하여 생성 후 함수를 넘김
            
            _thread.Start();
        }


        private void Run()
        {
            // UI Control이 자신이 만들어진 Thread가 아닌 다른 Thread에서 접근할 경우 Cross Thread가 발생
            // CheckForIllegalCrossThreadCalls = false;   // Thread 충돌에 대한 예외 처리를 무시 (Cross Thread 무시)

            int ivar = 0;

            Random rd = new Random();

            while (pbarPlayer.Value < 100)
            {
                if (this.InvokeRequired)   // 요청 한 Thread가 현재 Main Thread 있는 Contorl을 엑세스 할 수 있는지 확인
                {
                    this.Invoke(new Action(delegate ()
                    {

                        //함수값
                        ivar = rd.Next(1, 11);
                        //pbarPlayer.Value = ()
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
                    }));

                    Thread.Sleep(300);
                }
            }

            eventdelMessage(this, "완주!! (Thread Complete)");
        }
        
    }
}
