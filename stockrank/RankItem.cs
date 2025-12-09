using System.Collections.Generic;
using Newtonsoft.Json;

namespace stockrank   // ← 여기 이름을 네 Form1.cs 위에 있는 네임스페이스랑 똑같이!
{
	// 순위 한 줄(한 종목)을 담는 클래스
	public class RankItem
	{
		[JsonProperty("stk_cd")]
		public string StkCd { get; set; }      // 종목코드

		[JsonProperty("stk_nm")]
		public string StkNm { get; set; }      // 종목명

		[JsonProperty("cur_prc")]
		public string CurPrc { get; set; }     // 현재가

		[JsonProperty("pred_pre_sig")]
		public string PredPreSig { get; set; } // 전일대비 기호

		[JsonProperty("pred_pre")]
		public string PredPre { get; set; }    // 전일대비

		[JsonProperty("trde_qty")]
		public string TrdeQty { get; set; }    // 거래량

		[JsonProperty("tot_sel_req")]
		public string TotSelReq { get; set; }  // 총매도잔량

		[JsonProperty("tot_buy_req")]
		public string TotBuyReq { get; set; }  // 총매수잔량

		[JsonProperty("netprps_req")]
		public string NetprpsReq { get; set; } // 순매수잔량

		[JsonProperty("buy_rt")]
		public string BuyRt { get; set; }      // 매수비율
	}

	// 전체 응답(JSON)을 감싸는 클래스
	public class RankResponse
	{
		[JsonProperty("bid_req_upper")]
		public List<RankItem> BidReqUpper { get; set; }

		[JsonProperty("return_code")]
		public int ReturnCode { get; set; }

		[JsonProperty("return_msg")]
		public string ReturnMsg { get; set; }
	}
}
