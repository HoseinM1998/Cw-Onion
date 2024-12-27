using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw14.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string NationalCode { get; set; }
        public int VerificationCode { get; set; }
        public DateTime DataVerificationCode { get; set; }
        public List<Card> Cards { get; set; }

    }
}
