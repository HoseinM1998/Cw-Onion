using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainCore.Contract.User
{
    public interface IUserAppService
    {
        public string GenerateVerificationCode(int userId, string fullName);
        public bool ValidateVerificationCode(int userId, string fullName, int verificationCode);
    }
}
