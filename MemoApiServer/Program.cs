using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 테스트용 간단 API
app.MapGet("/api/hello", () => "폭탄을 설치하겠습니다!");
app.MapGet("/api/hello2", () => "폭탄이 해제되었습니다");
app.MapGet("/a/hello2", () => "폭탄이 해제되었습니다aa");

app.Run();
