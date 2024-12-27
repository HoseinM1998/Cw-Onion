using Hw14.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw14.Contracts
{
    public interface ICardRepository
    {
        bool PasswordIsValid(string cardNumber, string password);
        void SetWrongPasswordTry(string cardNumber);
        int GetWrongPasswordTry(string cardNumber);
        void ClearWrongPasswordTry(string cardNumber);
        public Card GetCard(string cardNumber);
        void Withdraw(string cardNumber, float amount);
        void Deposit(string cardNumber, float amount);
        public void BlockCard(string cardNumber);
        public float GetCardBalance(string cardNumber);
        string Changepassword(string cardNumber, string newPass);
        public Card GetCardByCardNumber(string cardNumber);

    }
}
