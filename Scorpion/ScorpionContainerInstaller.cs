using System;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Scorpion.CommandProcessor;
using Scorpion.Database;
using Scorpion.IoC;

namespace Scorpion
{
    public class CommandProcessorContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICommandHandlerFactory>().ImplementedBy<CommandHandlerFactory>());
            container.Register(
                Component.For<IProcessCommands>()
                    .ImplementedBy<CommandProcessor.CommandProcessor>()
                    .LifestyleTransient());
        }
    }

    public class ScorpionConfigurations
    {
        public static void UseCommandProcessor(IWindsorContainer container, Type assemblyWithHandlers)
        {
            container.Install(new CommandProcessorContainerInstaller());

            container.Register(
                Classes.FromAssemblyContaining(assemblyWithHandlers)
                    .BasedOn(typeof(IHandleCommandsAsync<>))
                    .WithService.Base()
                    .LifestyleTransient());

            container.Register(
                Classes.FromAssemblyContaining(assemblyWithHandlers)
                    .BasedOn(typeof(IHandleCommands<>))
                    .WithService.Base()
                    .LifestyleTransient());
        }

        public static void UseDatabaseContext(IWindsorContainer container, Type databaseContext)
        {
            container.Register(Component.For(databaseContext).LifestylePerWebRequest());
            container.Register(Component.For<IUnitOfWork>()
                .UsingFactoryMethod((a, b) => new UnitOfWork((DbContext)a.Resolve(databaseContext))).LifestyleTransient());
        }

        public static void UseWindsorForWebApi(IWindsorContainer container, HttpConfiguration config)
        {
            config.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
        }

        public static void UseWindsorForMvc(IWindsorContainer container, ControllerBuilder controllerBuilder)
        {
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            controllerBuilder.SetControllerFactory(controllerFactory);
        }
    }
}
