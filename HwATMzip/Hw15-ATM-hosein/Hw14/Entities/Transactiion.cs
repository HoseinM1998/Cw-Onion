using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw14.Entities
{
    public class Transactiion
    {
        public int TransactionId { get; set; }
        public int SourceCardNumber { get; set; }
        public Card SourceCard { get; set; }
        public int DestinationCardNumber { get; set; }
        public Card DestinationCard { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsSuccessful { get; set; }
        public float Fee { get; set; }


    }
}
