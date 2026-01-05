using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TimeCalculationProject
{
	public partial class Form1 : Form
	{
		#region 필드 (공통)
		// 기준 생일
		private static readonly DateTime BirthDate = new DateTime(1997, 8, 30, 0, 0, 0);

		// 하루 기준(가정)
		private const double SleepHoursPerDay = 8.0;
		private const double AwakeHoursPerDay = 16.0; // 24 - 8

		// UI 갱신 타이머
		private readonly Timer uiTimer;

		// DB 연결 문자열
		private readonly string connectionString = @"Server=localhost;Database=TimeDB;Integrated Security=True;";
		#endregion
		// 운동 시작 시간(버튼 Start 눌렀을 때 저장)
		private DateTime? exerciseStartTime;


		#region 생성자 및 공통 이벤트 연결
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
			UpdateRemainingToPicker(); // 처음 실행 시에도 표시

			// 탭/타이머/타임피커 이벤트 연결
			tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
			dateTimePicker6.ValueChanged += dateTimePicker6_ValueChanged;

			// Task(Insert/Delete) 버튼들(디자이너에서 변경된 이름 기반)
			btn3insertemergency.Click += BtnInsertEmergency_Click;
			btn4insertcontinue.Click += BtnInsertContinue_Click;
			btn5insertmind.Click += BtnInsertMind_Click;

			btn3deletemergency.Click += BtnDeleteEmergency_Click;
			btn4deletecontinue.Click += BtnDeleteContinue_Click;
			btn5deletemind.Click += BtnDeleteMind_Click;

			// 자주 쓰는 시간 버튼 - Tag에 분 저장
			btn611.Tag = 0;
			btn61110.Tag = 10;
			btn61120.Tag = 20;
			btn61130.Tag = 30;

			btn611.Click += TimeButton_Click;
			btn61110.Click += TimeButton_Click;
			btn61120.Click += TimeButton_Click;
			btn61130.Click += TimeButton_Click;

			btn2start.Click += Btn2Start_Click;
			btn2finish.Click += Btn2Finish_Click;
		}
		#endregion

		#region Tab1 (시간) - UI 업데이트 및 카운트다운 표시
		private void UiTimer_Tick(object sender, EventArgs e)
		{
			TabPage selectedTab = tabControl.SelectedTab;
			if (selectedTab == null)
				return;

			// ✅ 탭1일 때만 탭1 UI 갱신
			if (selectedTab == tabPage1)
			{
				UpdateUi();
				return;
			}

			// ✅ 탭6일 때만 탭6 UI 갱신
			if (selectedTab == tabPage6)
			{
				UpdateRemainingToPicker();
				return;
			}

			// 탭2~5는 타이머로 아무것도 갱신 안 함
		}


		private void UpdateUi()
		{
			DateTime now = DateTime.Now;

			// 태어난 날짜(단일 라벨)
			lbl1birth.Text = $"{BirthDate:yyyy년 M월 d일}";

			// 살아온 시간(전체 / 의식 / 수면)
			TimeSpan livedTotal = now - BirthDate;
			TimeSpan livedAwake = GetPartSpan(livedTotal, AwakeHoursPerDay);
			TimeSpan livedSleep = GetPartSpan(livedTotal, SleepHoursPerDay);

			// 살아온 시간 멀티라인 표시
			lbl1time.Text =
					$"살아온 시간(전체): {FormatSpan(livedTotal)}\r\n" +
					$"의식 시간(16h/일): {FormatSpan(livedAwake)}\r\n" +
					$"수면 시간(8h/일): {FormatSpan(livedSleep)}";

			// 목표 나이별 카운트다운 (텍스트박스 그룹)
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
		#endregion

		#region Tab2 (운동) - 운동 데이터 로드 / 등록
		// 운동 탭: DB에서 운동 기록 로드하여 그리드 바인딩
		private void LoadExerciseGrid()
		{
			const string sql = @"
									SELECT
										ExerciseDate,
										seq,
										ExerciseMinutes,
										InsertTime,
										UpdateTime
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

		// 운동 등록/수정 버튼 핸들러
		private void btn2insertexercise_Click(object sender, EventArgs e)
		{
			// 1) 날짜
			DateTime exerciseDate = Date2exercise.Value.Date;

			// 2) 분(ExerciseMinutes) - 디자이너에서 Text 컨트롤로 구성되어 있으면 Text 사용
			string minutesText = time2exercise.Text.Trim();
			if (!int.TryParse(minutesText, out int exerciseMinutes) || exerciseMinutes < 0 || exerciseMinutes > 1440)
			{
				MessageBox.Show("운동 시간(분)을 0~1440 사이 숫자로 입력해 주세요.");
				time2exercise.Focus();
				return;
			}

			// 3) InsertTime
			DateTime insertTime = DateTime.Now;

			// 4) 같은 날짜가 있으면 UPDATE, 없으면 INSERT
			const string sql = @"
								DECLARE @NextSeq NUMERIC(18,0);

								SELECT @NextSeq = ISNULL(MAX(seq), 0) + 1
								FROM dbo.Exercise WITH (UPDLOCK, HOLDLOCK)
								WHERE ExerciseDate = @ExerciseDate;

								INSERT INTO dbo.Exercise (ExerciseDate, seq, ExerciseMinutes, InsertTime)
								VALUES (@ExerciseDate, @NextSeq, @ExerciseMinutes, @InsertTime);
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
		private void Btn2Start_Click(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;

			// 이미 시작한 상태면 덮어쓸지 물어보기(원치 않으면 제거해도 됨)
			if (exerciseStartTime.HasValue)
			{
				DialogResult result = MessageBox.Show(
					"이미 시작 시간이 있습니다. 다시 시작하시겠습니까?",
					"확인",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);

				if (result != DialogResult.Yes)
					return;
			}

			exerciseStartTime = now;

			// 화면에 시작시간 표시 (txttime2가 TextBox라고 가정)
			txttime2.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
		}

		private void Btn2Finish_Click(object sender, EventArgs e)
		{
			if (!exerciseStartTime.HasValue)
			{
				MessageBox.Show("먼저 시작(btn2start)을 눌러주세요.");
				return;
			}

			DateTime startTime = exerciseStartTime.Value;
			DateTime finishTime = DateTime.Now;

			TimeSpan diff = finishTime - startTime;
			if (diff < TimeSpan.Zero)
			{
				MessageBox.Show("시간 계산이 이상합니다.");
				return;
			}

			// 분으로 변환 (원하면 Math.Floor로 내림 처리도 가능)
			int exerciseMinutes = (int)Math.Round(diff.TotalMinutes);

			// 0~1440 안전장치
			if (exerciseMinutes < 0) exerciseMinutes = 0;
			if (exerciseMinutes > 1440) exerciseMinutes = 1440;

			// 날짜는 시작한 날짜 기준(원하면 Date2exercise.Value.Date로 바꿔도 됨)
			DateTime exerciseDate = startTime.Date;

			// 업서트 저장
			InsertExercise(exerciseDate, exerciseMinutes);

			// UI 표시도 같이
			time2exercise.Text = exerciseMinutes.ToString();
			txttime2.Text = $"시작: {startTime:HH:mm:ss} / 종료: {finishTime:HH:mm:ss} / {exerciseMinutes}분";

			// 그리드 갱신
			LoadExerciseGrid();

			// 다음 세션 대비 초기화
			exerciseStartTime = null;

			MessageBox.Show("운동 기록이 저장(또는 수정)되었습니다.");
		}
		private void InsertExercise(DateTime exerciseDate, int exerciseMinutes)
		{
			DateTime insertTime = DateTime.Now;

			const string sql = @"
									INSERT INTO dbo.Exercise (ExerciseDate, ExerciseMinutes, InsertTime)
									VALUES (@ExerciseDate, @ExerciseMinutes, @InsertTime);
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
		}

		private void btn2delete_Click(object sender, EventArgs e)
		{
			// 1) 포커스(현재) 행 찾기
			DataGridViewRow row = dgw2exercise.SelectedRows.Count > 0
				? dgw2exercise.SelectedRows[0]
				: dgw2exercise.CurrentRow;

			if (row == null)
			{
				MessageBox.Show("삭제할 행을 먼저 선택해 주세요.");
				return;
			}

			// 2) 고유키(ExerciseId) 가져오기
			object idValue = row.Cells["ExerciseDate"]?.Value;
			if (idValue == null || idValue == DBNull.Value)
			{
				MessageBox.Show("선택한 행에서 ExerciseId를 찾을 수 없습니다.");
				return;
			}

			int exerciseId = Convert.ToInt32(idValue);

			// 3) 삭제 확인
			DialogResult result = MessageBox.Show(
				"선택한 운동 기록을 삭제하시겠습니까?",
				"삭제 확인",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (result != DialogResult.Yes)
				return;

			// 4) DB 삭제
			const string sql = "DELETE FROM dbo.Exercise WHERE ExerciseId = @ExerciseId;";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.Add("@ExerciseId", SqlDbType.Int).Value = exerciseId;

				connection.Open();
				int rows = command.ExecuteNonQuery();

				if (rows <= 0)
				{
					MessageBox.Show("삭제할 데이터가 없습니다.");
					return;
				}
			}

			// 5) 그리드 갱신
			LoadExerciseGrid();
			MessageBox.Show("삭제되었습니다.");
		}


		#endregion

		#region Tab3 (급한거) - 탭 진입점 및 컨트롤 연결
		private void BtnInsertEmergency_Click(object sender, EventArgs e)
		{
			InsertTask(1, txt3emergency, dgw3emergency);
		}

		private void BtnDeleteEmergency_Click(object sender, EventArgs e)
		{
			DeleteTask(1, dgw3emergency);
		}
		#endregion

		#region Tab4 (계속꾸준히) - 탭 진입점 및 컨트롤 연결
		private void BtnInsertContinue_Click(object sender, EventArgs e)
		{
			InsertTask(2, txt4continune, dgw4continue);
		}

		private void BtnDeleteContinue_Click(object sender, EventArgs e)
		{
			DeleteTask(2, dgw4continue);
		}
		#endregion

		#region Tab5 (마인드) - 탭 진입점 및 컨트롤 연결
		private void BtnInsertMind_Click(object sender, EventArgs e)
		{
			InsertTask(3, txt5Mind, dgw5Mind);
		}

		private void BtnDeleteMind_Click(object sender, EventArgs e)
		{
			DeleteTask(3, dgw5Mind);
		}
		#endregion

		#region Tab6 (자야하는시간설정) - 시간 선택 버튼 및 남은시간 표시
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
			DateTime baseDate = dateTimePicker6.Value.Date; // 날짜 유지
			dateTimePicker6.Value = new DateTime(
				baseDate.Year, baseDate.Month, baseDate.Day,
				hour24, minute, 0);
		}

		// dateTimePicker 값 변경 또는 타이머에서 호출
		private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
		{
			UpdateRemainingToPicker();
		}

		// dateTimePicker와 현재시간 차이를 text에 표시 (Tab6)
		private void UpdateRemainingToPicker()
		{
			DateTime target = dateTimePicker6.Value;
			DateTime now = DateTime.Now;

			TimeSpan diff = target - now;

			if (diff >= TimeSpan.Zero)
			{
				txt6time.Text = $"남음: {FormatSpan(diff)}";
			}
			else
			{
				txt6time.Text = $"지남: {FormatSpan(diff.Negate())}";
			}
		}
		#endregion

		#region Task 공통 (Insert/Delete/Load 공통 구현)
		// 공통 Insert 구현 (DB INSERT 및 그리드 갱신)
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

		// 공통 Delete 구현 (선택 행의 TaskId 사용)
		private void DeleteTask(byte taskType, DataGridView targetGrid)
		{
			if (targetGrid == null)
				return;

			DataGridViewRow row = targetGrid.SelectedRows.Count > 0
				? targetGrid.SelectedRows[0]
				: targetGrid.CurrentRow;

			if (row == null)
			{
				MessageBox.Show("삭제할 행을 먼저 선택해 주세요.");
				return;
			}

			object idValue = row.Cells["TaskId"]?.Value;
			if (idValue == null || idValue == DBNull.Value)
			{
				MessageBox.Show("선택한 행에서 TaskId를 찾을 수 없습니다.");
				return;
			}

			int taskId = Convert.ToInt32(idValue);

			DialogResult result = MessageBox.Show(
				"선택한 데이터를 삭제하시겠습니까?",
				"삭제 확인",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (result != DialogResult.Yes)
				return;

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

			// 삭제 후 그리드 갱신
			LoadTaskGrid(taskType, targetGrid);

			MessageBox.Show("삭제되었습니다.");
		}
		#endregion

		#region Task 그리드 로드 / 탭 변경 처리 (공통)
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
				LoadTaskGrid(1, dgw3emergency);
			}
			else if (selectedTab == tabPage4)
			{
				LoadTaskGrid(2, dgw4continue);
			}
			else if (selectedTab == tabPage5)
			{
				LoadTaskGrid(3, dgw5Mind);
			}
		}

		// 특정 TaskType 데이터를 DB에서 읽어와 그리드 바인딩
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
				targetGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

				if (targetGrid.Columns["TaskId"] != null)
					targetGrid.Columns["TaskId"].Visible = false;

				if (targetGrid.Columns["TaskType"] != null)
					targetGrid.Columns["TaskType"].Visible = false;

				if (targetGrid.Columns["순서"] != null)
				{
					targetGrid.Columns["순서"].Width = 60;
					targetGrid.Columns["순서"].SortMode = DataGridViewColumnSortMode.NotSortable;
				}
			}
		}

		private void LoadTaskGrids()
		{
			LoadTaskGrid(1, dgw3emergency);
			LoadTaskGrid(2, dgw4continue);
			LoadTaskGrid(3, dgw5Mind);
		}
		#endregion

		#region 공통 유틸리티 / 리소스 해제
		/// <summary>
		/// 기간에서 하루 중 hoursPerDay 만큼의 시간만 계산(ex: 16 -> 의식, 8 -> 수면)
		/// </summary>
		private TimeSpan GetPartSpan(TimeSpan span, double hoursPerDay)
		{
			bool isNegative = span < TimeSpan.Zero;
			TimeSpan abs = isNegative ? span.Negate() : span;

			TimeSpan part = TimeSpan.FromHours(abs.TotalDays * hoursPerDay);

			if (part < TimeSpan.Zero)
			{
				part = TimeSpan.Zero;
			}

			return isNegative ? part.Negate() : part;
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
		#endregion

		#region 카테고리 콤보 바인딩 (UI 목적)
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
        #endregion
    }
}
