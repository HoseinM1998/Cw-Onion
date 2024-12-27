using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw14.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string HolderName { get; set; }
        public float Balance { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public int WrongPasswordTries { get; set; } = 0;
        public int UserId { get; set; }  
        public User User { get; set; }
        public List<Transactiion> TransactionsAsSource { get; set; }
        public List<Transactiion> TransactionsAsDestination { get; set; }
    }
}
