using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using stockrank;

namespace stock
{
	public partial class Form1 : Form
	{
		// ★ 실제 발급받은 Access Token 넣기
		private const string AccessToken = "";

		public Form1()
		{
			InitializeComponent();
			this.button1.Click += new System.EventHandler(this.button1_Click);

		}

		// 버튼 클릭 시 실행
		private async void button1_Click(object sender, EventArgs e)
		{
			try
			{
				// 1. REST 호출해서 순위 데이터 가져오기
				List<RankItem> rankList = await GetRankListAsync();

				// 2. DataGridView에 바인딩
				dataGridView1.DataSource = rankList;

				// 보기 좋게 컬럼 헤더만 한글로 바꿔주기 (선택)
				dataGridView1.Columns[nameof(RankItem.StkCd)].HeaderText = "종목코드";
				dataGridView1.Columns[nameof(RankItem.StkNm)].HeaderText = "종목명";
				dataGridView1.Columns[nameof(RankItem.CurPrc)].HeaderText = "현재가";
				dataGridView1.Columns[nameof(RankItem.PredPreSig)].HeaderText = "전일대비";
				dataGridView1.Columns[nameof(RankItem.TrdeQty)].HeaderText = "거래량";
				dataGridView1.Columns[nameof(RankItem.NetprpsReq)].HeaderText = "순매수잔량";
				dataGridView1.Columns[nameof(RankItem.BuyRt)].HeaderText = "매수비율";
			}
			catch (Exception ex)
			{
				MessageBox.Show("조회 중 오류 : " + ex.Message);
			}
		}

		// 실제로 Kiwoom REST API 부르는 함수
		private async Task<List<RankItem>> GetRankListAsync()
		{
			string host = "https://mockapi.kiwoom.com";       
			// 모의투자
			//string host = "https://api.kiwoom.com";          // 실전투자 쓸 때
			string endpoint = "/api/dostk/rkinfo";
			string url = host + endpoint;

			// 1. body 파라미터 (문서 예제 그대로)
			var bodyObj = new
			{
				mrkt_tp = "001",     // 코스피
				sort_tp = "1",       // 1:순매수잔량순
				trde_qty_tp = "0000",// 0주 이상
				stk_cnd = "0",       // 전체조회
				crd_cnd = "0",       // 신용조건 전체
				stex_tp = "1"        // 1:KRX
			};

			string jsonBody = JsonConvert.SerializeObject(bodyObj);

			using (var httpClient = new HttpClient())
			{
				// 2. 헤더 세팅
				httpClient.DefaultRequestHeaders.Clear();
				httpClient.DefaultRequestHeaders.Add("authorization", $"Bearer {AccessToken}");
				httpClient.DefaultRequestHeaders.Add("cont-yn", "N");
				httpClient.DefaultRequestHeaders.Add("next-key", "");
				httpClient.DefaultRequestHeaders.Add("api-id", "ka10020"); // 호가잔량상위요청

				// 3. POST 전송
				var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await httpClient.PostAsync(url, content);

				string responseJson = await response.Content.ReadAsStringAsync();

				// 4. 상태 코드 체크
				if (!response.IsSuccessStatusCode)
				{
					throw new Exception("HTTP 오류 : " + response.StatusCode + "\r\n" + responseJson);
				}

				// 5. JSON → C# 객체로 변환
				RankResponse rankResponse = JsonConvert.DeserializeObject<RankResponse>(responseJson);

				if (rankResponse == null)
				{
					throw new Exception("응답 파싱 실패");
				}

				if (rankResponse.ReturnCode != 0)
				{
					throw new Exception("API 오류 : " + rankResponse.ReturnMsg);
				}

				// 6. 실제 리스트 반환 (없으면 빈 리스트)
				return rankResponse.BidReqUpper ?? new List<RankItem>();
			}
		}
	}
}
