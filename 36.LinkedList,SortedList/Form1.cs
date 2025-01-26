using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _36.LinkedList_SortedList
{
    public partial class Form1 : Form
    {
        SortedList<DateTime, string> slScheduler = new SortedList<DateTime, string>();
        public  Form1()
        {
            InitializeComponent();
        }

        private void btnScheduler_Click(object sender, EventArgs e)
        {
            // Linked List

            //LinkedList<string> sLinkTest = new LinkedList<string>();

            //for (int i = 0; i < 10; i++)
            //{
            //    sLinkTest.AddLast(i.ToString());
            //}

            //var findnode = sLinkTest.Find("5");
            //sLinkTest.AddAfter(findnode, "NewValue");

            // Sorted List



        }
    }
}
