using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainCore.Dto;
using AppDomainCore.Entities;

namespace AppDomainCore.Contract.Transaction
{
    public interface ITransactionRepository
    {


        public void AddTransaction(Transactiion transaction);
        float DailyWithdrawal(string cardNumber);
        public List<GetTransactionsDto> GetListOfTransactions(string cardNumber);

        public void UpdateTransactionFee(string cardNumber, float fee);

    }
}
