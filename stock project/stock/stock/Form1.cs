using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// C# 데이터 모델 (API 응답을 역직렬화하기 위한 클래스)
namespace KiwoomStockFinder
{
	// 1. 접근 토큰 응답 모델 (au10001)
	public class TokenResponse
	{
		public string expires_dt { get; set; }
		public string token_type { get; set; } // "bearer"
		public string token { get; set; } // 실제 접근 토큰
		public int return_code { get; set; }
		public string return_msg { get; set; }
	}

	// 2. 일별 주가 데이터 모델 (ka10086 응답 내 데이터 항목)
	public class DailyPriceData
	{
		// 실제 API 필드명에 맞게 수정해야 합니다. (예: '종가', '일자')
		public string date { get; set; }
		// 종가는 문자열로 올 수 있으므로 파싱을 위해 string으로 처리 후 int/decimal로 변환합니다.
		public string close_price { get; set; }
		// 여기에 다른 필요한 필드를 추가하세요 (시가, 고가, 저가 등)
	}

	// 3. 일별 주가 요청 응답 모델 (ka10086)
	public class DailyPriceResponse
	{
		public int return_code { get; set; }
		public string return_msg { get; set; }
		// 일별 주가 데이터의 배열 (가장 최근 데이터가 배열의 앞쪽에 있다고 가정)
		public List<DailyPriceData> data { get; set; }
	}

	// 4. API 통신 및 로직을 처리하는 핵심 클래스
	public class KiwoomApiClient
	{
		private readonly HttpClient _httpClient;
		private const string RealHost = "https://api.kiwoom.com";
		private const string MockHost = "https://mockapi.kiwoom.com";
		private readonly string _appKey;
		private readonly string _secretKey;
		private string _accessToken;

		// 생성자: 앱키와 시크릿키를 받아 HttpClient 인스턴스를 생성합니다.
		public KiwoomApiClient(string appKey, string secretKey)
		{
			_httpClient = new HttpClient();
			_appKey = appKey;
			_secretKey = secretKey;
		}

		/// <summary>
		/// 1. 접근 토큰을 발급받아 내부 변수에 저장합니다. (au10001)
		/// </summary>
		public async Task<bool> RequestAccessTokenAsync()
		{
			Console.WriteLine("--- 1. 접근 토큰 발급 요청 중... ---");
			var url = $"{RealHost}/oauth2/token";

			// 요청 본문 데이터
			var requestBody = new
			{
				grant_type = "client_credentials",
				appkey = _appKey,
				secretkey = _secretKey
			};

			var jsonBody = JsonSerializer.Serialize(requestBody);
			var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

			try
			{
				var response = await _httpClient.PostAsync(url, content);
				var responseString = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseString);
					if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.token))
					{
						_accessToken = tokenResponse.token;
						Console.WriteLine($"토큰 발급 성공. 토큰: {_accessToken.Substring(0, 10)}...");
						return true;
					}
				}

				Console.WriteLine($"토큰 발급 실패: {response.StatusCode}. 응답: {responseString}");
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"토큰 발급 중 오류 발생: {ex.Message}");
				return false;
			}
		}

		/// <summary>
		/// 2. 특정 종목의 일별 주가 데이터를 조회합니다. (ka10086 가정)
		/// </summary>
		/// <param name="stk_cd">조회할 종목 코드</param>
		/// <returns>일별 주가 데이터 리스트</returns>
		private async Task<List<DailyPriceData>> GetDailyPriceAsync(string stk_cd)
		{
			if (string.IsNullOrEmpty(_accessToken))
			{
				Console.WriteLine("에러: 접근 토큰이 유효하지 않습니다.");
				return new List<DailyPriceData>();
			}

			// ka10086 API를 위한 가정된 URL 및 TR명
			var url = $"{RealHost}/api/dostk/dayprice"; // 가상의 일별 주가 엔드포인트
			var apiId = "ka10086"; // 일별주가요청 TR명

			// 요청 본문 데이터
			var requestBody = new { stk_cd = stk_cd };
			var jsonBody = JsonSerializer.Serialize(requestBody);
			var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

			// Header 설정 (토큰 포함)
			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Add("authorization", $"Bearer {_accessToken}");
			_httpClient.DefaultRequestHeaders.Add("api-id", apiId);
			_httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json;charset=UTF-8");

			try
			{
				var response = await _httpClient.PostAsync(url, content);
				var responseString = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					var priceResponse = JsonSerializer.Deserialize<DailyPriceResponse>(responseString);
					return priceResponse?.data ?? new List<DailyPriceData>();
				}
				// 실제 키움 API는 오류 코드를 응답 본문에 포함할 수 있습니다.
				// Console.WriteLine($"[{stk_cd}] 시세 조회 실패: {response.StatusCode}. 응답: {responseString}");
				return new List<DailyPriceData>();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[{stk_cd}] 시세 조회 중 오류 발생: {ex.Message}");
				return new List<DailyPriceData>();
			}
		}

		/// <summary>
		/// 3. 일별 주가 데이터를 바탕으로 3일 연속 상승했는지 판단합니다.
		/// </summary>
		/// <param name="prices">최신 데이터부터 정렬된 일별 주가 리스트</param>
		/// <returns>3일 연속 상승 여부</returns>
		private bool CheckThreeDayRise(List<DailyPriceData> prices)
		{
			if (prices.Count < 3) return false; // 데이터가 3일 미만

			// 종가를 숫자(int)로 변환
			var recentPrices = prices
				.Take(3)
				.Select(d => int.TryParse(d.close_price, out var price) ? price : (int?)null)
				.Where(p => p.HasValue)
				.Select(p => p.Value)
				.ToList();

			if (recentPrices.Count < 3) return false;

			// 로직: 오늘(0) > 어제(1) 이고, 어제(1) > 그제(2)
			// 가정: prices 리스트는 최신 날짜부터 과거 순으로 정렬되어 있습니다.
			bool day1VsDay2 = recentPrices[0] > recentPrices[1];
			bool day2VsDay3 = recentPrices[1] > recentPrices[2];

			return day1VsDay2 && day2VsDay3;
		}

		/// <summary>
		/// 4. 전체 프로세스를 실행하고 결과를 반환합니다.
		/// </summary>
		public async Task<List<string>> FindRisingStocksAsync()
		{
			if (!await RequestAccessTokenAsync())
			{
				return new List<string> { "접근 토큰 발급에 실패하여 작업을 진행할 수 없습니다." };
			}

			Console.WriteLine("--- 2. 전체 종목 리스트를 순회하며 시세 조회 (가정 데이터) ---");

			// NOTE: 실제로는 전체 종목 코드(TR)를 조회하는 API를 먼저 호출해야 합니다.
			// 여기서는 테스트를 위해 종목 리스트를 하드코딩합니다.
			var stockCodes = new Dictionary<string, string>
			{
				{"005930", "삼성전자"},
				{"000660", "SK하이닉스"},
				{"373220", "LG에너지솔루션"},
				{"017670", "SK텔레콤"}, // 가상으로 3일 상승 종목
            };

			var risingStocks = new List<string>();

			// 가상 데이터 (API가 실제로 데이터 대신 이 데이터를 반환한다고 가정)
			var dummyRisingData = new List<DailyPriceData>
			{
				new DailyPriceData { date = "20251121", close_price = "75000" },
				new DailyPriceData { date = "20251120", close_price = "74000" },
				new DailyPriceData { date = "20251119", close_price = "73000" }
			};

			var dummyFlatData = new List<DailyPriceData>
			{
				new DailyPriceData { date = "20251121", close_price = "10000" },
				new DailyPriceData { date = "20251120", close_price = "11000" }, // 하락
                new DailyPriceData { date = "20251119", close_price = "10500" }
			};

			foreach (var stock in stockCodes)
			{
				// 실제 API 호출 (GetDailyPriceAsync) 대신 더미 데이터를 사용
				// var dailyPrices = await GetDailyPriceAsync(stock.Key);

				List<DailyPriceData> dailyPrices = stock.Key == "017670" ? dummyRisingData : dummyFlatData;

				if (CheckThreeDayRise(dailyPrices))
				{
					risingStocks.Add($"[{stock.Key}] {stock.Value}");
					Console.WriteLine($"[성공] {stock.Value} - 3일 연속 상승 확인!");
				}
				else
				{
					Console.WriteLine($"[패스] {stock.Value} - 조건 미충족.");
				}
				await Task.Delay(100); // API 과부하 방지를 위한 지연
			}

			return risingStocks;
		}
	}

	// 메인 실행 클래스 (WinForms 환경에서는 Form의 버튼 클릭 이벤트로 대체됩니다.)
	public class Program
	{
		// TODO: 사용자 본인의 실제 앱키와 시크릿키를 입력하세요.
		private const string MY_APP_KEY = "YOUR_APP_KEY";
		private const string MY_SECRET_KEY = "YOUR_SECRET_KEY";

		public static async Task Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine("Kiwoom REST API를 이용한 3일 연속 상승 종목 탐색기");
			Console.WriteLine("-------------------------------------------------");

			if (MY_APP_KEY == "YOUR_APP_KEY")
			{
				Console.WriteLine("경고: 앱키/시크릿키를 실제 값으로 변경해야 합니다.");
				return;
			}

			var apiFinder = new KiwoomApiClient(MY_APP_KEY, MY_SECRET_KEY);
			var results = await apiFinder.FindRisingStocksAsync();

			Console.WriteLine("\n\n=============== 🌟 최종 결과 🌟 ===============");
			if (results.Any())
			{
				Console.WriteLine("✅ 3일 연속 상승 종목:");
				foreach (var stock in results)
				{
					Console.WriteLine($"- {stock}");
				}
			}
			else
			{
				Console.WriteLine("❌ 3일 연속 상승한 종목을 찾지 못했습니다.");
			}
			Console.WriteLine("=================================================");
		}
	}
}