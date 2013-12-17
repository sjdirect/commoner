using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Commoner.Core.Unity
{
    public class AlternateConfigFileFactoryRegistrar : IFactoryRegistrar
    {
        string _fileName;

        public AlternateConfigFileFactoryRegistrar(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");

            _fileName = fileName;
        }

        public void Register(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            //http://msdn.microsoft.com/en-us/library/ff660935%28PandP.20%29.aspx#_Loading_the_Configuration
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = _fileName };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection unitySection = (UnityConfigurationSection)configuration.GetSection("unity");

            container.LoadConfiguration(unitySection);
        }
    }
}
