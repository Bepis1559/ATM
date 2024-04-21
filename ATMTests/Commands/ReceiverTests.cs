using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ATM.Bank.Interfaces;
using ATM.Helpers.interfaces;
using ATM.User.interfaces;

namespace ATM.Commands.Tests
{
    [TestClass()]
    public class ReceiverTests
    {
        private Mock<IATM>? _atmMock;
        private Mock<ILogger>? _loggerMock;
        private Mock<IGetUser>? _usersManagerMock;
        private Mock<IStrategyRetriever>? _strategyRetrieverMock;
        private Mock<IObserver>? _userTypeObserverMock;
        private Receiver? _receiver;

        [TestInitialize]
        public void Setup()
        {
            _atmMock = new Mock<IATM>();
            _loggerMock = new Mock<ILogger>();
            _usersManagerMock = new Mock<IGetUser>();
            _strategyRetrieverMock = new Mock<IStrategyRetriever>();
            _userTypeObserverMock = new Mock<IObserver>();

            _receiver = new Receiver(_atmMock.Object, _loggerMock.Object, _usersManagerMock.Object, _strategyRetrieverMock.Object, _userTypeObserverMock.Object);
        }

        [TestMethod()]
        public void CheckBalanceTest()
        {
            // Arrange
            var user = new Mock<IUser>();
            _usersManagerMock?.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user.Object);

            // Act
            _receiver?.CheckBalance();

            // Assert
            _atmMock?.Verify(x => x.CheckBalance(user.Object), Times.Once);
        }

        [TestMethod()]
        public void TransferMoneyTest()
        {
            // Arrange
            var sender = new Mock<IUser>();
            var receiver = new Mock<IUser>();
            _usersManagerMock?.SetupSequence(x => x.GetUser(It.IsAny<string>()))
                .Returns(sender.Object)
                .Returns(receiver.Object);

            // Act
            _receiver?.TransferMoney();

            // Assert
            _atmMock?.Verify(x => x.TransferMoney(sender.Object, receiver.Object, It.IsAny<decimal>()), Times.Once);
        }

        [TestMethod()]
        public void WithdrawMoneyTest()
        {
            // Arrange
            var user = new Mock<IUser>();
            _usersManagerMock?.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user.Object);

            // Act
            _receiver?.WithdrawMoney();

            // Assert
            _atmMock?.Verify(x => x.WithdrawMoney(user.Object, It.IsAny<decimal>(), _strategyRetrieverMock.Object, _userTypeObserverMock.Object), Times.Once);
        }
    }
}
