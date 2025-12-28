using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
		private readonly string connectionString = @"Server=localhost;Database=TimeDB;Integrated Security=True;";

		public Form1()
		{
			InitializeComponent();
			CenterToParent();

			uiTimer = new Timer();
			uiTimer.Interval = 1000;
			uiTimer.Tick += UiTimer_Tick;
			uiTimer.Start();

			UpdateUi();
			BindCategoryCombo();
			UpdateRemainingToPicker(); // ✅ 처음 실행 시에도 표시

			// ✅ 값 바뀌면 바로 갱신
			dateTimePicker1.ValueChanged += DateTimePicker1_ValueChanged;
			tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

			// insert
			btninsertemergency.Click += BtnInsertEmergency_Click;
			btninsertcontinue.Click += BtnInsertContinue_Click;
			btninsertmind.Click += BtnInsertMind_Click;

			// delete
			btndeletemergency.Click += BtnDeleteEmergency_Click;
			btndeletecontinue.Click += BtnDeleteContinue_Click;
			btndeletemind.Click += BtnDeleteMind_Click;

			btn11.Tag = 0;
			btn1110.Tag = 10;
			btn1120.Tag = 20;
			btn1130.Tag = 30;

			btn11.Click += TimeButton_Click;
			btn1110.Click += TimeButton_Click;
			btn1120.Click += TimeButton_Click;
			btn1130.Click += TimeButton_Click;
		}
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
			DateTime baseDate = dateTimePicker1.Value.Date; // 날짜 유지
			dateTimePicker1.Value = new DateTime(
				baseDate.Year, baseDate.Month, baseDate.Day,
				hour24, minute, 0);
		}


		private void BtnInsertEmergency_Click(object sender, EventArgs e)
		{
			InsertTask(1, txtemergency, dgwemergency);
		}

		private void BtnInsertContinue_Click(object sender, EventArgs e)
		{
			InsertTask(2, txtcontinune, dgwcontinue);
		}

		private void BtnInsertMind_Click(object sender, EventArgs e)
		{
			InsertTask(3, txtMind, dgwMind);
		}

		private void BtnDeleteEmergency_Click(object sender, EventArgs e)
		{
			DeleteTask(1, dgwemergency);
		}

		private void BtnDeleteContinue_Click(object sender, EventArgs e)
		{
			DeleteTask(2, dgwcontinue);
		}

		private void BtnDeleteMind_Click(object sender, EventArgs e)
		{
			DeleteTask(3, dgwMind);
		}


		private void InsertTask(byte taskType, TextBox inputTextBox, DataGridView targetGrid)
		{
			string content = inputTextBox.Text.Trim();
			if (string.IsNullOrWhiteSpace(content))
			{
				MessageBox.Show("내용을 입력해 주세요.");
				inputTextBox.Focus();
				return;
			}

			DateTime insertTime = DateTime.Now;

			const string sql = @"
									INSERT INTO dbo.TaskItem (TaskType, Content, InsertTime)
									VALUES (@TaskType, @Content, @InsertTime);
								";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.Add("@TaskType", SqlDbType.TinyInt).Value = taskType;
				command.Parameters.Add("@Content", SqlDbType.NVarChar, 200).Value = content;
				command.Parameters.Add("@InsertTime", SqlDbType.DateTime2).Value = insertTime;

				connection.Open();
				command.ExecuteNonQuery();
			}

			// 등록 즉시 현재 탭 그리드 갱신
			LoadTaskGrid(taskType, targetGrid);

			inputTextBox.Clear();
			inputTextBox.Focus();
		}


		private void btninsert_Click(object sender, EventArgs e)
		{
			string content = txtemergency.Text.Trim();
			if (string.IsNullOrWhiteSpace(content))
			{
				MessageBox.Show("내용을 입력해 주세요.");
				return;
			}

			(byte taskType, DataGridView targetGrid) = GetCurrentTaskGridInfo();
			if (taskType == 0)
			{
				MessageBox.Show("급한거/꾸준히/마인드 탭에서만 등록할 수 있어요.");
				return;
			}


			// 3) CreatedAt: 현재시간
			DateTime insertTime = DateTime.Now;

			const string sql = @"
								INSERT INTO dbo.TaskItem (TaskType, Content, InsertTime)
											VALUES (@TaskType, @Content, @InsertTime);
								";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.Add("@TaskType", SqlDbType.TinyInt).Value = taskType;
				command.Parameters.Add("@Content", SqlDbType.NVarChar, 200).Value = content;
				command.Parameters.Add("@InsertTime", SqlDbType.DateTime2).Value = insertTime;

				connection.Open();
				int rows = command.ExecuteNonQuery();
				MessageBox.Show($"{rows}건 저장되었습니다.");
			}
			LoadTaskGrid(taskType, targetGrid);
			txtemergency.Clear();
			txtemergency.Focus();	
		}
		
		private void UiTimer_Tick(object sender, EventArgs e)
		{
			UpdateUi();
			UpdateRemainingToPicker(); // ✅ 매초 textBox13 갱신

		}
		private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadTaskGridBySelectedTab();
		}
		private void LoadTaskGridBySelectedTab()
		{
			TabPage selectedTab = tabControl.SelectedTab;
			if (selectedTab == null)
				return;

			if (selectedTab == tabPage2)
			{
				LoadExerciseGrid();
				return;
			}

			if (selectedTab == tabPage3)
			{
				LoadTaskGrid(1, dgwemergency); // TaskType=1
			}
			else if (selectedTab == tabPage4)
			{
				LoadTaskGrid(2, dgwcontinue); // TaskType=2
			}
			else if (selectedTab == tabPage5)
			{
				LoadTaskGrid(3, dgwMind); // TaskType=3
			}
		}

		private void LoadExerciseGrid()
		{
			const string sql = @"
									SELECT
										ExerciseDate,
										ExerciseMinutes,
										InsertTime,
										CASE WHEN ExerciseMinutes > 0 THEN N'Y' ELSE N'N' END AS IsExercised
									FROM dbo.Exercise
									ORDER BY ExerciseDate DESC;
								";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				DataTable table = new DataTable();
				connection.Open();

				using (SqlDataReader reader = command.ExecuteReader())
				{
					table.Load(reader);
				}

				dgwexercise.DataSource = table;
				dgwexercise.AutoResizeColumns();
			}
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
		private (byte TaskType, DataGridView Grid) GetCurrentTaskGridInfo()
		{
			if (tabControl.SelectedTab == tabPage3)
				return (1,dgwemergency);

			if (tabControl.SelectedTab == tabPage4)
				return (2, dgwcontinue);

			if (tabControl.SelectedTab == tabPage5)
				return (3, dgwMind);

			return (0, null);
		}
		private void DeleteTask(byte taskType, DataGridView targetGrid)
		{
			if (targetGrid == null)
				return;

			// 1) 선택/포커스 행 가져오기
			DataGridViewRow row = targetGrid.SelectedRows.Count > 0
				? targetGrid.SelectedRows[0]
				: targetGrid.CurrentRow;

			if (row == null)
			{
				MessageBox.Show("삭제할 행을 먼저 선택해 주세요.");
				return;
			}

			// 2) TaskId 가져오기
			object idValue = row.Cells["TaskId"]?.Value;
			if (idValue == null || idValue == DBNull.Value)
			{
				MessageBox.Show("선택한 행에서 TaskId를 찾을 수 없습니다.");
				return;
			}

			int taskId = Convert.ToInt32(idValue);

			// 3) 삭제 확인
			DialogResult result = MessageBox.Show(
				"선택한 데이터를 삭제하시겠습니까?",
				"삭제 확인",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (result != DialogResult.Yes)
				return;

			// 4) DB 삭제
			const string sql = "DELETE FROM dbo.TaskItem WHERE TaskId = @TaskId;";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.Add("@TaskId", SqlDbType.Int).Value = taskId;

				connection.Open();
				int rows = command.ExecuteNonQuery();

				if (rows <= 0)
				{
					MessageBox.Show("삭제할 데이터가 없습니다.");
					return;
				}
			}

			// 5) 삭제 후 해당 탭 그리드 즉시 갱신
			LoadTaskGrid(taskType, targetGrid);

			MessageBox.Show("삭제되었습니다.");
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
		}
		
		private void LoadTaskGrids()
		{
			LoadTaskGrid(1, dgwemergency); // 급한거
			LoadTaskGrid(2, dgwcontinue); // 계속꾸준히해야하는거
			LoadTaskGrid(3, dgwMind); // 마인드
		}

		private void LoadTaskGrid(byte taskType, DataGridView targetGrid)
		{
			const string sql = @"
									;WITH Cte AS
									(
										SELECT
											ROW_NUMBER() OVER (ORDER BY InsertTime DESC, TaskId DESC) AS [순서],
											TaskId, TaskType, Content, InsertTime
										FROM dbo.TaskItem
										WHERE TaskType = @TaskType
									)
									SELECT [순서], TaskId, TaskType, Content, InsertTime
									FROM Cte
									ORDER BY [순서];
								";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.Add("@TaskType", SqlDbType.TinyInt).Value = taskType;

				DataTable table = new DataTable();
				connection.Open();

				using (SqlDataReader reader = command.ExecuteReader())
				{
					table.Load(reader);
				}

				targetGrid.DataSource = table;
				targetGrid.AutoResizeColumns();

				if (targetGrid.Columns["TaskId"] != null)
					targetGrid.Columns["TaskId"].Visible = false;

				if (targetGrid.Columns["TaskType"] != null)
					targetGrid.Columns["TaskType"].Visible = false;

				// (선택) 순서 컬럼 폭/정렬 잠금
				if (targetGrid.Columns["순서"] != null)
				{
					targetGrid.Columns["순서"].Width = 60;
					targetGrid.Columns["순서"].SortMode = DataGridViewColumnSortMode.NotSortable;
				}
			}
		}

		private void btninsertexercise_Click(object sender, EventArgs e)
		{
			// 1) 날짜 (yyyyMMdd로 보여도 DB에는 Date 타입으로 들어가면 됨)
			DateTime exerciseDate = Dateexercise.Value.Date;

			// 2) 분(ExerciseMinutes)
			string minutesText = timeexercise.Text.Trim();
			if (!int.TryParse(minutesText, out int exerciseMinutes) || exerciseMinutes < 0 || exerciseMinutes > 1440)
			{
				MessageBox.Show("운동 시간(분)을 0~1440 사이 숫자로 입력해 주세요.");
				timeexercise.Focus();
				return;
			}

			// 3) 현재시간(InsertTime)
			DateTime insertTime = DateTime.Now;

			// 4) 같은 날짜가 있으면 UPDATE, 없으면 INSERT
			const string sql = @"
					IF EXISTS (SELECT 1 FROM dbo.Exercise WHERE ExerciseDate = @ExerciseDate)
					BEGIN
						UPDATE dbo.Exercise
						SET ExerciseMinutes = @ExerciseMinutes,
								 UpdateTime = SYSDATETIME()
						 WHERE ExerciseDate = @ExerciseDate;
					END
					ELSE
					BEGIN
						INSERT INTO dbo.Exercise (ExerciseDate, ExerciseMinutes, InsertTime)
						VALUES (@ExerciseDate, @ExerciseMinutes, @InsertTime);
					END
					";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.Add("@ExerciseDate", SqlDbType.Date).Value = exerciseDate;
				command.Parameters.Add("@ExerciseMinutes", SqlDbType.Int).Value = exerciseMinutes;
				command.Parameters.Add("@InsertTime", SqlDbType.DateTime2).Value = insertTime;

				connection.Open();
				command.ExecuteNonQuery();
			}
			LoadExerciseGrid();

			timeexercise.Focus();
			MessageBox.Show("운동 기록이 저장(또는 수정)되었습니다.");
		}

		private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			UpdateRemainingToPicker(); // ✅ 사용자가 시간 바꾸면 즉시 반영
		}
		/// <summary>
		/// Tab : 자야하는시간설정
		/// </summary>
		private void UpdateRemainingToPicker()
		{
			DateTime target = dateTimePicker1.Value;
			DateTime now = DateTime.Now;

			TimeSpan diff = target - now;

			if (diff >= TimeSpan.Zero)
			{
				textBox13.Text = $"남음: {FormatSpan(diff)}";
			}
			else
			{
				textBox13.Text = $"지남: {FormatSpan(diff.Negate())}";
			}
		}

        private void btn11_Click(object sender, EventArgs e)
        {

        }
    }


}
