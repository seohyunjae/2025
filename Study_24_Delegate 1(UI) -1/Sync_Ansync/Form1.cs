using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sync_Ansync
{
    public partial class Form1 : Form
    {

        #region 전역

        CRobot _cRobot;  // Robot Class
        CDoor _cDoor1, _cDoor2;  // Door Class
        int _iRobot_Rotate = 0;  // Robot Rotate
        int _iSpeed = 100;  // Thread Sleep Time
        bool _bObjectExist = false;   // Robot이 Object를 가지고 있는지 여부

        #endregion


        public Form1()
        {
            InitializeComponent();
        }


        #region Event

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
                    break;
                case "btnD1Close":
                    break;
                case "btnD2Open":
                    break;
                case "btnD2Close":
                    break;
                case "btnRobotE":
                    break;
                case "btnRobotR":
                    break;
                case "btnRobotRotate":
                    break;
                case "btnSimulation":
                    break;
                case "btnSimulationAsync":
                    break;
                default:
                    break;
            }

            Log(enLogLevel.Info_L1, $"BtnTEST : {btn.Text}");
        }

        #endregion



        #region function

        /// <summary>
        /// 상태가 틀어 졌을 경우 초기 상태를 맞춰 주기 위해 추가
        /// </summary>
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

        

        /// <summary>
        /// Panel에 Robot을 Draw
        /// </summary>
        /// <param name="iRotate"></param>
        /// <param name="iRobot_Arm_Move"></param>
        /// <param name="isObject"></param>
        private void fRobotDraw(int iRotate, int iRobot_Arm_Move, bool isObject)
        {
            pRobot.Refresh();

            Graphics g = pRobot.CreateGraphics();

            g.FillEllipse(_cRobot.fBrushInfo(), _cRobot._rtCircle_Robot);

            _cRobot.fMove(iRobot_Arm_Move);

            Point center = new Point(100, 100);
            g.Transform = GetMatrix(center, iRotate);

            g.DrawRectangle(_cRobot.fPenInfo(), _cRobot._rtSquare_Arm);

            if (isObject)
            {
                g.FillRectangle(_cRobot.fBrushInfo(), _cRobot._rtSquare_Object);
            }
        }
        


        /// <summary>
        /// Panel에 Door 1을 Draw
        /// </summary>
        /// <param name="Move"></param>
        private void fDoor1Draw(int Move)
        {
            pDoor1.Refresh();

            _cDoor1.fMove(Move);

            Graphics g = pDoor1.CreateGraphics();

            g.FillRectangle(_cDoor1.fBrushInfo(), _cDoor1._rtDoor);
            g.DrawRectangle(_cDoor1.fPenInfo(), _cDoor1._rtDoorSide);
        }



        /// <summary>
        /// Panel에 Door 2를 Draw
        /// </summary>
        /// <param name="Move"></param>
        private void fDoor2Draw(int Move)
        {
            pDoor2.Refresh();

            _cDoor2.fMove(Move);

            Graphics g = pDoor2.CreateGraphics();

            g.FillRectangle(_cDoor2.fBrushInfo(), _cDoor2._rtDoor);
            g.DrawRectangle(_cDoor2.fPenInfo(), _cDoor2._rtDoorSide);
        }



        /// <summary>
        /// Robot 회전 시 사용 하는 함수 (실제 Robot이 회전하는게 아니고 Robot Arm을 Robot 중심 기준으로 회전 시킴)
        /// </summary>
        /// <param name="centerPoint"></param>
        /// <param name="rotateAngle"></param>
        /// <returns></returns>
        private Matrix GetMatrix(Point centerPoint, float rotateAngle)
        {
            Matrix matrix = new Matrix();

            matrix.RotateAt(rotateAngle, centerPoint);

            return matrix;
        }


        #endregion



        #region Log Viewer 

        //// Log Level을 지정 할 Enum (44강 Tree View 참조)
        enum enLogLevel
        {
            Info_L1,
            Info_L2,
            Info_L3,
            Warning,
            Error,
        }

        private void Log(enLogLevel eLevel, string LogDesc)
        {
            this.Invoke(new Action(delegate ()
            {
                DateTime dTime = DateTime.Now;
                string LogInfo = $"{dTime:yyyy-MM-dd hh:mm:ss.fff} [{eLevel.ToString()}] {LogDesc}";
                lboxLog.Items.Insert(0, LogInfo);
            }));
        }

        private void Log(DateTime dTime, enLogLevel eLevel, string LogDesc)
        {
            this.Invoke(new Action(delegate ()
            {
                string LogInfo = $"{dTime:yyyy-MM-dd hh:mm:ss.fff} [{eLevel.ToString()}] {LogDesc}";
                lboxLog.Items.Insert(0, LogInfo);
            }));
        }


        #endregion

    }
}
