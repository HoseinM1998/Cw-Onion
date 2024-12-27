using Hw14.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw14.Contracts
{
    public interface ICardService
    {
        public bool Login(string cardNumber, string password);

        public float GetCardBalance(string cardNumber);

        public bool GetCardOnline(string cardNumber);
        
        public bool ChangePassword(string cardNumber,string newPass);

        public Card GetCardByCardNumber(string cardNumber);
    }
}
