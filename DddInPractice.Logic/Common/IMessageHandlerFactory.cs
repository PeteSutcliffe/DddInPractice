using System;
using System.Collections.Generic;

namespace DddInPractice.Logic.Common
{
    public interface IMessageHandlerFactory
    {
        IEnumerable<Action<IDomainEvent>> GetEventHandlerActions(IDomainEvent domainEvent);
    }
}