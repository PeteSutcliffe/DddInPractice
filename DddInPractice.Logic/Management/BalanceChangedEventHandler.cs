using DddInPractice.Logic.Atms;
using DddInPractice.Logic.Common;

namespace DddInPractice.Logic.Management
{
    public class BalanceChangedEventHandler : IHandler<BalanceChangedEvent>
    {
        private readonly IHeadOfficeRepository _repository;

        public BalanceChangedEventHandler(IHeadOfficeRepository repository)
        {
            _repository = repository;
        }

        public void Handle(BalanceChangedEvent domainEvent)
        {
            var headOffice = HeadOfficeInstance.Instance;
            headOffice.ChangeBalance(domainEvent.Delta);
            _repository.Save(headOffice);
        }
    }
}
