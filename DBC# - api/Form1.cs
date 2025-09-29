using Newtonsoft.Json; // JSON 직렬화/역직렬화 라이브러리 (Newtonsoft)
using System;           // 기본 시스템 네임스페이스
using System.Collections.Generic; // 제네릭 컬렉션 사용 (List 등)
using System.Net.Http;  // HTTP 요청 처리용
using System.Threading.Tasks; // 비동기 처리
using System.Windows.Forms;   // WinForms UI 사용


namespace NexonAPIApp
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient httpClient = new HttpClient();

        // 🔑 API Key (네가 발급받은 키 넣기)
        private string apiKey = "test_9f5c10c4c5205ff65667dca9456c446bb87db359e4422f7fcdee194c562850c2efe8d04e6d233bd35cf2fabdeb93fb0d";
        public Form1()
        {
            InitializeComponent();
            //this.Load += MainForm_Load;
            this.button1.Click += button1_Click; // 버튼 이벤트 등록
        }

        //private async void MainForm_Load(object sender, EventArgs e) //기다림
        //{
        //    // ✅ 테스트할 캐릭터명과 월드명
        //    await LoadCharacterBasicAsync("3사단", "스카니아");
        //}

        /// <summary>
        /// 캐릭터 기본 정보를 불러와서 DataGridView에 출력
        /// </summary>
        private async Task LoadCharacterBasicAsync(string characterName, string worldName)
        {
            try
            {   
                // 1️⃣ OCID 조회
                string ocid = await GetOcidAsync(characterName, worldName);
                if (string.IsNullOrEmpty(ocid))
                {
                    MessageBox.Show("OCID를 가져올 수 없습니다.");
                    return;
                }

                // 2캐릭터 기본 정보 조회
                string basicUrl = $"https://open.api.nexon.com/maplestorym/v1/character/basic?ocid={ocid}";

                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("x-nxopen-api-key", apiKey);

                HttpResponseMessage response = await httpClient.GetAsync(basicUrl);
                string json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"기본 정보 조회 실패: {response.StatusCode}\n{json}");
                    return;
                }

                var basicInfo = JsonConvert.DeserializeObject<CharacterBasic>(json);

                if (basicInfo != null)
                {
                    // ✅ DataGridView에 표시 (리스트 형태로 바인딩)
                    dataGridView1.DataSource = new List<CharacterBasic> { basicInfo };
                }
                else
                {
                    MessageBox.Show("기본 정보를 불러오지 못했습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API 호출 오류: " + ex.Message);
            }
        }

        /// <summary>
        /// OCID를 조회하는 메서드
        /// </summary>
        private async Task<string> GetOcidAsync(string characterName, string worldName)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("x-nxopen-api-key", apiKey);

            string encodedName = Uri.EscapeDataString(characterName);
            string encodedWorld = Uri.EscapeDataString(worldName);

            string ocidUrl = $"https://open.api.nexon.com/maplestorym/v1/id?character_name={encodedName}&world_name={encodedWorld}";

            HttpResponseMessage response = await httpClient.GetAsync(ocidUrl);
            string json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show($"OCID 조회 실패: {response.StatusCode}\n{json}");
                return null;
            }

            var ocidResponse = JsonConvert.DeserializeObject<OcidResponse>(json);
            return ocidResponse?.ocid;  
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string characterName = textBoxCharacter.Text.Trim();
            string worldName = textBoxWorld.Text.Trim();

            if (string.IsNullOrEmpty(characterName))
            {
                MessageBox.Show("캐릭터명을 입력하세요.");
                return;
            }

            if (string.IsNullOrEmpty(worldName))
            {
                MessageBox.Show("월드명을 입력하세요.");
                return;
            }

            await LoadCharacterBasicAsync(characterName, worldName);
        }
    }

    // ✅ 모델 클래스들
    public class OcidResponse
    {
        public string ocid { get; set; }
    }

    public class CharacterBasic
    {
        [JsonProperty("character_name")]
        public string CharacterName { get; set; }

        [JsonProperty("world_name")]
        public string WorldName { get; set; }

        [JsonProperty("character_gender")]
        public string CharacterGender { get; set; }

        [JsonProperty("character_class")]
        public string CharacterClass { get; set; }

        [JsonProperty("character_level")]
        public string CharacterLevel { get; set; }

        [JsonProperty("character_exp")]
        public string CharacterExp { get; set; }

        [JsonProperty("character_guild_name")]
        public string CharacterGuildName { get; set; }
    }
}
