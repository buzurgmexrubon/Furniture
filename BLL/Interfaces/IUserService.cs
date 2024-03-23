namespace BLL.Interfaces;

public interface IUserService
{
  Task CreateAsync(RegisterUserDto dto, string role);
  Task<LoginResult> LoginAsync(LoginUserDto dto);
  Task DeleteAccountAsync(LoginUserDto dto);
  Task LogoutAsync(LoginUserDto dto);
  Task ChangePasswordAsync(ChangePasswordDto dto);
  Task ConfirmPhoneNumberAsync(ConfirmPhoneNumberDto dto);
  Task SendOtpAsync(SendOtpDto dto);
  Task SetProfilePictureAsync(SetAvatarDto avatarDto);
  Task UpdateProfileImageAsync(SetAvatarDto avatarDto);
  Task DeleteProfilePictureAsync(string userId);

  Task<UserDto> GetUserAsync(string userId);
  Task<List<UserDto>> GetUsersAsync(string role);
  Task CreateAdminAsync(NewAdminDto dto);
  Task ActivateAdminAsync(string userId);
  Task DeleteUserAsync(string userId);
  Task UpdateUserAsync(UpdateUserDto dto);
  Task ChangePhoneNumber(string userId, string phoneNumber);
  Task ResetPassword(string userId);
}