using System;
using System.Windows.Forms;

namespace TimeCalculationProject
{
	public partial class Form1 : Form
	{
		private void BtnInsertEmergency_Click(object sender, EventArgs e)
		{
			InsertTask(1, txt3emergency, dgw3emergency);
		}

		private void BtnDeleteEmergency_Click(object sender, EventArgs e)
		{
			DeleteTask(1, dgw3emergency);
		}
	}
}
