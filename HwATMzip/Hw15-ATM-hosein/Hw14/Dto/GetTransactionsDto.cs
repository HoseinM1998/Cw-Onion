using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw14.Dto
{
    public class GetTransactionsDto
    {
        public string SourceCardNumber { get; set; }
        public string DestinationsCardNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public float Amount { get; set; }
        public float Fee { get; set; }
        public bool IsSuccess { get; set; }
    }
}
