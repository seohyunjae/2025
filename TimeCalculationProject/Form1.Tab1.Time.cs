using System;
using System.Windows.Forms;

namespace TimeCalculationProject
{
	public partial class Form1 : Form
	{
		private void UpdateUi()
		{
			DateTime now = DateTime.Now;

			lbl1birth.Text = $"{BirthDate:yyyy년 M월 d일}";

			TimeSpan livedTotal = now - BirthDate;
			TimeSpan livedAwake = GetPartSpan(livedTotal, AwakeHoursPerDay);
			TimeSpan livedSleep = GetPartSpan(livedTotal, SleepHoursPerDay);

			// ✅ 추가: 살아온 년수(소수점)
			double livedYears = livedTotal.TotalDays / 365.2425;

			lbl1time.Text =
				$"살아온 시간(전체): {FormatSpan(livedTotal)}\r\n" +
				$"살아온 년수: {livedYears:0.0}년\r\n" +
				$"의식 시간(16h/일): {FormatSpan(livedAwake)}\r\n" +
				$"수면 시간(8h/일): {FormatSpan(livedSleep)}";

			SetCountdown(txtbox1live30, txtbox1awake30, txtbox1sleep30, BirthDate.AddYears(30), now);
			SetCountdown(txtbox1live40, txtbox1awake40, txtbox1sleep40, BirthDate.AddYears(40), now);
			SetCountdown(txtbox1live60, txtbox1awake60, txtbox1sleep60, BirthDate.AddYears(60), now);
			SetCountdown(txtbox1live65, txtbox1awake65, txtbox1sleep65, BirthDate.AddYears(65), now);
		}

		private void SetCountdown(TextBox totalTextBox, TextBox awakeTextBox, TextBox sleepTextBox, DateTime targetDate, DateTime now)
		{
			TimeSpan diffTotal = targetDate - now;
			TimeSpan diffAwake = GetPartSpan(diffTotal, AwakeHoursPerDay);
			TimeSpan diffSleep = GetPartSpan(diffTotal, SleepHoursPerDay);

			if (diffTotal >= TimeSpan.Zero)
			{
				totalTextBox.Text = $"남음(전체): {FormatSpan(diffTotal)}";
				awakeTextBox.Text = $"남음(의식): {FormatSpan(diffAwake)}";
				sleepTextBox.Text = $"남음(수면): {FormatSpan(diffSleep)}";
			}
			else
			{
				totalTextBox.Text = $"지남(전체): {FormatSpan(diffTotal.Negate())}";
				awakeTextBox.Text = $"지남(의식): {FormatSpan(diffAwake.Negate())}";
				sleepTextBox.Text = $"지남(수면): {FormatSpan(diffSleep.Negate())}";
			}
		}
	}
}
