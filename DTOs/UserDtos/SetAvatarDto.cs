namespace DTOs.UserDtos;

//public record SetAvatarDto(string UserId, string ImageUrl);
public class SetAvatarDto()
{
  public string UserId { get; set; } = string.Empty;

  public string ImageUrl { get; set; } = string.Empty;
}