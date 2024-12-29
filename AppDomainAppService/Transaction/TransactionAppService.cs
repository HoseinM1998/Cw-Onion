using AppDomainCore.Contract.Transaction;
using AppDomainCore.Dto;
using AppDomainCore.Entities;
using AppDomainService.Card;
using AppDomainService.Transaction;
using Repository.Card;

namespace AppDomainAppService.Transaction
{
    public class TransactionAppService : ITransactionAppService
    {
        private readonly CardService _cardService;
        private readonly TransactionService _transactionService;

        public TransactionAppService()
        {
            _transactionService = new TransactionService();
            _cardService = new CardService();
        }
        public bool TransferFunds(string sourceCardNumber, string destinationCardNumber, float amount)
        {

            if (sourceCardNumber.Length != 16 || destinationCardNumber.Length != 16)
            {
                throw new Exception("Card Numbers 16 Digits");
            }

            if (amount <= 0)
            {
                throw new Exception("CanNot Zero");
            }

            var sourceCard = _cardService.GetCard(sourceCardNumber);
            var destinationCard = _cardService.GetCard(destinationCardNumber);

            if (sourceCard == null || destinationCard == null)
            {
                throw new Exception("Card Not Found");
            }

            if (!sourceCard.IsActive || !destinationCard.IsActive)
            {
                throw new Exception("Blocked Account");
            }

            var totalToday = GetTotalTransactionsForToday(sourceCardNumber);
            if (totalToday + amount > 250)
            {
                throw new Exception("Maximum Amount Allowed Per Day 250$");
            }

          

            if (sourceCard.Balance < amount)
            {
                throw new Exception("Not Enough Inventory.");
            }

            bool isSuccess = false;
            try
            {
                _cardService.Withdraw(sourceCardNumber, amount);
                _cardService.Deposit(destinationCardNumber, amount);  

                isSuccess = true;
            }
            catch
            {
                _cardService.Deposit(sourceCardNumber, amount); 
                throw;
            }
            finally
            {
                var transaction = new Transactiion()
                {
                    SourceCardNumber = sourceCard.Id,
                    DestinationCardNumber = destinationCard.Id,
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    Fee = 0,
                    IsSuccessful = isSuccess
                };

                _transactionService.AddTransaction(transaction);
            }

            return isSuccess;
        }


        public List<GetTransactionsDto> GetTransactionsByCardNumber(string cardNumber)
        {
            if (cardNumber == null)
            {
                throw new Exception("Can Not Null");
            }
            return _transactionService.GetListOfTransactions(cardNumber);
        }

        public float GetTotalTransactionsForToday(string cardNumber)
        {
            var transactions = _transactionService.DailyWithdrawal(cardNumber);
            return transactions;
        }

        public float CalculateFee(string cardNumber, float amount)
        {
            var card = _cardService.GetCard(cardNumber);
            if (card == null)
            {
                throw new Exception("Card Not Found");
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
            _transactionService.UpdateTransactionFee(cardNumber, calFee);
            return calFee;
        }
    }
}
