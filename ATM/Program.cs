using ATM.Bank;
using ATM.Bank.FeeCalculations;
using ATM.Bank.Interfaces;
using ATM.Bank.WithdrawCalculation;
using ATM.Commands;
using ATM.Helpers.classes;
using ATM.Helpers.interfaces;
using ATM.User;
using ATM.User.interfaces;
using ATM.User.Interfaces;
using ATM.User.userHandlers;
using ATM.User.UserHandlers;
using ATM.User.UserTypes;


ILogger logger = new Logger();
IReader reader = new Reader();
ITypeUpdater typeUpdater = new TypeUpdater();
IUserFactory userFactory = new UserFactory();
UsersManager usersManager = UsersManager.GetUsersStateManager();
IStrategyRetriever strategyRetriever = new StrategyRetriever();
IObserver userTypeObserver = UserTypeObserver.GetUserTypeObserver(typeUpdater, logger);
IObserver dividendObserver = MonthlyDividendObserver.GetMonthlyDividendObserver(logger);
ATM_Device atm = new(logger);
Invoker invoker = new ();
ICommandReceiver commandReceiver = new Receiver (atm,logger,usersManager,strategyRetriever,userTypeObserver,reader);
IUser user1 = usersManager.AddUser("Georgi", 150, dividendObserver, logger,userFactory);
IUser user2 = usersManager.AddUser("Pesho", 550, dividendObserver, logger,userFactory);
IUser user3 = usersManager.AddUser("Joe", 1150, dividendObserver, logger, userFactory);
List<IUser> users = usersManager.GetUsers();




foreach (IUser user in users)
{
    Console.WriteLine(user.Id);
}

invoker.HandleCustomerInteraction(logger, commandReceiver,reader);
