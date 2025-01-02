using AppDomainAppService.Card;
using AppDomainAppService.Transaction;
using AppDomainAppService.User;
using AppDomainCore.Contract.Card;
using AppDomainCore.Contract.Transaction;
using AppDomainCore.Contract.User;
using AppDomainCore.Dto;
using AppDomainCore.Entities;
using Configuration.BankDb;
using Microsoft.AspNetCore.Mvc;

namespace BankMvc.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ICardAppSerevice cardService;
        private readonly ITransactionAppService transactionService;
        private readonly IUserAppService userService;
        string cardNumber = InMemory.CurentUser;

        public TransactionController(ICardAppSerevice _cardService, ITransactionAppService _transactionService, IUserAppService _userService)
        {
            cardService = _cardService;
            transactionService = _transactionService;
            userService = _userService;
        }


        public ActionResult List()
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                return RedirectToAction("Login", "Card");
            }

            List<GetTransactionsDto> transactions = transactionService.GetTransactionsByCardNumber(cardNumber);
            return View("ListTransaction", transactions);
        }

        [HttpGet]
        public ActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Transfer(float amount, string recipientCardNumber)
        {
            var sourceCard = cardService.GetCardByCardNumber(cardNumber);
            var recipientCard = cardService.GetCardByCardNumber(recipientCardNumber);
            if (recipientCard != null)
            {
                ViewBag.TransferAmount = amount;
                var model = new Card
                {
                    CardNumber = recipientCard.CardNumber,
                    User = new User
                    {
                        FullName = recipientCard.User.FullName
                    },
                    HolderName = recipientCard.HolderName
                };
                userService.GenerateVerificationCode(sourceCard.Id, sourceCard.User.FullName);
                return View(model);
            }
            return RedirectToAction("Transfer");
        }

        [HttpPost]
        public ActionResult ConfirmTransfer(int verificationCode, string recipientCardNumber, float amount)
        {
            var sourceCard = cardService.GetCardByCardNumber(cardNumber);
            bool isCodeValid = userService.ValidateVerificationCode(sourceCard.Id, sourceCard.User.FullName, verificationCode);

            if (isCodeValid)
            {
                bool transferSuccess = transactionService.TransferFunds(cardNumber, recipientCardNumber, amount);
                if (transferSuccess)
                {
                    return RedirectToAction("List");
                }
            }
            return RedirectToAction("Transfer");
        }
    }
}
