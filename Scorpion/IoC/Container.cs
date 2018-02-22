using System;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Scorpion.IoC
{
    public static class Container
    {
        private static readonly WindsorContainer _container;

        static Container()
        {
            _container = new WindsorContainer();
        }

        public static T Resolve<T>(string name)
        {
            return _container.Resolve<T>(name);

        }

        public static void SetComponentRegistrationCallback(ComponentModelDelegate callback)
        {
            _container.Kernel.ComponentModelCreated += callback;
        }

        public static void Install(params IWindsorInstaller[] installers)
        {
            _container.Install(installers);
        }

        public static IWindsorContainer GetContainer()
        {
            return _container;
        }

        public static Object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public static void Dispose(Object commandHandler)
        {
            _container.Release(commandHandler);
        }

        public static bool HasRegistration(Type type)
        {
            return _container.Kernel.HasComponent(type);
        }
    }
}
