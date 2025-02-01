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

namespace _38.Lock
{
    public partial class Form1 : Form
    {
        Thread _T1;
        Thread _T2;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRoom1_Click(object sender, EventArgs e)
        {
            _T1 = new Thread(Run);
            _T1.Start();
        }

        private void Run()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(delegate ()
                {
                    lboxRoom1.Items.Add("1번 예약");
                }));
            }

        }

        private void btnRoom2_Click(object sender, EventArgs e)
        {

        }
    }
}
