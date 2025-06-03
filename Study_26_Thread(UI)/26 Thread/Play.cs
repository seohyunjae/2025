using System;
using System.Threading;
using System.Windows.Forms;

namespace _26_Thread
{
    public partial class Play : Form
    {
        public delegate int delMessage(object sender, string strResult);  // delegate 선언

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
            //_thread = new Thread(new ThreadStart(Run));  //ThreadStart 델리게이트 타입 객체를 생성 후 함수를 넘김

            _thread = new Thread(Run); // 컴파일러에서 델리게이트 객체를 추론해서 생성 후 함수로 넘김 (new Threadstat 생략)

            //_thread = new Thread(delegate () { Run(); }); //익명메소드를 사용하여 생성 후 함수를 넘김

            _thread.Start();
        }
        private void Run()
        {
            // UI Control이 자신이 만들어진 Thread가 아닌 다른 Thread에서 접근할 경우 Cross Thread가 발생
            //CheckForIllegalCrossThreadCalls = false; //Thread 충돌에 대한 예외 처리를 무시 (Cross Thread 무시)
            try
            {
                int ivar = 0;

                Random rd = new Random();

                while (pbarPlayer.Value < 100)
                {
                    if (this.InvokeRequired) //요청한 Thread가 현재 Main Tread 있는 Control를 엑세스 할 수 있는지
                    {
                        this.Invoke(new Action(delegate ()
                        {


                            //함수값
                            ivar = rd.Next(1, 11);
                            //barPlayer.Value = (pbarPlayer.Value + ivar > 100)
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
            catch  (ThreadInterruptedException exInterrupt)
            {
                exInterrupt.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ThreadAbort();
        }


        public void ThreadAbort()
        {
            if (_thread.IsAlive) //Thread가 동작 중일 경우
            {
                _thread.Abort(); // Thread를 강제 종료
            }
        }

        public void ThreadJoin()
        {
            if (_thread.IsAlive) //Thread가 동작 중일 경우
            {
                bool bThreadEnd = _thread.Join(3000);
            }
        }

        public void Threadinterrupt()
        {
            if (_thread.IsAlive) //Thread가 동작 중일 경우
            {
                _thread.Interrupt();
            }
        }
    }
}
