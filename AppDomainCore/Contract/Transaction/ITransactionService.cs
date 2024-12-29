using AppDomainCore.Dto;
using AppDomainCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainCore.Contract.Transaction
{
    public interface ITransactionService
    {
        public void AddTransaction(Transactiion transaction);
        float DailyWithdrawal(string cardNumber);
        public List<GetTransactionsDto> GetListOfTransactions(string cardNumber);

        public void UpdateTransactionFee(string cardNumber, float fee);
    }
}
