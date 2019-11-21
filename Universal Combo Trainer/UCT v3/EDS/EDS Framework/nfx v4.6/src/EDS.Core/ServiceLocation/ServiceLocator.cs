using System;
using System.Collections.Generic;

using EDS.ServiceLocation.Exceptions;

namespace EDS.ServiceLocation
{
    public static class ServiceLocator
    {
        private static Dictionary<string, Node> _services;
        private static Dictionary<string, Node> Services
        {
            get
            {
                if (_services == null)
                {
                    _services = new Dictionary<string, Node>();
                }

                return _services;
            }
            set
            {
                _services = value;
            }
        }

        #region RegisterService
        /// <summary>
        /// Registers the service 
        /// </summary>
        /// <typeparam name="TServiceType">The public exposed interface the service will be referenced by.</typeparam>
        /// <typeparam name="TMappedType">The concrete type that will be instanciated to.</typeparam>
        /// <param name="isSingleton">If true, the same instance will be provided to all consumers</param>
        public static void RegisterService<TServiceType, TMappedType>(bool isSingleton)
            where TMappedType : TServiceType
        {
            DoRegisterService<TServiceType, TMappedType>(typeof(TServiceType).ToString(), isSingleton);
        }

        /// <summary>
        /// Registers the service
        /// </summary>
        /// <typeparam name="TServiceType">The public exposed interface the service will be referenced by.</typeparam>
        /// <typeparam name="TMappedType">The concrete type that will be instanciated to.</typeparam>
        /// <param name="serviceKey">The unique key that will be used to reference the service.</param>
        /// <remarks>All named services will be created as Singleton services.</remarks>
        public static void RegisterService<TServiceType, TMappedType>(string serviceKey)
            where TMappedType : TServiceType
        {
            #region Housekeeping
            if (serviceKey == null)
            {
                throw new ArgumentNullException("serviceKey");
            }

            if (string.IsNullOrWhiteSpace(serviceKey))
            {
                throw new ArgumentException();
            }

            if (Equals(typeof(TServiceType).ToString(), serviceKey))
            {
                throw new ArgumentException("ServiceKey cannot be set to TServiceType.ToString()");
            }
            #endregion

            DoRegisterService<TServiceType, TMappedType>(serviceKey, true);
        }

        private static void DoRegisterService<TServiceType, TMappedType>(string key, bool isSingleton)
            where TMappedType : TServiceType
        {
            if (!Services.ContainsKey(key))
            {
                var node = new Node(typeof(TServiceType), typeof(TMappedType), isSingleton);
                Services.Add(key, node);
            }
            else
            {
                var node = Services[key];
                if (!Equals(typeof(TMappedType), node.MappedType))
                {
                    throw new DuplicateServiceRegistrationException();
                }
                else if (!Equals(isSingleton, node.IsSingleton))
                {
                    throw new MismatchedServiceModeException();
                }
            }
        }
        #endregion

        #region GetInstance
        /// <summary>
        /// Gets an instance of the registered service
        /// </summary>
        /// <typeparam name="TServiceType">The interface the service is known as</typeparam>
        /// <returns>Returns an instance of the service</returns>
        public static TServiceType GetInstance<TServiceType>()
        {
            return (TServiceType)DoGetInstance(typeof(TServiceType).ToString());
        }

        /// <summary>
        /// Gets an instance of the named service
        /// </summary>
        /// <typeparam name="TServiceType">The interface the service is known as</typeparam>
        /// <param name="serviceKey">The unique key the service is referenced by</param>
        /// <returns>Returns an instance of the named service</returns>
        public static TServiceType GetInstance<TServiceType>(string serviceKey)
        {
            return (TServiceType)DoGetInstance(serviceKey);
        }

        private static object DoGetInstance(string serviceKey)
        {
            object instance = null;

            if (Services.ContainsKey(serviceKey))
            {
                var node = Services[serviceKey];

                if(node.Instance == null)
                {
                    instance = Activator.CreateInstance(node.MappedType);

                    if (node.IsSingleton)
                    {
                        node.Instance = instance;
                    }
                }
                else
                {
                    instance = node.Instance;
                }

            }
            else
            {
                throw new UnregisteredServiceException();
            }

            return instance;
        }
        #endregion

    }
}
