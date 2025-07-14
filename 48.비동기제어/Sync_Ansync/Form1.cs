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
        //속성값
        CRobot _cRobot;
        CDoor _cDoor1, _cDoor2;
        int _iRobot_Rotate = 0;
        int _iSpeed = 100;
        bool _bObjectExist =false;

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
                    //fRobotDraw(_iRobot_Rotate, 0, false);
                    //fDoor1Draw(0);
                    //fDoor2Draw(0);
                    //initDraw();
                    break;
                case "btnD1Open":
                    //Door1Open();
                    break;
                case "btnD1Close":
                    //Door1Close();
                    break;
                case "btnD2Open":
                    //Door2Open();
                    break;
                case "btnD2Close":
                    //Door2Close();
                    break;
                case "btnRobotE":
                    //RobotArmExtend();
                    break;
                case "btnRobotR":
                    //RobotArmRetract();
                    break;
                case "btnRobotRotate":
                    //RobotRotate();
                    break;
                case "btnSimulation":
                    //Start_Move();
                    break;
                case "btnSimulationAsync":
                    //Start_Move_Async();
                    break;
                default:
                    break;
            }
            Log(enLoglevel.lnfo_L1, $"BtnTest : {btn.Text}");
        }

        private void initDraw()
        {
            _cRobot = null;
            _cDoor1 = null;
            _cDoor2 = null;
            _iRobot_Rotate = 0;
        }

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
