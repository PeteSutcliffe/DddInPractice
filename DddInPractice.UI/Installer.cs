using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DddInPractice.Logic.Atms;
using DddInPractice.Logic.Common;
using DddInPractice.Logic.Management;
using DddInPractice.Logic.SnackMachines;
using DddInPractice.Mongo;
using DddInPractice.UI.Common;

namespace DddInPractice.UI
{
    public class Installer : IWindsorInstaller
    {
        class MyFactory : IMessageHandlerFactory
        {
            private readonly IWindsorContainer _container;

            public MyFactory(IWindsorContainer container)
            {
                _container = container;
            }

            public IEnumerable<Action<IDomainEvent>> GetEventHandlerActions(IDomainEvent domainEvent)
            {
                var handlers = _container.ResolveAll(typeof(IHandler<>).MakeGenericType(domainEvent.GetType()));
                foreach (var handler in handlers)
                {
                    var method = handler.GetType().GetMethod("Handle", new Type[] { domainEvent.GetType() });
                    yield return @event => method.Invoke(handler, new object[] { @event });
                }
            }
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<MainViewModel>());
            container.Register(Component.For<EventDispatcher>());
            container.Register(Component.For<IAtmRepository>().ImplementedBy<AtmRepository>());
            container.Register(Component.For<ISnackMachineRepository>().ImplementedBy<SnackMachineRepository>());
            container.Register(Component.For<IHeadOfficeRepository>().ImplementedBy<HeadOfficeRepository>());
            container.Register(Component.For<IMessageHandlerFactory>().Instance(new MyFactory(container)));
            container.Register(
                Component.For<IHandler<BalanceChangedEvent>>().ImplementedBy<BalanceChangedEventHandler>());
        }
    }
}