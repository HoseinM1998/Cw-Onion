using Hw14.Contracts;
using Hw14.Entities;
using Hw14.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw14.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public string GenerateVerificationCode(int userId, string fullName)
        {
            var existingVerification = _userRepository.GetVerificationDataById(userId);

            if (existingVerification != null && existingVerification.DateVerificationCode > DateTime.Now)
            {
                return "Code Already Sent";
            }

            var random = new Random();
            int verificationCode = random.Next(10000, 99999);
            DateTime expirationTime = DateTime.Now.AddMinutes(5);
            _userRepository.GenerateAndSaveVerificationCode(userId, fullName, verificationCode, expirationTime);
            return $"Code Send";
        }

        public bool ValidateVerificationCode(int userId, string fullName, int verificationCode)
        {
            var verificationDto = _userRepository.GetVerificationDataById(userId);
            if (verificationDto != null &&
                verificationDto.FullName == fullName &&
                verificationDto.VerificationCode == verificationCode)
            {
                if (verificationDto.DateVerificationCode > DateTime.Now)
                {
                    verificationDto.DateVerificationCode = DateTime.Now.AddMinutes(-1);
                    _userRepository.SaveVerificationData(verificationDto);
                    return true;
                }
            }
            return false;
        }
    }
}


