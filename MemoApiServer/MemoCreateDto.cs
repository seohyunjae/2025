// POST로 받을 때 쓸 DTO (Id는 자동으로 부여)
public class MemoCreateDto
{
	public string Title { get; set; } = "";
	public string Content { get; set; } = "";
}