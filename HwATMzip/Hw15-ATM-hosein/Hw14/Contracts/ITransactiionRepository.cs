using Hw14.Dto;
using Hw14.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw14.Contracts
{
    public interface ITransactiionRepository
    {
        public void AddTransaction(Transactiion transaction);
        float DailyWithdrawal(string cardNumber);
        public List<GetTransactionsDto> GetListOfTransactions(string cardNumber);

        public void UpdateTransactionFee(string cardNumber, float fee);

    }
}
