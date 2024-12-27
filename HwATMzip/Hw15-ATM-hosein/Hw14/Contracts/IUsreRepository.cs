using Hw14.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw14.Contracts
{
    public interface IUsreRepository
    {
        public void GenerateAndSaveVerificationCode(int userId, string fullName, int verificationCode, DateTime expirationTime);
        public VerificationDto GetVerificationDataById(int userId);

    }
}
