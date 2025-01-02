using AppDomainCore.Contract.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Card;

namespace AppDomainService.Card
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public bool PasswordIsValid(string cardNumber, string password)
        {
            var card = _cardRepository.GetCard(cardNumber);
            if (card == null)
            {
                throw new Exception("Card Not Found");
            }
            return _cardRepository.PasswordIsValid(cardNumber, password);
        }

        public void SetWrongPasswordTry(string cardNumber)
        {
            _cardRepository.SetWrongPasswordTry(cardNumber);
        }

        public int GetWrongPasswordTry(string cardNumber)
        {
            return _cardRepository.GetWrongPasswordTry(cardNumber);
        }

        public void ClearWrongPasswordTry(string cardNumber)
        {
            _cardRepository.ClearWrongPasswordTry(cardNumber);
        }

        public AppDomainCore.Entities.Card GetCard(string cardNumber)
        {
            var card = _cardRepository.GetCard(cardNumber);
            if (card == null)
            {
                throw new Exception("Card Not Found");
            }
            return card;
        }

        public void Withdraw(string cardNumber, float amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Amount Not Zero");
            }

            var balance = _cardRepository.GetCardBalance(cardNumber);
            if (balance < amount)
            {
                throw new Exception("Not Enough Inventory");
            }

            _cardRepository.Withdraw(cardNumber, amount);
        }

        public void Deposit(string cardNumber, float amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Amount Not Zero");
            }

            _cardRepository.Deposit(cardNumber, amount);
        }

        public void BlockCard(string cardNumber)
        {
            var card = _cardRepository.GetCard(cardNumber);
            if (card == null)
            {
                throw new Exception("Card Not Found");
            }

            _cardRepository.BlockCard(cardNumber);
        }

        public float GetCardBalance(string cardNumber)
        {
            return _cardRepository.GetCardBalance(cardNumber);
        }

        public string Changepassword(string cardNumber, string newPass)
        {
            if (newPass == null && newPass.Length != 4)
            {
                throw new Exception("Password Must Exactly 4 Digits");
            }

            _cardRepository.Changepassword(cardNumber, newPass);
            return "Password Changed Successfully";
        }

        public AppDomainCore.Entities.Card GetCardByCardNumber(string cardNumber)
        {
            var card = _cardRepository.GetCardByCardNumber(cardNumber);
            if (card == null)
            {
                throw new Exception("Card Not Found");
            }
            return card;
        }
    }
}
