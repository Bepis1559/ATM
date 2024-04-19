using ATM.Bank;
using ATM.Bank.FeeCalculations;
using ATM.Bank.Interfaces;
using ATM.Bank.WithdrawCalculation;
using ATM.Helpers.classes;
using ATM.Helpers.interfaces;
using ATM.User;
using ATM.User.interfaces;
using ATM.User.Interfaces;
using ATM.User.userHandlers;
using ATM.User.UserHandlers;
using ATM.User.UserTypes;


ILogger logger = new Logger();
ITypeUpdater typeUpdater = new TypeUpdater();
UsersManager usersStateManager = UsersManager.GetUsersStateManager(new UserFactory());
IStrategyRetriever strategyRetriever = new StrategyRetriever();
IUserTypeObserver userTypeObserver = UserTypeObserver.GetUserTypeObserver(typeUpdater, logger);
ATM_Device atm = new(logger);
IUser user1 = usersStateManager.AddUser(UserType.Platinum, "Georgi", 150);
IUser user2 = usersStateManager.AddUser(UserType.Premium, "Pesho", 550);
IUser user3 = usersStateManager.AddUser(UserType.Standard, "Joe", 1150);
List<IUser> users = usersStateManager.GetUsers();


foreach (IUser user in users)
{
    atm.CheckBalance(user);

}
Console.WriteLine("ATM operations start : \n");
atm.WithdrawMoney(user1, 50, strategyRetriever, userTypeObserver);
atm.TransferMoney(user3, user1, 250);
atm.WithdrawMoney(user1, 50, strategyRetriever, userTypeObserver);
atm.WithdrawMoney(user1, 20, strategyRetriever, userTypeObserver);
atm.WithdrawMoney(user1, 10, strategyRetriever, userTypeObserver);
atm.WithdrawMoney(user2, 100, strategyRetriever, userTypeObserver);
foreach (IUser user in users)
{
    atm.CheckBalance(user);

}


//foreach (var user in users)
//{
//    Console.WriteLine(user.MonthlyWithdrawalsCount);
//}