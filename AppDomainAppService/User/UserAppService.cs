using AppDomainCore.Contract.User;
using AppDomainService.User;

namespace AppDomainAppService.User
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;

        public UserAppService(IUserService userService)
        {
            _userService = userService;
        }


        public string GenerateVerificationCode(int userId, string fullName)
        {
            var existingVerification = _userService.GetVerificationDataById(userId);

            if (existingVerification != null && existingVerification.DateVerificationCode > DateTime.Now)
            {
                return "Code Already Sent";
            }

            var random = new Random();
            int verificationCode = random.Next(10000, 99999);
            DateTime expirationTime = DateTime.Now.AddMinutes(5);

            _userService.GenerateAndSaveVerificationCode(userId, fullName, verificationCode, expirationTime);

            return "Code Sent";
        }
        public bool ValidateVerificationCode(int userId, string fullName, int verificationCode)
        {
            var verificationDto = _userService.GetVerificationDataById(userId);
            if (verificationDto != null &&
                verificationDto.FullName == fullName &&
                verificationDto.VerificationCode == verificationCode)
            {
                _userService.ValidateVerificationCode(userId, fullName, verificationCode);
                return true;

            }
            return false;
        }
    }
}
