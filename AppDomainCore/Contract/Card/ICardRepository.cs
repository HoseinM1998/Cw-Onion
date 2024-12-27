using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainCore.Entities;


namespace AppDomainCore.Contract.Card
{
    public interface ICardRepository
    {
        bool PasswordIsValid(string cardNumber, string password);
        void SetWrongPasswordTry(string cardNumber);
        int GetWrongPasswordTry(string cardNumber);
        void ClearWrongPasswordTry(string cardNumber);
        public Entities.Card GetCard(string cardNumber);
        void Withdraw(string cardNumber, float amount);
        void Deposit(string cardNumber, float amount);
        public void BlockCard(string cardNumber);
        public float GetCardBalance(string cardNumber);
        string Changepassword(string cardNumber, string newPass);
        public Entities.Card GetCardByCardNumber(string cardNumber);

    }
}
