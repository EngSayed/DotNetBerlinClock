using Autofac;
using BerlinClock.Classes;
using BerlinClock.Interfaces;

namespace BerlinClock.IoC
{
    public class TimeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TimeConverter>().As<ITimeConverter>();
            builder.RegisterType<TimeParser>().As<ITimeParser>();
            builder.RegisterType<BerlinTime>().As<ITime>();
            builder.RegisterType<TimeValidator>().AsSelf();
        }
    }
}