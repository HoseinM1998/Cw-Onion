﻿using AppDomainCore.Contract.Card;
using Configuration.BankDb;
using Microsoft.EntityFrameworkCore;

namespace Repository.Card
{
    public class CardRepository : ICardRepository
    {
        private readonly BankDbContext _context;

        public CardRepository(BankDbContext context)
        {
            _context = context;
        }

        public bool PasswordIsValid(string cardNumber, string password)
        {
            return _context.Cards.Any(x => x.CardNumber == cardNumber && x.Password == password);
        }

        public void SetWrongPasswordTry(string cardNumber)
        {
            var card = _context.Cards
           .FirstOrDefault(x => x.CardNumber == cardNumber);
            if (card is null)
            {
                throw new Exception($"Not Found {cardNumber}");
            }
            card.WrongPasswordTries++;
            _context.SaveChanges();
        }

        public int GetWrongPasswordTry(string cardNumber)
        {
            var card = _context.Cards
            .FirstOrDefault(x => x.CardNumber == cardNumber);
            if (card is null)
            {
                throw new Exception($"Not Found {cardNumber}");
            }
            return card.WrongPasswordTries;
        }

        public void ClearWrongPasswordTry(string cardNumber)
        {
            var card = _context.Cards
           .FirstOrDefault(x => x.CardNumber == cardNumber);
            if (card is null)
            {
                throw new Exception($"Not Found {cardNumber}");
            }
            card.WrongPasswordTries = 0;
            _context.SaveChanges();
        }

        public AppDomainCore.Entities.Card GetCard(string cardNumber)
        {
            var card = _context.Cards.FirstOrDefault(x => x.CardNumber == cardNumber);
            if (card is null)
            {
                throw new Exception($"Card  {cardNumber} Not Found");
            }
            else
            {
                return card;
            }
        }

        public void Withdraw(string cardNumber, float amount)
        {
            var card = _context.Cards
            .FirstOrDefault(x => x.CardNumber == cardNumber);
            if (card is null)
            {
                throw new Exception($"Not Found {cardNumber}");
            }
            card.Balance -= amount;
            _context.SaveChanges();
        }

        public void Deposit(string cardNumber, float amount)
        {
            var card = _context.Cards
         .FirstOrDefault(x => x.CardNumber == cardNumber);
            if (card is null)
            {
                throw new Exception($"Not Fund {cardNumber}");
            }
            card.Balance += amount;
            _context.SaveChanges();
        }
        public void BlockCard(string cardNumber)
        {
            var card = _context.Cards.FirstOrDefault(x => x.CardNumber == cardNumber);
            if (card == null)
            {
                throw new Exception($"Not Found");
            }
            card.IsActive = false;
            _context.SaveChanges();
        }

        public float GetCardBalance(string cardNumber)
        {
            return _context.Cards
                .Where(c => c.CardNumber == cardNumber)
                .Select(c => c.Balance)
                .FirstOrDefault();
        }

        public string Changepassword(string cardNumber, string newPass)
        {
            var card = _context.Cards.FirstOrDefault(x => x.CardNumber == cardNumber);
            if (card is null)
            {
                return $"Cannot Find CardNumber {cardNumber}.";
            }
            card.Password = newPass;
            _context.SaveChanges();
            return $"Password Changed Successfully";
        }

        public AppDomainCore.Entities.Card GetCardByCardNumber(string cardNumber)
        {
            return _context.Cards
                .Include(c => c.User)
                .FirstOrDefault(c => c.CardNumber == cardNumber);
        }
    }
}
