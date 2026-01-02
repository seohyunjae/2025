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
			btn3insertemergency.Click += BtnInsertEmergency_Click;
			btn4insertcontinue.Click += BtnInsertContinue_Click;
			btninsertmind.Click += BtnInsertMind_Click;

			// delete
			btn3deletemergency.Click += BtnDeleteEmergency_Click;
			btn4deletecontinue.Click += BtnDeleteContinue_Click;
			btndeletemind.Click += BtnDeleteMind_Click;

			// 자주 쓰는 시간 버튼에 분(Tag) 값 설정 및 클릭 이벤트 연결
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
			InsertTask(1, txt3emergency, dgw3emergency);
		}

		private void BtnInsertContinue_Click(object sender, EventArgs e)
		{
			InsertTask(2, txt4continune, dgw4continue);
		}

		private void BtnInsertMind_Click(object sender, EventArgs e)
		{
			InsertTask(3, txtMind, dgwMind);
		}

		private void BtnDeleteEmergency_Click(object sender, EventArgs e)
		{
			DeleteTask(1, dgw3emergency);
		}

		private void BtnDeleteContinue_Click(object sender, EventArgs e)
		{
			DeleteTask(2, dgw4continue);
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

		// 폼에 있는 공용 btninsert 클릭 (현재 선택된 탭으로 저장)
		private void btninsert_Click(object sender, EventArgs e)
		{
			string content = txt3emergency.Text.Trim();
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

			// CreatedAt: 현재시간
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
			txt3emergency.Clear();
			txt3emergency.Focus();
		}

		// 삭제: 선택된 행의 TaskId로 DB에서 삭제하고 그리드 갱신
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

		#region 탭 변경 처리 및 그리드 로드 (TabControl 선택에 따라 각 탭의 데이터를 로드)
		private void UiTimer_Tick(object sender, EventArgs e)
		{
			UpdateUi();
			UpdateRemainingToPicker(); // ✅ 매초 textBox13 갱신
		}

		private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadTaskGridBySelectedTab();
		}

		// 탭이 바뀌면 해당 탭에 맞는 데이터(운동 기록 또는 Task 그리드)를 로드
		private void LoadTaskGridBySelectedTab()
		{
			TabPage selectedTab = tabControl.SelectedTab;
			if (selectedTab == null)
				return;

			// 탭 순서/이름에 따라 분기 처리
			if (selectedTab == tabPage2)
			{
				// 운동 탭
				LoadExerciseGrid();
				return;
			}

			if (selectedTab == tabPage3)
			{
				// 급한거 탭
				LoadTaskGrid(1, dgw3emergency); // TaskType=1
			}
			else if (selectedTab == tabPage4)
			{
				// 계속 꾸준히 해야 하는거 탭
				LoadTaskGrid(2, dgw4continue); // TaskType=2
			}
			else if (selectedTab == tabPage5)
			{
				// 마인드 탭
				LoadTaskGrid(3, dgwMind); // TaskType=3
			}
		}
		#endregion

		#region 운동 기록(Exercise) 탭 처리
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

				dgw2exercise.DataSource = table;
				dgw2exercise.AutoResizeColumns();
			}
		}

		// 운동 기록 추가/수정 처리 (버튼 클릭)
		private void btninsertexercise_Click(object sender, EventArgs e)
		{
			// 1) 날짜 (yyyyMMdd로 보여도 DB에는 Date 타입으로 들어가면 됨)
			DateTime exerciseDate = Date2exercise.Value.Date;

			// 2) 분(ExerciseMinutes)
			string minutesText = time2exercise.Text.Trim();
			if (!int.TryParse(minutesText, out int exerciseMinutes) || exerciseMinutes < 0 || exerciseMinutes > 1440)
			{
				MessageBox.Show("운동 시간(분)을 0~1440 사이 숫자로 입력해 주세요.");
				time2exercise.Focus();
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

			time2exercise.Focus();
			MessageBox.Show("운동 기록이 저장(또는 수정)되었습니다.");
		}
		#endregion

		#region UI 업데이트 및 나이/카운트다운 관련 (Tab: 전체/타이머 관련)
		// 정기적인 UI 갱신에서 호출되는 메서드
		private void UpdateUi()
		{
			DateTime now = DateTime.Now;

			// 1) 태어난 날짜 표시
			lbl1time.Text = $"태어난 날짜: {BirthDate:yyyy년 M월 d일}";

			// 2) 살아온 시간(전체/의식/수면)
			TimeSpan livedTotal = now - BirthDate;
			TimeSpan livedAwake = GetPartSpan(livedTotal, AwakeHoursPerDay);
			TimeSpan livedSleep = GetPartSpan(livedTotal, SleepHoursPerDay);

			// lbl1time 한 줄이면 길어서 줄바꿈으로 표시(라벨 AutoSize면 잘 보임)
			lbl1time.Text =
					$"살아온 시간(전체): {FormatSpan(livedTotal)}\r\n" +
					$"의식 시간(16h/일): {FormatSpan(livedAwake)}\r\n" +
					$"수면 시간(8h/일): {FormatSpan(livedSleep)}";

			// 3) 목표 나이까지 남은 시간 (전체/의식/수면) -> 텍스트박스 3개씩 사용
			SetCountdown(txtbox1live30, txtbox1awake30, txtbox1sleep30, BirthDate.AddYears(30), now);
			SetCountdown(txtbox1live40, txtbox1awake40, txtbox1sleep40, BirthDate.AddYears(40), now);
			SetCountdown(txtbox1live60, txtbox1awake60, txtbox1sleep60, BirthDate.AddYears(60), now);
			SetCountdown(txtbox1live65, txtbox1awake65, txtbox1sleep65, BirthDate.AddYears(65), now);
		}

		/// <summary>
		/// diff(기간)에서 "하루 hoursPerDay 만큼"의 시간만 뽑아서 계산
		/// 예) hoursPerDay=16 => 의식 시간, 8 => 수면 시간
		/// </summary>
		private TimeSpan GetPartSpan(TimeSpan span, double hoursPerDay)
		{
			bool isNegative = span < TimeSpan.Zero;
			TimeSpan abs = isNegative ? span.Negate() : span;

			// 기간(일) * (하루 해당 시간)
			TimeSpan part = TimeSpan.FromHours(abs.TotalDays * hoursPerDay);

			// 방어코드: 음수 처리 방지(일반적으로 불필요하지만 안전하게)
			if (part < TimeSpan.Zero)
			{
				part = TimeSpan.Zero;
			}

			return isNegative ? part.Negate() : part;
		}

		// 목표 날짜까지 남은 전체/의식/수면 시간을 각 텍스트박스에 표시
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
		#endregion

		#region Task 그리드 로드 및 도우미 (DB SELECT -> DataGridView 바인딩)
		// 현재 선택된 탭에 따른 TaskType 및 대상 그리드 반환
		private (byte TaskType, DataGridView Grid) GetCurrentTaskGridInfo()
		{
			if (tabControl.SelectedTab == tabPage3)
				return (1, dgw3emergency);

			if (tabControl.SelectedTab == tabPage4)
				return (2, dgw4continue);

			if (tabControl.SelectedTab == tabPage5)
				return (3, dgwMind);

			return (0, null);
		}

		// 모든 Task 그리드를 한 번에 로드 (폼 초기화/리프레시용)
		private void LoadTaskGrids()
		{
			LoadTaskGrid(1, dgw3emergency); // 급한거
			LoadTaskGrid(2, dgw4continue); // 계속꾸준히해야하는거
			LoadTaskGrid(3, dgwMind); // 마인드
		}

		// 특정 TaskType에 해당하는 데이터를 DB에서 읽어와 그리드에 바인딩
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

				// 내부 식별자 컬럼은 화면에서 숨김
				if (targetGrid.Columns["TaskId"] != null)
					targetGrid.Columns["TaskId"].Visible = false;

				if (targetGrid.Columns["TaskType"] != null)
					targetGrid.Columns["TaskType"].Visible = false;

				// 순서 컬럼 고정 너비 및 정렬 잠금
				if (targetGrid.Columns["순서"] != null)
				{
					targetGrid.Columns["순서"].Width = 60;
					targetGrid.Columns["순서"].SortMode = DataGridViewColumnSortMode.NotSortable;
				}
			}
		}
		#endregion

		#region 기타 유틸 및 리소스 정리
		// TimeSpan을 사람이 보기 좋은 형태로 포맷 (일, 총시간, 시:분:초)
		private string FormatSpan(TimeSpan span)
		{
			long totalDays = (long)span.TotalDays;
			long totalHours = (long)span.TotalHours;

			return $"{totalDays:N0}일 (총 {totalHours:N0}시간) {span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}";
		}

		// 폼 종료 시 타이머 해제
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			uiTimer.Stop();
			uiTimer.Dispose();
			base.OnFormClosing(e);
		}
		#endregion

		#region 카테고리 콤보 바인딩 (UI 목적, 현재는 목록만 생성)
		private sealed class CategoryItem
		{
			public byte CategoryId { get; set; }
			public string CategoryName { get; set; } = "";
		}

		// 콤보 바인딩용 항목 생성 (필요시 comboBox에 바인딩하도록 확장 가능)
		private void BindCategoryCombo()
		{
			var items = new List<CategoryItem>
			{
				new CategoryItem { CategoryId = 1, CategoryName = "급한거" },
				new CategoryItem { CategoryId = 2, CategoryName = "계속꾸준히해야하는거" },
				new CategoryItem { CategoryId = 3, CategoryName = "마인드" }
			};
		}
		#endregion

		#region Tab: 자야하는시간설정 관련 (dateTimePicker -> 남은시간 표시)
		// dateTimePicker 변화시 또는 타이머로 주기적으로 호출되어 남은 시간을 textBox13에 표시
		private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			UpdateRemainingToPicker(); // ✅ 사용자가 시간 바꾸면 즉시 반영
		}

		/// <summary>
		/// Tab : 자야하는시간설정
		/// dateTimePicker1에 설정된 시간과 현재 시간의 차이를 textBox13에 표시
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
		#endregion
    }
}
