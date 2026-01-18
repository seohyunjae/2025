using System;
using System.Windows.Forms;

namespace TimeCalculationProject
{
	public partial class Form1 : Form
	{
		#region Tab5 (마인드)

		// 탭5 진입 시 호출(선택)
		private void EnterTab5()
		{
			LoadTaskGrid(3, dgw5Mind);
		}

		private void BtnInsertMind_Click(object sender, EventArgs e)
		{
			InsertTask(3, txt5Mind, dgw5Mind);
		}

		private void BtnDeleteMind_Click(object sender, EventArgs e)
		{
			DeleteTask(3, dgw5Mind);
		}

		#endregion
	}
}
