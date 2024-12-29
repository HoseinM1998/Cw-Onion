using AppDomainCore.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainCore.Contract.User
{
    public interface IUserService
    {
        public void GenerateAndSaveVerificationCode(int userId, string fullName, int verificationCode, DateTime expirationTime);
        public VerificationDto GetVerificationDataById(int userId);
        public bool ValidateVerificationCode(int userId, string fullName, int verificationCode);

    }
}
