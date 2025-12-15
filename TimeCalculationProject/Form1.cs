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
			
			tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
			btninsertemergency.Click += btninsert_Click; // ★ 버튼 이름이 button1일 때
			btndeletemergency.Click += Button5_Click; // ✅ 삭제 버튼
		}

		private void btninsert_Click(object sender, EventArgs e)
		{
			// 1) Content: textbox13
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
		private void TabPage_Enter(object sender, EventArgs e)
		{
			LoadTaskGrids();
		}

		private void UiTimer_Tick(object sender, EventArgs e)
		{
			UpdateUi();
		}
		private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadTaskGridBySelectedTab();
		}
		private void LoadTaskGridBySelectedTab()
		{
			TabPage selectedTab = tabControl1.SelectedTab;
			if (selectedTab == null)
				return;

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
			if (tabControl1.SelectedTab == tabPage3)
				return (1,dgwemergency);

			if (tabControl1.SelectedTab == tabPage4)
				return (2, dgwcontinue);

			if (tabControl1.SelectedTab == tabPage5)
				return (3, dgwMind);

			return (0, null);
		}


		private void Button5_Click(object sender, EventArgs e)
		{
			// 1) 현재 탭에 맞는 그리드/TaskType 선택
			(byte taskType, DataGridView targetGrid) = GetCurrentTaskGridInfo();
			if (targetGrid == null)
			{
				MessageBox.Show("삭제할 탭/그리드를 찾을 수 없습니다.");
				return;
			}

			// 2) 포커스(선택)된 행 확인
			DataGridViewRow row = targetGrid.SelectedRows.Count > 0
				? targetGrid.SelectedRows[0]
				: targetGrid.CurrentRow;

			if (row == null)
			{
				MessageBox.Show("삭제할 행을 먼저 선택해 주세요.");
				return;
			}

			// 3) TaskId 가져오기
			object idValue = row.Cells["TaskId"]?.Value;
			if (idValue == null || idValue == DBNull.Value)
			{
				MessageBox.Show("선택한 행에서 TaskId를 찾을 수 없습니다.");
				return;
			}

			int taskId = Convert.ToInt32(idValue);

			// 4) 삭제 확인
			DialogResult result = MessageBox.Show(
				"선택한 데이터를 삭제하시겠습니까?",
				"삭제 확인",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (result != DialogResult.Yes)
				return;

			// 5) DB 삭제
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

			// 6) 현재 탭 그리드만 새로고침
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

			//comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			//comboBox1.DisplayMember = nameof(CategoryItem.CategoryName);
			//comboBox1.ValueMember = nameof(CategoryItem.CategoryId);
			//comboBox1.DataSource = items;
		}
		
		//private byte GetSelectedCategoryId()
		//{
		//	return comboBox1.SelectedValue is byte v ? v : (byte)0;
		//}

		private void LoadTaskGrids()
		{
			LoadTaskGrid(1, dgwemergency); // 급한거
			LoadTaskGrid(2, dgwcontinue); // 계속꾸준히해야하는거
			LoadTaskGrid(3, dgwMind); // 마인드
		}

		private void LoadTaskGrid(byte taskType, DataGridView targetGrid)
		{
			const string sql = @"
									SELECT TaskId, TaskType, Content, InsertTime
									FROM dbo.TaskItem
									WHERE TaskType = @TaskType;
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
				{
					targetGrid.Columns["TaskId"].Visible = false;
				}

				if (targetGrid.Columns["TaskType"] != null)
				{
					targetGrid.Columns["TaskType"].Visible = false;
				}
			}
		}
    }
}
