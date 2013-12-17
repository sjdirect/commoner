using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Commoner.Core.Unity
{
    public interface IFactoryRegistrar
    {
        /// <summary>
        /// Registers types in the param container
        /// </summary>
        /// <param name="container"></param>
        void Register(IUnityContainer container);
    }

    public class ConfigFileFactoryRegistrar : IFactoryRegistrar
    {
        public void Register(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            if (section == null)
                throw new ApplicationException("There is no Unity config section in the app.config or web.config");
            section.Configure(container);
        }
    }
}
