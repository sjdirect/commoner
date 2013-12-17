using Microsoft.Practices.Unity;

namespace Commoner.Core.Unity
{
    public static class Factory
    {
        static IUnityContainer _container;

        /// <summary>
        /// A singleton instance of the unity container
        /// </summary>
        public static IUnityContainer Container 
        {
            get
            {
                if (_container == null)
                    _container = new UnityContainer();
               
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        /// <summary>
        /// Configure the container with a custom implemenation of IFactoryRegistrar
        /// </summary>
        public static void Configure(IFactoryRegistrar registrar)
        {
            registrar.Register(Container);
        }

        /// <summary>
        /// Disposes the container
        /// </summary>
        public static void Dispose()
        {
            if (_container != null)
                _container.Dispose();
        }
    }
}
