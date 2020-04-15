using Autofac;

namespace BerlinClock.Autofac
{
    public class AutoFacServiceLocator : IServiceLocator
    {
        private readonly IContainer _container;

        public AutoFacServiceLocator(IContainer container)
        {
            _container = container;
        }

        public T Get<T>()
        {
            return _container.Resolve<T>();
        }
    }
}