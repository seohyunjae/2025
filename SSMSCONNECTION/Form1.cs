using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SSMSCONNECTION
{
	public partial class Form1 : Form
	{
		private readonly string connectionString =
		 @"Server=localhost;Database=StockDB;Integrated Security=True;";
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			// 1) textBox1에 사용자가 쓴 SQL 문 가져오기
			string query = textBox1.Text;

			// 아무 것도 안 썼으면 그냥 리턴
			if (string.IsNullOrWhiteSpace(query))
			{
				MessageBox.Show("실행할 SQL을 입력해 주세요.");
				return;
			}

			try
			{
				// 2) DB 연결 만들기
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					// 3) SqlDataAdapter로 SELECT 결과를 DataTable에 담기
					using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
					{
						DataTable dataTable = new DataTable();

						connection.Open();      // DB 문 열기
						adapter.Fill(dataTable); // 쿼리 실행 + 결과 DataTable에 채우기

						// 4) dataGridView1에 결과 바인딩
						dataGridView1.DataSource = dataTable;
					}
				}
			}
			catch (Exception ex)
			{
				// 에러 나면 메시지로 보여주기 (SQL 문법 에러 등)
				MessageBox.Show(ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

	}
}
