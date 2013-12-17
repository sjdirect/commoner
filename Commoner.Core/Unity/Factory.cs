using Microsoft.Practices.Unity;

namespace Commoner.Core.Unity
{
    public static class Factory
    {
        static IUnityContainer _container;
        static object _locker = new object();

        /// <summary>
        /// A singleton instance of the unity container
        /// </summary>
        public static IUnityContainer Container 
        {
            get
            {
                lock (_locker)
                {
                    if (_container == null)
                        _container = new UnityContainer();
                }
                return _container;
            }
            set
            {
                lock (_locker)
                {
                    _container = value;
                }
            }
        }

        /// <summary>
        /// Configure the container with a custom implemenation of IFactoryRegistrar
        /// </summary>
        public static void Configure(IFactoryRegistrar registrar)
        {
            lock (_locker)
            {
                registrar.Register(Container);
            }
        }

        /// <summary>
        /// Disposes the container
        /// </summary>
        public static void Dispose()
        {
            lock (_locker)
            {
                if (_container != null)
                    _container.Dispose();
            }
        }
    }
}
