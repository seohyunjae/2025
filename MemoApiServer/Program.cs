using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

// ---------------------
// 2) 웹 애플리케이션 설정
// ---------------------
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// 4) 개발 환경일 때 Swagger 미들웨어 사용
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
	
// ---------------------
// 3) 기존 테스트용 문자열 API
// ---------------------
app.MapGet("/api/hello", () => "폭탄을 설치하겠습니다!");
app.MapGet("/api/hello2", () => "폭탄이 해제되었습니다");
app.MapGet("/a/hello2", () => "폭탄이 해제되었습니다aa");

// ---------------------
// 4) 메모를 위한 "가짜 DB" (메모리 List)
// ---------------------
List<Memo> memoList = new List<Memo>();
int nextId = 1;    // 새 메모가 추가될 때마다 1씩 증가

// ---------------------
// 4-1) 전체 메모 목록 조회
//      GET /api/memos
// ---------------------
app.MapGet("/api/memos", () =>
{
	// memoList 전체를 JSON으로 반환
	return memoList;
});

// ---------------------
// 4-2) 특정 메모 하나 조회
//      GET /api/memos/{id}
// ---------------------
app.MapGet("/api/memos/{id}", (int id) =>
{
	// id가 같은 메모 하나 찾기
	Memo memo = memoList.FirstOrDefault(m => m.Id == id);

	if (memo == null)
	{
		// 404 Not Found
		return Results.NotFound();
	}

	// 200 OK + memo JSON
	return Results.Ok(memo);
});

// ---------------------
// 4-3) 메모 추가
//      POST /api/memos
//      Body(JSON):
//      { "title": "제목", "content": "내용" }
// ---------------------
app.MapPost("/api/memos", (MemoCreateDto dto) =>
{
	// 새 메모 객체 생성
	Memo memo = new Memo
	{
		Id = nextId,
		Title = dto.Title,
		Content = dto.Content
	};

	nextId++;               // 다음에 또 추가될 때를 위해 증가
	memoList.Add(memo);     // 리스트에 추가

	// 201 Created + 생성된 메모를 JSON으로 반환
	return Results.Created($"/api/memos/{memo.Id}", memo);
});

// ---------------------
// 4-4) 메모 삭제
//      DELETE /api/memos/{id}
// ---------------------
app.MapDelete("/api/memos/{id}", (int id) =>
{
	Memo memo = memoList.FirstOrDefault(m => m.Id == id);

	if (memo == null)
	{
		return Results.NotFound();
	}

	memoList.Remove(memo);

	// 204 No Content (응답 바디 없음)
	return Results.NoContent();
});

// ---------------------
// 5) 서버 실행
// ---------------------
app.Run();
