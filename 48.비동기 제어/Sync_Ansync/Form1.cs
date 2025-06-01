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
        CRobot _cRobot;
        CDoor _cDoor1, _cDoor2;


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
                case "btninit":
                    break;
                case "btnD1Open":
                    break;
                case "btnD1Colse":
                    break;
                case "btnD2Openn":
                    break;
                case "btnD2Colse":
                    break;
                case "btnRobotE":
                    break;
                case "btnRobotR":
                    break;
                case "btnRobotRotate":
                    break;
                case "btnSimuation":
                    break;
                case "btnSimualtionAsync":
                    break;
                default:
                    break;
            }
        }

        #region LogView
        private void Log(enLogLevel eLevel, string LogDesc)
        {
            this.Invoke(new Action(delegate ()))
            {

            }
        }


    }
}
