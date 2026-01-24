using System;
using System.Windows.Forms;

namespace TimeCalculationProject
{
	public partial class Form1 : Form
	{
		private void BtnInsertContinue_Click(object sender, EventArgs e)
		{
			InsertTask(2, txt4continune, dgw4continue);
		}

		private void BtnDeleteContinue_Click(object sender, EventArgs e)
		{
			DeleteTask(2, dgw4continue);
		}
	}
}
