using Colors.Net.StringColorExtensions;
using Colors.Net;
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
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService()
        {
            _cardRepository = new CardRepository();
        }

        public bool Login(string cardNumber, string password)
        {
            var tryCount = _cardRepository.GetWrongPasswordTry(cardNumber);
            var card = _cardRepository.GetCard(cardNumber);
            if (card == null)
            {
                throw new Exception("Card not found");
            }
            if (!card.IsActive)
            {
                throw new Exception("Account Blocked");
            }

            if (tryCount > 3)
            {
                _cardRepository.BlockCard(cardNumber);
                return false;
            }

            var passwordIsValid = _cardRepository.PasswordIsValid(cardNumber, password);

            if (passwordIsValid == false)
            {
                _cardRepository.SetWrongPasswordTry(cardNumber);
                return false;
            }

            _cardRepository.ClearWrongPasswordTry(cardNumber);
            return true;
        }

        public float GetCardBalance(string cardNumber)
        {
            var balance = _cardRepository.GetCardBalance(cardNumber);
            if (balance == 0)
            {
                throw new Exception("Balance is Zero");
            }
            return balance;
        }

        public bool GetCardOnline(string cardNumber)
        {
            var card = _cardRepository.GetCard(cardNumber);
            if (card == null)
            {
                throw new Exception("Not Found");
            }
            return true;
        }

        public bool ChangePassword(string cardNumber, string newPass)
        {
            if (string.IsNullOrEmpty(newPass))
            {
                throw new Exception("Password cannot be empty");
            }

            if (newPass.Length != 4)
            {
                throw new Exception("Password Must Exactly 4 Digits");
            }
            _cardRepository.Changepassword(cardNumber, newPass);
            return true;
        }

        public Card GetCardByCardNumber(string cardNumber)
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
