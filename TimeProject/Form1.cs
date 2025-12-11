using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TimeProject
{
    public partial class Form1 : Form
    {
		private readonly string connectionString =@"Server=localhost;Database=TimeDB;Integrated Security=True;";
		public Form1()
        {
            InitializeComponent();
			this.Load += Form1_Load;
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			// 날짜 기본값 오늘
			Date.Value = DateTime.Today;
			Datefrtime.Value = DateTime.Today;
			Dateendtime.Value = DateTime.Today;

			// Activity 콤보박스 채우기
			LoadActivityCombo();
		}
		private void LoadActivityCombo()
		{
			string sql = "SELECT Id, CategoryName FROM Activity ORDER BY CategoryName;";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				DataTable table = new DataTable();
				connection.Open();

				using (SqlDataReader reader = command.ExecuteReader())
				{
					table.Load(reader);
				}

				cboactivity.DisplayMember = "CategoryName"; // 화면에 보이는 값
				cboactivity.ValueMember = "Id";             // 실제 선택되는 값
				cboactivity.DataSource = table;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			// 1) 기본 유효성 체크 -----------------------------
			if (cboactivity.SelectedValue == null)
			{
				MessageBox.Show("활동(카테고리)을 선택해 주세요.");
				return;
			}

			// 날짜
			DateTime date = Date.Value.Date;

			// 날짜 + 시간 합치기 (시작/종료)
			DateTime startTime = date.Date + Datefrtime.Value.TimeOfDay;
			DateTime endTime = date.Date + Dateendtime.Value.TimeOfDay;

			if (endTime <= startTime)
			{
				MessageBox.Show("끝나는 시간이 시작 시간보다 같거나 빠를 수 없습니다.");
				return;
			}

			int activityCategoryId = (int)cboactivity.SelectedValue;
			string txtremark = remark.Text;

			// 2) INSERT 쿼리 준비 -----------------------------

			string sql = @"
                INSERT INTO TimeLog
                    (Date, StartTime, EndTime, ActivityCategoryId, REMARK)
                VALUES
                    (@Date, @StartTime, @EndTime, @ActivityCategoryId, @Remark);
            ";

			// 3) DB 연결 후 실행 -----------------------------

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@Date", date);
				command.Parameters.AddWithValue("@StartTime", startTime);
				command.Parameters.AddWithValue("@EndTime", endTime);
				command.Parameters.AddWithValue("@ActivityCategoryId", activityCategoryId);

				if (string.IsNullOrEmpty(remark.ToString()))
					command.Parameters.AddWithValue("@Remark", DBNull.Value);
				else
					command.Parameters.AddWithValue("@Remark", remark.Text.ToString());

				connection.Open();
				int rows = command.ExecuteNonQuery();

				MessageBox.Show($"{rows}개의 스케줄이 저장되었습니다.");
			}

			// 4) 입력칸 정리 (메모만 지우고 싶다면 여기서 처리) -----

			remark.Clear();

			// TODO: 위에 큰 DataGridView가 있으면 여기서 다시 조회 함수 호출하면 됨.
			// LoadTimeLogGrid(); 같은 함수 만들어서 호출해도 좋고.
		}

        private void buttonretrieve_Click(object sender, EventArgs e)
        {
			DateTime selectedDate = Date1.Value.Date;

			// 2) 해당 날짜의 TimeLog + Activity JOIN해서 가져오는 쿼리
			string sql = @"
						SELECT  
							T.Id,
							T.Date,
							T.StartTime,
							T.EndTime,
							A.CategoryName,
							A.ActivityType,
							T.REMARK
						FROM TimeLog T
						INNER JOIN Activity A
							    ON T.ActivityCategoryId = A.Id
						WHERE T.Date = @Date
						ORDER BY T.StartTime;
					";

			// 3) DB 연결해서 DataTable에 담기
			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@Date", selectedDate);

				DataTable table = new DataTable();

				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					table.Load(reader);
				}

				// 4) 그리드에 바인딩
				dataGridView1.DataSource = table;
				dataGridView1.AutoResizeColumns();

				if (dataGridView1.Columns["Id"] != null)
				{
					dataGridView1.Columns["Id"].Visible = false; // 화면에서는 안 보이게
				}
			}
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			// 1) 현재 선택된 행이 있는지 체크
			if (dataGridView1.CurrentRow == null)
			{
				MessageBox.Show("삭제할 행을 먼저 선택해 주세요.");
				return;
			}
				
			// 2) 선택된 행에서 TimeLog Id 가져오기
			//    (그리드에 Id 컬럼이 있다고 가정)
			object idValue = dataGridView1.CurrentRow.Cells["Id"].Value;

			if (idValue == null || idValue == DBNull.Value)
			{
				MessageBox.Show("선택한 행에서 Id 값을 찾을 수 없습니다.");
				return;
			}

			int timeLogId = Convert.ToInt32(idValue);

			// 3) 진짜 삭제할지 사용자에게 확인
			DialogResult result = MessageBox.Show(
				"선택한 스케줄을 삭제하시겠습니까?",
				"삭제 확인",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (result != DialogResult.Yes)
			{
				return; // 취소
			}

			// 4) DB에서 삭제
			string sql = "DELETE FROM TimeLog WHERE Id = @Id;";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@Id", timeLogId);

				connection.Open();
				int rows = command.ExecuteNonQuery();

				if (rows > 0)
				{
					MessageBox.Show("스케줄이 삭제되었습니다.");
				}
				else
				{
					MessageBox.Show("삭제할 데이터가 없습니다.");
				}
			}

			// 5) 그리드 새로고침
			//    (지금은 날짜 조회 버튼의 로직을 재사용하는 게 제일 간단)
			buttonretrieve_Click(null, EventArgs.Empty);
		}

	}
}