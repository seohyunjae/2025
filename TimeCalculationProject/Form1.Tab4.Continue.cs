using System;
using System.Windows.Forms;

namespace TimeCalculationProject
{
	public partial class Form1 : Form
	{
		#region Tab4 (계속꾸준히)

		// 탭4 진입 시 호출(선택)
		private void EnterTab4()
		{
			LoadTaskGrid(2, dgw4continue);
		}

		private void BtnInsertContinue_Click(object sender, EventArgs e)
		{
			InsertTask(2, txt4continune, dgw4continue);
		}

		private void BtnDeleteContinue_Click(object sender, EventArgs e)
		{
			DeleteTask(2, dgw4continue);
		}

		#endregion
	}
}
