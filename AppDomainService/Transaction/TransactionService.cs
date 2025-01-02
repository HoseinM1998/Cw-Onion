using AppDomainCore.Contract.Transaction;
using AppDomainCore.Dto;
using AppDomainCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainCore.Contract.Card;
using Repository.Card;
using Repository.Transaction;

namespace AppDomainService.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ICardRepository _cardRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ICardRepository cardRepository, ITransactionRepository transactionRepository)
        {
            _cardRepository = cardRepository;
            _transactionRepository =  transactionRepository;
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
