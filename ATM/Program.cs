using ATM.Bank;
using ATM.Bank.FeeCalculations;
using ATM.User;
using ATM.User.Interfaces;


// withdrawal inplementation using strategy pattern
//decimal amount = 500;
//ATM_Device atm = new();
//UpTo100WithdrawCalculationStrategy upTo100WithdrawCalculationStrategy = new(); 
//UpTo1000WithdrawCalculationStrategy upTo1000WithdrawCalculationStrategy = new(); 
//Over1000WithdrawCalculationStrategy over1000WithdrawCalculationStrategy = new(); 


//atm.SetWithdrawCalculationStrategy(over1000WithdrawCalculationStrategy);
//decimal moneyWithdrawn = atm.WithdrawMoney(amount);
//Console.WriteLine(moneyWithdrawn);



// UsersStateManager for adding users with the factory and singleton design patterns
//UsersStateManager usersStateManager = UsersStateManager.GetUsersStateManager(new UserFactory());
//usersStateManager.AddUser(UserType.Platinum, "Georgi", 150);
//usersStateManager.AddUser(UserType.Premium, "Pesho", 550);
//usersStateManager.AddUser(UserType.Standard, "Joe", 1150);

//List<BaseUser> users = usersStateManager.GetUsers();
//foreach (BaseUser user in users)
//{
//    Console.WriteLine(user.Name + " has " + user.MoneyInAccount + " leva .");
//    Console.WriteLine();
   
//}