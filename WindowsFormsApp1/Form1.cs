using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // 생성자
        public Form1()
        {
            InitializeComponent();
            LoadData();  // 폼 로드시 데이터 로드
        }

        // 데이터 로드 함수
        private void LoadData()
        {
            // SQL Server 연결 문자열
            string connectionString = @"Server=.\SQLEXPRESS;Database=school;Trusted_Connection=True;";
            string query = "SELECT * FROM tblStudent";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // 결과를 DataGridView에 바인딩
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ 오류: " + ex.Message);
                }
            }
        }
    }
}
