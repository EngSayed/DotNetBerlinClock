using Autofac;
using BerlinClock.Autofac;
using BerlinClock.IoC;
using BoDi;
using TechTalk.SpecFlow;

namespace BerlinClock.BDD
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void RegisterServiceLocator()
        {
            var container = CreateContainer();

            var serviceLocator = new AutoFacServiceLocator(container);

            _objectContainer.RegisterInstanceAs<IServiceLocator>(serviceLocator);
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<TimeModule>();

            return builder.Build();
        }
    }
}