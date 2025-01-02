using AppDomainCore.Contract.User;
using AppDomainCore.Dto;
using Repository.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainService.User
{
    public class UserService : IUserService
    {
        private readonly IUsreRepository _userRepository;

        public UserService(IUsreRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void GenerateAndSaveVerificationCode(int userId, string fullName, int verificationCode, DateTime expirationTime)
        {
            var existingVerification = _userRepository.GetVerificationDataById(userId);

            if (existingVerification != null && existingVerification.DateVerificationCode > DateTime.Now)
            {
                throw new Exception("Code Already Sent");
            }

            _userRepository.GenerateAndSaveVerificationCode(userId, fullName, verificationCode, expirationTime);
        }

        public VerificationDto GetVerificationDataById(int userId)
        {
            return _userRepository.GetVerificationDataById(userId);
        }

        public bool ValidateVerificationCode(int userId, string fullName, int verificationCode)
        {
            var verificationDto = _userRepository.GetVerificationDataById(userId);

            if (verificationDto == null)
            {
                return false;
            }

            bool isCodeValid = verificationDto.FullName == fullName &&
                               verificationDto.VerificationCode == verificationCode &&
                               verificationDto.DateVerificationCode > DateTime.Now;

            if (isCodeValid)
            {
                verificationDto.DateVerificationCode = DateTime.Now.AddMinutes(-1);
                _userRepository.SaveVerificationData(verificationDto);
                return true;
            }

            return false;
        }
    }
}
