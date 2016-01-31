namespace DddInPractice.Logic.Common
{
    public class EventDispatcher
    {
        private readonly IMessageHandlerFactory _factory;

        public EventDispatcher(IMessageHandlerFactory factory)
        {
            _factory = factory;
        }

        public void Dispatch(IDomainEvent message)
        {
            var handlers = _factory.GetEventHandlerActions(message);

            foreach (var handler in handlers)
            {
                handler.Invoke(message);
            }
        }

        public void DispatchPendingFor(AggregateRoot aggregate)
        {
            var pending = aggregate.DomainEvents;
            foreach (var domainEvent in pending)
            {
                Dispatch(domainEvent);
            }
            aggregate.ClearEvents();
        }
    }
}
