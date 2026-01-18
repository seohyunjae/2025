using System;
using System.Windows.Forms;

namespace TimeCalculationProject
{
	public partial class Form1 : Form
	{
		private void TimeButton_Click(object sender, EventArgs e)
		{
			Button button = sender as Button;
			if (button == null)
				return;

			int minute = 0;
			if (button.Tag != null)
				minute = Convert.ToInt32(button.Tag);

			SetPickerTime(23, minute);
		}

		private void SetPickerTime(int hour24, int minute)
		{
			DateTime baseDate = dateTimePicker6.Value.Date;
			dateTimePicker6.Value = new DateTime(
				baseDate.Year, baseDate.Month, baseDate.Day,
				hour24, minute, 0);
		}

		private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
		{
			UpdateRemainingToPicker();
		}

		private void UpdateRemainingToPicker()
		{
			DateTime target = dateTimePicker6.Value;
			DateTime now = DateTime.Now;

			TimeSpan diff = target - now;

			if (diff >= TimeSpan.Zero)
				txt6time.Text = $"남음: {FormatSpan(diff)}";
			else
				txt6time.Text = $"지남: {FormatSpan(diff.Negate())}";
		}
	}
}
