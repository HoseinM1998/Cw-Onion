using Hw14.Contracts;
using Hw14.Dto;
using Hw14.Entities;
using Hw14.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hw14.Repositories.TransactionRepository;

namespace Hw14.Services
{
    public class TransactionService : ITransactiionService
    {
        private readonly CardRepository _cardRepository;
        private readonly TransactionRepository _transactionRepository;

        public TransactionService()
        {
            _transactionRepository = new TransactionRepository();
            _cardRepository = new CardRepository();
        }

        public bool TransferFunds(string sourceCardNumber, string destinationCardNumber, float amount)
        {
            var isSuccess = false;

            if (string.IsNullOrEmpty(sourceCardNumber) || string.IsNullOrEmpty(destinationCardNumber))
            {
                throw new ArgumentException("Card numbers cannot be null or empty");
            }

            if (sourceCardNumber.Length != 16 || destinationCardNumber.Length != 16)
            {
                throw new ArgumentException("Card numbers must be 16 digits");
            }

            var totalToday = GetTotalTransactionsForToday(sourceCardNumber);

            if (totalToday + amount > 250)
            {
                throw new InvalidOperationException("Maximum Amount Allowed Per Day 250$");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Transfer Amount Must Be Greater Than Zero");
            }

            var sourceCard = _cardRepository.GetCard(sourceCardNumber);
            var destinationCard = _cardRepository.GetCard(destinationCardNumber);

            if (sourceCard == null || destinationCard == null)
            {
                throw new InvalidOperationException("Card Not Found");
            }

            if (!sourceCard.IsActive || !destinationCard.IsActive)
            {
                throw new InvalidOperationException("Blocked Account");
            }

            float fee = CalculateFee(sourceCardNumber, amount);
            var totalAmount = amount + fee;

            if (sourceCard.Balance < totalAmount)
            {
                throw new InvalidOperationException("Insufficient funds on source card");
            }

            _cardRepository.Withdraw(sourceCardNumber, totalAmount);

            try
            {
                _cardRepository.Deposit(destinationCardNumber, amount);

                _cardRepository.Withdraw(sourceCardNumber, fee);
                _cardRepository.Deposit(destinationCardNumber, fee);

                isSuccess = true;
                var transaction = new Transactiion()
                {
                    SourceCardNumber = sourceCard.Id,
                    DestinationCardNumber = destinationCard.Id,
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    Fee = fee,
                    IsSuccessful = isSuccess
                };

                _transactionRepository.AddTransaction(transaction);

                return true;
            }
            catch (Exception)
            {
                _cardRepository.Deposit(sourceCardNumber, totalAmount);
                throw new InvalidOperationException("Transaction Failed, Funds Returned");
            }
        }

        public List<GetTransactionsDto> GetTransactionsByCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new ArgumentException("Can Not Null", nameof(cardNumber));
            }
            return _transactionRepository.GetListOfTransactions(cardNumber);
        }

        public float GetTotalTransactionsForToday(string cardNumber)
        {
            var transactions = _transactionRepository.DailyWithdrawal(cardNumber);
            return transactions;
        }

        public float CalculateFee(string cardNumber, float amount)
        {
            var card = _cardRepository.GetCard(cardNumber);
            if (card == null)
            {
                throw new InvalidOperationException("Card Not Found");
            }
            float calFee;
            if (amount >= 1000)
            {
                calFee = amount * 0.015f;
            }
            else
            {
                calFee = amount * 0.005f;
            }
            _transactionRepository.UpdateTransactionFee(cardNumber, calFee);
            return calFee;
        }
    }
}
