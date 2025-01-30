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
        SortedList<DateTime, string> slScheduler = new SortedList<DateTime, string>(); // 키값 기준으로 정렬 검색편안
        Dictionary<DateTime, string> dScheduler = new Dictionary<DateTime, string>();

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

            DateTime dSetDate = mcScheduler.SelectionStart;

            if (!slScheduler.ContainsKey(dSetDate))
            {
                slScheduler.Add(dSetDate, tboxScheduler.Text);

                mcScheduler.AddBoldedDate(dSetDate);
                mcScheduler.UpdateBoldedDates();

                //lboxScheduler.Items.Add(string.Format("{0} : {1}", dSetDate.ToString(), txboxScheduler.Text));
            }

            lboxScheduler.Items.Clear();

            foreach (KeyValuePair<DateTime, string> oitem in slScheduler)
            {
                lboxScheduler.Items.Add(string.Format("{0} : {1}", oitem.Key.ToString("yyyy-MM-dd"), oitem.Value));
            }
        }

        private void mcScheduler_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime dSetDate = mcScheduler.SelectionStart;

            if (slScheduler.ContainsKey(dSetDate))
            {
                tboxScheduler.Text = slScheduler[dSetDate];
            }
            else
            {
                tboxScheduler.Text = string.Empty;
            }
        }
    }
}
