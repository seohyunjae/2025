using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
            LoadDataAsync(); // 폼 로드시 API 데이터 가져오기
        }

        private async void LoadDataAsync()
        {
            try
            {
                string apiUrl = "https://jsonplaceholder.typicode.com/users";
                string json = await client.GetStringAsync(apiUrl);

                List<User> users = JsonSerializer.Deserialize<List<User>>(json);

                // DataGridView에 바인딩
                dataGridView1.DataSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ API 호출 실패: " + ex.Message);
            }
        }
    }

    // API에서 반환되는 JSON 구조에 맞춰 클래스 생성
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
