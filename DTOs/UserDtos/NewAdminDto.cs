namespace DTOs.UserDtos;

public class NewAdminDto
{
  public string FullName { get; set; } = string.Empty;

  public Gender Gender { get; set; }

  public string BirthDate { get; set; } = string.Empty;

  public string Address { get; set; } = string.Empty;

  public string PhoneNumber { get; set; } = string.Empty;

  public static implicit operator User(NewAdminDto newAdminDto)
      => new()
      {
        FullName = newAdminDto.FullName,
        Gender = newAdminDto.Gender,
        BirthDate = newAdminDto.BirthDate.ToDateTime(),
        Address = newAdminDto.Address,
        PhoneNumber = newAdminDto.PhoneNumber,
        UserName = newAdminDto.PhoneNumber
      };
}