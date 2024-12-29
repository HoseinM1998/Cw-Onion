using AppDomainCore.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainCore.Contract.Transaction
{
    public interface ITransactionAppService
    {
        public bool TransferFunds(string sourceCardNumber, string destinationCardNumber, float amount);
        public List<GetTransactionsDto> GetTransactionsByCardNumber(string cardNumber);
        public float GetTotalTransactionsForToday(string cardNumber);
        public float CalculateFee(string cardNumber, float amount);
    }
}
