using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sync_Ansync
{
    public partial class Form1 : Form
    {
        //속성값
        CRobot _cRobot;
        CDoor _cDoor1, _cDoor2;
        int _iRobot_Rotate = 0;
        int _iSpeed = 100;
        bool _bObjectExist = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _cRobot = new CRobot("Robot");
            _cDoor1 = new CDoor("DoorLeft");
            _cDoor2 = new CDoor("DoorRight");
        }
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            switch (btn.Name)
            {
                case "btnInit":
                    initDraw();
                    break;
                case "btnD1Open":
                    Door1Open();
                    break;
                case "btnD1Close":
                    Door1Close();
                    break;
                case "btnD2Open":
                    Door2Open();
                    break;
                case "btnD2Close":
                    Door2Close();
                    break;
                case "btnRobotE":
                    RobotArmExtend();
                    break;
                case "btnRobotR":
                    RobotArmRetract();
                    break;
                case "btnRobotRotate":
                    RobotRotate();
                    break;
                case "btnSimulation":
                    Start_Move();
                    break;
                case "btnSimulationAsync":
                    //Start_Move_Async();
                    break;
                default:
                    break;
            }
            Log(enLoglevel.lnfo_L1, $"BtnTest : {btn.Text}");
        }

        private void tboxSpeed_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tboxSpeed.Text, out int iRet))
            {
                _iSpeed = iRet;
            }
            _iSpeed = int.Parse(tboxSpeed.Text);
        }
        #region Simulation
        private void Start_Move()
        {
            Task.Run(() =>
            {
                Log(enLoglevel.lnfo_L2, "Move Sequence Start");

                Door1Open();

                RobotArmExtend();
                _bObjectExist = true;
                RobotArmRetract();

                Door1Close();
                RobotRotate();
                Door2Open();
                RobotArmExtend();
                _bObjectExist = false;
                RobotArmRetract();
                Door2Close();
                RobotRotate();

                Log(enLoglevel.lnfo_L2, "Move Sequence Complete");
            });
        }
        #endregion

        #region 단위동작

        private int Door1Open()
        {
            Log(enLoglevel.lnfo_L1, "Door1 Open Start");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(_iSpeed);
                fDoor1Draw(-5);
            }

            Log(enLoglevel.lnfo_L1, "Door1 Open Complete");

            return 0;
        }


        private int Door1Close()
        {
            Log(enLoglevel.lnfo_L1, "Door1 Close Start");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(_iSpeed);
                fDoor1Draw(5);
            }

            Log(enLoglevel.lnfo_L1, "Door1 Close Complete");

            return 0;
        }

        private int Door2Open()
        {
            Log(enLoglevel.lnfo_L1, "Door2 Open Start");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(_iSpeed);
                fDoor2Draw(-5);
            }

            Log(enLoglevel.lnfo_L1, "Door2 Open Complete");

            return 0;
        }


        private int Door2Close()
        {
            Log(enLoglevel.lnfo_L1, "Door2 Close Start");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(_iSpeed);
                fDoor2Draw(5);
            }

            Log(enLoglevel.lnfo_L1, "Door2 Close Complete");

            return 0;
        }

        private int RobotArmExtend()
        {
            Log(enLoglevel.lnfo_L1, "Robot Arm Extend Start");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(_iSpeed);
                fRobotDraw(_iRobot_Rotate, -5, _bObjectExist);
            }

            Log(enLoglevel.lnfo_L1, "Robot Arm Extend Complete");

            return 0;
        }


        private int RobotArmRetract()
        {
            Log(enLoglevel.lnfo_L1, "Robot Arm Retract Start");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(_iSpeed);
                fRobotDraw(_iRobot_Rotate, 5, _bObjectExist);
            }

            Log(enLoglevel.lnfo_L1, "Robot Arm Retract Complete");

            return 0;
        }

        private int RobotRotate()
        {
            Log(enLoglevel.lnfo_L1, "Robot Rotate Start");

            // 각도 15도 기준으로 12 : 180도 만큼 돌아야 하니까
            for (int i = 0; i < 12; i++)
            {
                Thread.Sleep(_iSpeed);
                _iRobot_Rotate = _iRobot_Rotate + 15;
                fRobotDraw(_iRobot_Rotate, 0, _bObjectExist);
            }

            if (_iRobot_Rotate > 360) _iRobot_Rotate -= 360;

            Log(enLoglevel.lnfo_L1, "Robot Rotate Start");

            return 0;
        }


        #endregion

        #region function
        private void initDraw()
        {
            _cRobot = null;
            _cDoor1 = null;
            _cDoor2 = null;
            _iRobot_Rotate = 0;
            _bObjectExist = false;

            _cRobot = new CRobot("Robot");
            _cDoor1 = new CDoor("DoorLeft");
            _cDoor2 = new CDoor("DoorRight");

            fRobotDraw(_iRobot_Rotate, 0, _bObjectExist);
            fDoor1Draw(0);
            fDoor2Draw(0);
        }

        private void fRobotDraw(int iRotate, int iRobot_Arm_move, bool isObject)
        {
            pRobot.Refresh();

            Graphics g = pRobot.CreateGraphics();

            g.FillEllipse(_cRobot.fBrushInfo(), _cRobot._rtCircle_Robot);

            _cRobot.fMove(iRobot_Arm_move);

            Point center = new Point(100, 100);
            g.Transform = GetMatrix(center, iRotate);

            g.DrawRectangle(_cRobot.fPenInfo(), _cRobot._rtSquare_Arm);

            if (isObject)
            {
                g.FillRectangle(_cRobot.fBrushInfo(), _cRobot._rtSquare_Object);
            }
        }


        private Matrix GetMatrix(Point centerPoint, float rotateAngie)
        {
            Matrix matrix = new Matrix();

            matrix.RotateAt(rotateAngie, centerPoint);
            
            return matrix;  
        }


        private void fDoor1Draw(int Move)
        {
            pDoor1.Refresh();

            _cDoor1.fMove(Move);

            Graphics g = pDoor1.CreateGraphics();

            g.FillRectangle(_cDoor1.fBrushInfo(), _cDoor1._rtDoor);
            g.DrawRectangle(_cDoor1.fPenInfo(), _cDoor1._rtDoorSide);
        }

        private void fDoor2Draw(int Move)
        {
            pDoor2.Refresh();

            _cDoor2.fMove(Move);

            Graphics g = pDoor2.CreateGraphics();

            g.FillRectangle(_cDoor2.fBrushInfo(), _cDoor2._rtDoor);
            g.DrawRectangle(_cDoor2.fPenInfo(), _cDoor2._rtDoorSide);
        }
        #endregion

        private void Log(enLoglevel eLevel, string LogDesc)
        {
            this.Invoke(new Action(delegate ()
            {
                DateTime dTime = DateTime.Now;
                string LogInfo = $"{dTime:yyyy-MM-dd hh:mm:ss.fff} [{eLevel.ToString()}] {LogDesc}";
                lboxLog.Items.Insert(0, LogInfo);
            }));
        }

        private void Log(DateTime dTime, enLoglevel eLevel, string LogDesc)
        {
            this.Invoke(new Action(delegate ()
            {
                string LogInfo = $"{dTime:yyyy-MM-dd hh:mm:ss.fff} [{eLevel.ToString()}] {LogDesc}";
                lboxLog.Items.Insert(0, LogInfo);
            }));
        }



        #region LogView
        enum enLoglevel
        {
            lnfo_L1,
            lnfo_L2,
            lnfo_L3,
            Warning,
            Error,
        }
        #endregion
    }
}
