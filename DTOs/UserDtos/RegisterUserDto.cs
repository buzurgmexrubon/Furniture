namespace DTOs.UserDtos;

public class RegisterUserDto : LoginUserDto
{
    public string FullName { get; set; } = string.Empty;

    public Gender Gender { get; set;}

    public string BirthDate { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;


    public static explicit operator User(RegisterUserDto registerUserDto)
        => new()
        {
            FullName = registerUserDto.FullName,
            Gender = registerUserDto.Gender,
            BirthDate = registerUserDto.BirthDate.ToDateTime(),
            Address = registerUserDto.Address,
            PhoneNumber = registerUserDto.PhoneNumber
        };
}