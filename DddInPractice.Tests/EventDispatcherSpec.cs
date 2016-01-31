using System;
using System.Collections.Generic;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using DddInPractice.Logic.Common;
using NUnit.Framework;

namespace DddInPractice.Tests
{
    [TestFixture]
    public class EventDispatcherSpec
    {
        private WindsorContainer _container;
        private EventDispatcher _dispatcher;

        [SetUp]
        public void Setup()
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());
            _dispatcher = _container.Resolve<EventDispatcher>();
        }

        [Test]
        public void CanResolveEventDispatcher()
        {
            Assert.That(_dispatcher, Is.Not.Null);
        }

        [Test]
        public void CanDispatchEvent()
        {
            IDomainEvent testEvent = new TestEvent();
            _dispatcher.Dispatch(testEvent);
            Assert.That(TestEventHandler.MessagesHandled, Is.EqualTo(1));
        }
    }

    public class TestEvent : IDomainEvent
    {
    }

    public class TestEventHandler : IHandler<TestEvent>
    {
        public static int MessagesHandled;

        public void Handle(TestEvent message)
        {
            MessagesHandled++;
        }
    }

    

    public class EventDispatcherInstaller : IWindsorInstaller
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
                var handlers = _container.ResolveAll(typeof (IHandler<>).MakeGenericType(domainEvent.GetType()));
                foreach (var handler in handlers)
                {
                    var method = handler.GetType().GetMethod("Handle", new Type[] {domainEvent.GetType()});
                    yield return @event => method.Invoke(handler, new object[] {@event});
                }
            }
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<EventDispatcher>());
            container.Register(Component.For<IHandler<TestEvent>>().ImplementedBy<TestEventHandler>());
            container.Register(Component.For<IMessageHandlerFactory>().Instance(new MyFactory(container)));
        }
    }
}