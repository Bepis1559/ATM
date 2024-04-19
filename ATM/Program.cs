using ATM.Bank;
using ATM.Bank.FeeCalculations;
using ATM.Bank.Interfaces;
using ATM.Bank.WithdrawCalculation;
using ATM.User;
using ATM.User.Interfaces;
using ATM.User.UserHandlers;
using ATM.User.UserTypes;


// UsersManager for adding users with the factory and singleton design patterns
UsersManager usersStateManager = UsersManager.GetUsersStateManager(new UserFactory());
BaseUser user1 = usersStateManager.AddUser(UserType.Platinum, "Georgi", 150);
BaseUser user2 = usersStateManager.AddUser(UserType.Premium, "Pesho", 550);
BaseUser user3 = usersStateManager.AddUser(UserType.Standard, "Joe", 1150);

List<BaseUser> users = usersStateManager.GetUsers();
foreach (BaseUser user in users)
{
    Console.WriteLine(user.Name + " has " + user.MoneyInAccount + " leva .");

}
// withdrawal inplementation using strategy pattern
Console.WriteLine("ATM operations start : \n");
IStrategyRetriever strategyRetriever = new StrategyRetriever();
ATM_Device atm = new(users);
atm.WithdrawMoney(user1.Id,50,strategyRetriever);
atm.TransferMoney(user3.Id, user1.Id, 250);
foreach (BaseUser user in users)
{
    Console.WriteLine(user.Name + " has " + user.MoneyInAccount + " leva .");

}


