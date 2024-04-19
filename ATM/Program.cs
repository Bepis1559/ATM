using ATM.Bank;
using ATM.Bank.FeeCalculations;
using ATM.Bank.Interfaces;
using ATM.Bank.WithdrawCalculation;
using ATM.User;
using ATM.User.Interfaces;
using ATM.User.UserHandlers;
using ATM.User.UserTypes;


UsersManager usersStateManager = UsersManager.GetUsersStateManager(new UserFactory());
IWithdrawalsObserver withdrawalsObserver = WithdrawalsObserver.GetWithdrawsObserver();
IStrategyRetriever strategyRetriever = new StrategyRetriever();
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
ATM_Device atm = new(withdrawalsObserver);
atm.WithdrawMoney(user1,50,strategyRetriever);
atm.TransferMoney(user3, user1, 250);
atm.WithdrawMoney(user1, 50,strategyRetriever); 
atm.WithdrawMoney(user1, 20,strategyRetriever); 
atm.WithdrawMoney(user1, 10,strategyRetriever); 
atm.WithdrawMoney(user2, 100,strategyRetriever); 
foreach (BaseUser user in users)
{
    Console.WriteLine(user.Name + " has " + user.MoneyInAccount + " leva .");

}

Dictionary<BaseUser, int> usersWithdrawals = withdrawalsObserver.GetMonthlyWidrawals();

foreach (var withdrawal in usersWithdrawals)
{
    Console.WriteLine($"User: {withdrawal.Key.Name}, Withdrawals: {withdrawal.Value}");
}