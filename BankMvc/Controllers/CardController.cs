using AppDomainAppService.Card;
using AppDomainCore.Contract.Card;
using AppDomainCore.Entities;
using AppDomainService.Card;
using Configuration.BankDb;
using Microsoft.AspNetCore.Mvc;

namespace BankMvc.Controllers
{
    public class CardController : Controller
    {
        ICardAppSerevice cardService = new CardAppService();

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string cardNumber, string password)
        {
            var user = cardService.Login(cardNumber, password);
           return RedirectToAction("Index", "Home");

        }



        public IActionResult Profile()
        {
            var card = cardService.GetCardByCardNumber(cardNumber);
            return View("Profile" ,card);
        }
        string cardNumber = InMemory.CurentUser;



        public IActionResult ShowBalance()
        {
            var balance = cardService.GetCardBalance(cardNumber);
            return View(balance);

        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string newPassword)
        {
            var newpass = cardService.ChangePassword(cardNumber, newPassword);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            InMemory.CurentUser = null;
            return RedirectToAction("Login", "Card");
        }

    }
}