namespace DTOs.UserDtos;

public class UpdateUserDto
{
  public string Id { get; set; } = string.Empty;

  public string PhoneNumber { get; set; } = string.Empty;

  public string FullName { get; set; } = string.Empty;

  public Gender Gender { get; set; }

  public string BirthDate { get; set; } = string.Empty;

  public string Address { get; set; } = string.Empty;

  public static implicit operator User(UpdateUserDto updateUserDto)
      => new()
      {
        Id = updateUserDto.Id,
        FullName = updateUserDto.FullName,
        PhoneNumber = updateUserDto.PhoneNumber,
        Address = updateUserDto.Address,
        BirthDate = updateUserDto.BirthDate.ToDateTime(),
        Gender = updateUserDto.Gender
      };
}