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
