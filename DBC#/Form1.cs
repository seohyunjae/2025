using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBC_
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
            this.button1.Click += button_Click;
            this.textBox1.KeyDown += textBox1_KeyDown;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadStudentsData();
        }

        private void LoadStudentsData()
        {
            // 📌 1. 연결 문자열 - 서버이름, DB이름 확인
            string connectionString = "Server=DESKTOP-E9JTTS5\\SQLEXPRESS;Database=school;Integrated Security=True";

            // 📌 2. 쿼리
            string query = "SELECT * FROM Students ORDER BY BirthDate";

            // 📌 3. 연결 및 데이터 로드
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // 📌 4. DataGridView에 바인딩
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("데이터 로딩 중 오류 발생: " + ex.Message);
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            string userQuery = textBox1.Text.Trim();  // textBox에 작성된 쿼리

            if (string.IsNullOrEmpty(userQuery))
            {
                MessageBox.Show("쿼리를 입력해주세요.");
                return;
            }

            string connectionString = "Server=DESKTOP-E9JTTS5\\SQLEXPRESS;Database=school;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(userQuery, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("쿼리 실행 중 오류 발생: " + ex.Message);
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_Click(button1, EventArgs.Empty); // 버튼 클릭 이벤트 호출
                e.SuppressKeyPress = true; // Enter 입력 소리 및 추가 동작 방지
            }
        }
    }
}
