using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw14.Contracts
{
    public interface IUserService
    {
        public string GenerateVerificationCode(int userId, string fullName);
        public bool ValidateVerificationCode(int userId, string fullName, int verificationCode);
    }
}
