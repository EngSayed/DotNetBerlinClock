namespace BerlinClock.Autofac
{
    public interface IServiceLocator
    {
        T Get<T>();
    }
}