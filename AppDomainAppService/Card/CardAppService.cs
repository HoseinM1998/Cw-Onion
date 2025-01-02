using AppDomainCore.Contract.Card;
using AppDomainService.Card;
using Configuration.BankDb;

namespace AppDomainAppService.Card
{
    public class CardAppService : ICardAppSerevice
    {
        private readonly ICardService _cardService;
        public CardAppService(ICardService cardService)
        {
            _cardService = cardService;
        }

        public bool Login(string cardNumber, string password)
        {
            var tryCount = _cardService.GetWrongPasswordTry(cardNumber);
            var card = _cardService.GetCard(cardNumber);
            if (card == null)
            {
                throw new Exception("Card not found");
            }
            if (!card.IsActive)
            {
                throw new Exception("Account Blocked");
            }

            if (tryCount > 3)
            {
                _cardService.BlockCard(cardNumber);
                return false;
            }

            var passwordIsValid = _cardService.PasswordIsValid(cardNumber, password);

            if (passwordIsValid == false)
            {
                _cardService.SetWrongPasswordTry(cardNumber);
                return false;
            }

            _cardService.ClearWrongPasswordTry(cardNumber);
            InMemory.CurentUser = cardNumber;

            return true;
        }

        public float GetCardBalance(string cardNumber)
        {
            var balance = _cardService.GetCardBalance(cardNumber);
            if (balance == 0)
            {
                throw new Exception("Balance is Zero");
            }
            return balance;
        }

        public bool GetCardOnline(string cardNumber)
        {
            var card = _cardService.GetCard(cardNumber);
            if (card == null)
            {
                throw new Exception("Not Found");
            }
            return true;
        }

        public bool ChangePassword(string cardNumber, string newPass)
        {
            if (string.IsNullOrEmpty(newPass))
            {
                throw new Exception("Password cannot be empty");
            }

            if (newPass.Length != 4)
            {
                throw new Exception("Password Must Exactly 4 Digits");
            }
            _cardService.Changepassword(cardNumber, newPass);
            return true;
        }

        public AppDomainCore.Entities.Card GetCardByCardNumber(string cardNumber)
        {
            var card = _cardService.GetCardByCardNumber(cardNumber);
            if (card == null)
            {
                throw new Exception("Card Not Found");
            }
            return card;
        }
    }
}
