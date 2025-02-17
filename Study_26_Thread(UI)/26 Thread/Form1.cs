using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _26_Thread
{
    public partial class Form1 : Form
    {
        private enum enumPlayer
        {
            아이린,
            슬기,
            웬디,
            조이,
            예리,
        }

        int _locationX = 0;
        int _locationY = 0;

        public Form1()
        {
            InitializeComponent();

            _locationX = this.Location.X;
            _locationY = this.Location.Y;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _locationX = this.Location.X + this.Width;
            _locationY = this.Location.Y;

            for (int i = 0; i < numPlayerCount.Value; i++)
            {
                Play p1 = new Play(((enumPlayer)i).ToString());
                p1.Location = new Point(_locationX, _locationY + p1.Height * i);
                p1.Show();

                p1.test();
            }
        }
    }
}
