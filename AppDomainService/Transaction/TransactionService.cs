using AppDomainCore.Contract.Transaction;
using AppDomainCore.Dto;
using AppDomainCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Card;
using Repository.Transaction;

namespace AppDomainService.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly CardRepository _cardRepository;
        private readonly TransactionRepository _transactionRepository;

        public TransactionService()
        {
            _transactionRepository = new TransactionRepository();
            _cardRepository = new CardRepository();
        }
        public void AddTransaction(Transactiion transaction)
        {
            if (transaction == null)
            {
                throw new Exception("Transaction CanNot Null");
            }

            _transactionRepository.AddTransaction(transaction);
        }

        public float DailyWithdrawal(string cardNumber)
        {
            if (cardNumber == null)
            {
                throw new Exception("CanNot Null");
            }

            return _transactionRepository.DailyWithdrawal(cardNumber);
        }

        public List<GetTransactionsDto> GetListOfTransactions(string cardNumber)
        {
            if (cardNumber == null)
            {
                throw new Exception("CanNot Null");
            }

            return _transactionRepository.GetListOfTransactions(cardNumber);
        }

        public void UpdateTransactionFee(string cardNumber, float fee)
        {
            if (cardNumber == null)
            {
                throw new Exception("CanNot Null");
            }

            if (fee < 0)
            {
                throw new Exception("Invalid");
            }

            _transactionRepository.UpdateTransactionFee(cardNumber, fee);
        }

    }
}
