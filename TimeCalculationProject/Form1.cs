using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TimeCalculationProject
{
	public partial class Form1 : Form
	{
		// 기준 생일
		private static readonly DateTime BirthDate = new DateTime(1997, 8, 30, 0, 0, 0);

		// 하루 기준(가정)
		private const double SleepHoursPerDay = 8.0;
		private const double AwakeHoursPerDay = 16.0; // 24 - 8
		private readonly Timer uiTimer;

		public Form1()
		{
			InitializeComponent();

			uiTimer = new Timer();
			uiTimer.Interval = 1000;
			uiTimer.Tick += UiTimer_Tick;
			uiTimer.Start();

			UpdateUi();
			GetSelectedCategoryId();
			BindCategoryCombo();
		}

		private void UiTimer_Tick(object sender, EventArgs e)
		{
			UpdateUi();
		}

		private void UpdateUi()
		{
			DateTime now = DateTime.Now;

			// 1) 태어난 날짜 표시
			label.Text = $"태어난 날짜: {BirthDate:yyyy년 M월 d일}";

			// 2) 살아온 시간(전체/의식/수면)
			TimeSpan livedTotal = now - BirthDate;
			TimeSpan livedAwake = GetPartSpan(livedTotal, AwakeHoursPerDay);
			TimeSpan livedSleep = GetPartSpan(livedTotal, SleepHoursPerDay);

			// label7 한 줄이면 길어서 줄바꿈으로 표시(라벨 AutoSize면 잘 보임)
			label1.Text =
					$"살아온 시간(전체): {FormatSpan(livedTotal)}\r\n" +
					$"의식 시간(16h/일): {FormatSpan(livedAwake)}\r\n" +
					$"수면 시간(8h/일): {FormatSpan(livedSleep)}";

			// 3) 목표 나이까지 남은 시간 (전체/의식/수면) -> 텍스트박스 3개씩 사용
			SetCountdown(textBox1, textBox5, textBox9, BirthDate.AddYears(30), now);
			SetCountdown(textBox2, textBox6, textBox10, BirthDate.AddYears(40), now);
			SetCountdown(textBox3, textBox7, textBox11, BirthDate.AddYears(60), now);
			SetCountdown(textBox4, textBox8, textBox12, BirthDate.AddYears(65), now);
		}

		/// <summary>
		/// diff(기간)에서 "하루 hoursPerDay 만큼"의 시간만 뽑아서 계산
		/// 예) hoursPerDay=16 => 의식 시간, 8 => 수면 시간
		/// </summary>
		/// 

		private TimeSpan GetPartSpan(TimeSpan span, double hoursPerDay)
		{
			bool isNegative = span < TimeSpan.Zero;
			TimeSpan abs = isNegative ? span.Negate() : span;

			// 기간(일) * (하루 해당 시간)
			TimeSpan part = TimeSpan.FromHours(abs.TotalDays * hoursPerDay);

			// 방어코드
			if (part < TimeSpan.Zero)
			{
				part = TimeSpan.Zero;
			}

			return isNegative ? part.Negate() : part;
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
		private string FormatSpan(TimeSpan span)
		{
			long totalDays = (long)span.TotalDays;
			long totalHours = (long)span.TotalHours;

			return $"{totalDays:N0}일 (총 {totalHours:N0}시간) {span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}";
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			uiTimer.Stop();
			uiTimer.Dispose();
			base.OnFormClosing(e);
		}
		private sealed class CategoryItem
		{
			public byte CategoryId { get; set; }
			public string CategoryName { get; set; } = "";
		}
		private void BindCategoryCombo()
		{
			var items = new List<CategoryItem>
			{
				new CategoryItem { CategoryId = 1, CategoryName = "급한거" },
				new CategoryItem { CategoryId = 2, CategoryName = "계속꾸준히해야하는거" },
				new CategoryItem { CategoryId = 3, CategoryName = "마인드" }
			};

			comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox1.DisplayMember = nameof(CategoryItem.CategoryName);
			comboBox1.ValueMember = nameof(CategoryItem.CategoryId);
			comboBox1.DataSource = items;
		}
		
		private byte GetSelectedCategoryId()
		{
			return comboBox1.SelectedValue is byte v ? v : (byte)0;
		}
	}
}
