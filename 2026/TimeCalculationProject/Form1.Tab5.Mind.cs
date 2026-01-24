using System;
using System.Windows.Forms;

namespace TimeCalculationProject
{
	public partial class Form1 : Form
	{
		private void BtnInsertMind_Click(object sender, EventArgs e)
		{
			InsertTask(3, txt5Mind, dgw5Mind);
		}

		private void BtnDeleteMind_Click(object sender, EventArgs e)
		{
			DeleteTask(3, dgw5Mind);
		}
	}
}
