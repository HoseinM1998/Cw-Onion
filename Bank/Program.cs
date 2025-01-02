

//using AppDomainAppService.Card;
//using AppDomainAppService.Transaction;
//using AppDomainAppService.User;
//using AppDomainCore.Contract.Card;
//using AppDomainCore.Contract.Transaction;
//using AppDomainCore.Contract.User;
//using AppDomainCore.Dto;
//using AppDomainService.Card;
//using AppDomainService.Transaction;
//using AppDomainService.User;
//using Colors.Net;
//using Colors.Net.StringColorExtensions;
//using ConsoleTables;

//ICardAppSerevice cardService = new CardAppService();
//ITransactionAppService transactionService = new TransactionAppService();
//IUserAppService userService = new UserAppService();

//bool loggedIn = false;
//string cardNumber = "";

//while (true)
//{
//    Console.Clear();
//    ColoredConsole.WriteLine("********* Welcome to Bank System *********".DarkGreen());
//    ColoredConsole.WriteLine("1.Login".DarkBlue());
//    ColoredConsole.WriteLine("2.Exit".DarkRed());
//    string choice = Console.ReadLine();

//    switch (choice)
//    {
//        case "1":
//            ColoredConsole.WriteLine("Enter Card Number: ".DarkYellow());
//            cardNumber = Console.ReadLine();
//            ColoredConsole.WriteLine("Enter Password: ".DarkYellow());
//            string password = Console.ReadLine();

//            try
//            {
//                bool loginResult = cardService.Login(cardNumber, password);

//                if (!loginResult)
//                {
//                    ColoredConsole.WriteLine("Invalid Password".DarkRed());
//                    Console.ReadKey();
//                    break;
//                }

//                loggedIn = true;
//                ColoredConsole.WriteLine("Login Successful".DarkGreen());
//                Console.ReadKey();
//            }
//            catch (Exception ex)
//            {
//                ColoredConsole.WriteLine($"Error: {ex.Message}".DarkRed());
//                Console.ReadKey();
//                break;
//            }
//            break;

//        case "2":
//            return;

//        default:
//            ColoredConsole.WriteLine("Invalid Choice".DarkRed());
//            Console.ReadKey();
//            break;
//    }

//    if (loggedIn)
//    {
//        try
//        {
//            bool inMenu = true;
//            while (inMenu)
//            {
//                Console.Clear();
//                ColoredConsole.WriteLine("********* Bank Menu *********".DarkGreen());
//                ColoredConsole.WriteLine("1.View User Transactions".DarkBlue());
//                ColoredConsole.WriteLine("2.Fund Transfer".DarkBlue());
//                ColoredConsole.WriteLine("3.Show Balance".DarkBlue());
//                ColoredConsole.WriteLine("4.Change Password".DarkBlue());
//                ColoredConsole.WriteLine("5.Exit".DarkRed());


//                string input = Console.ReadLine();

//                switch (input)
//                {
//                    case "1":
//                        var transactions = transactionService.GetTransactionsByCardNumber(cardNumber);
//                        if (transactions.Any())
//                        {
//                            var table = ConsoleTable.From<GetTransactionsDto>(transactions);
//                            Console.ForegroundColor = ConsoleColor.DarkCyan;
//                            table.Write();
//                            Console.ResetColor();
//                        }
//                        else
//                        {
//                            ColoredConsole.WriteLine("No Transaction History Found".DarkRed());
//                        }
//                        Console.ReadKey();
//                        break;

//                    case "2":
//                        ColoredConsole.WriteLine("Enter Amount to Transfer: ".DarkYellow());
//                        float amount = float.Parse(Console.ReadLine());

//                        ColoredConsole.WriteLine("Enter Recipient Card Number: ".DarkYellow());
//                        string recipientCardNumber = Console.ReadLine();
//                        var sourceCardNumber = cardService.GetCardByCardNumber(cardNumber);
//                        var recipientCard = cardService.GetCardByCardNumber(recipientCardNumber);
//                        if (recipientCard != null)
//                        {
//                            ColoredConsole.WriteLine($"Recipient Card Information:".DarkYellow());
//                            ColoredConsole.WriteLine($"CardNumber: {recipientCardNumber}".DarkGray());
//                            ColoredConsole.WriteLine($"Full Name: {recipientCard.User.FullName}".DarkGreen());
//                            ColoredConsole.WriteLine($"Bank: {recipientCard.HolderName}".DarkBlue());
//                            ColoredConsole.WriteLine($"Transfer Amount: {amount}$".DarkCyan());


//                            ColoredConsole.WriteLine("Do You Want To Continue?".DarkMagenta());
//                            ColoredConsole.WriteLine("1:Confirm | 2:Cancel".DarkYellow());

//                            string userInput = Console.ReadLine();

//                            if (userInput == "1")
//                            {
//                                userService.GenerateVerificationCode(sourceCardNumber.Id, sourceCardNumber.User.FullName);
//                                ColoredConsole.WriteLine("Enter Verification Code: ".DarkYellow());
//                                int enteredCode = int.Parse(Console.ReadLine());

//                                bool isCodeValid = userService.ValidateVerificationCode(sourceCardNumber.Id, sourceCardNumber.User.FullName, enteredCode);

//                                if (isCodeValid)
//                                {
//                                    bool transferSuccess = transactionService.TransferFunds(cardNumber, recipientCardNumber, amount);
//                                    var balance = cardService.GetCardBalance(cardNumber);

//                                    if (transferSuccess)
//                                    {
//                                        ColoredConsole.WriteLine("Transfer Successful".DarkGreen());
//                                        ColoredConsole.WriteLine($"New Balance {balance}$".DarkBlue());
//                                    }
//                                    else
//                                    {
//                                        ColoredConsole.WriteLine("Transfer Failed".DarkRed());
//                                    }
//                                }
//                                else
//                                {
//                                    ColoredConsole.WriteLine("Invalid verification code".DarkRed());
//                                }
//                            }
//                            else if (userInput == "2")
//                            {
//                                ColoredConsole.WriteLine("Transfer Canceled".DarkRed());
//                            }
//                            else
//                            {
//                                ColoredConsole.WriteLine("Invalid Option".DarkRed());
//                            }
//                        }
//                        else
//                        {
//                            ColoredConsole.WriteLine("Recipient Card Not Found".DarkRed());
//                        }
//                        Console.ReadKey();
//                        break;

//                    case "3":
//                        try
//                        {
//                            var balance1 = cardService.GetCardBalance(cardNumber);
//                            ColoredConsole.WriteLine($"Your Balance: {balance1}$".DarkGreen());
//                        }
//                        catch (Exception ex)
//                        {
//                            ColoredConsole.WriteLine($"Error: {ex.Message}".DarkRed());
//                        }
//                        Console.ReadKey();
//                        break;
//                    case "4":
//                        ColoredConsole.WriteLine("Enter New Password".DarkBlue());
//                        string newPassword = Console.ReadLine();
//                        bool isPasswordChanged = cardService.ChangePassword(cardNumber, newPassword);
//                        if (isPasswordChanged)
//                        {
//                            ColoredConsole.WriteLine("Change Password Successful".DarkGreen());
//                        }
//                        else
//                        {
//                            ColoredConsole.WriteLine("Failed Password Is 4 Digits".DarkRed());
//                        }
//                        Console.ReadKey();
//                        break;
//                    case "5":
//                        inMenu = false;
//                        ColoredConsole.WriteLine("Logged Out".DarkRed());
//                        Console.ReadKey();
//                        break;

//                    default:
//                        ColoredConsole.WriteLine("Invalid Option".DarkRed());
//                        break;
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            ColoredConsole.WriteLine($"Error: {ex.Message}".DarkRed());
//            Console.ReadKey();
//        }
//    }
//}
