using System;

namespace EDS.ServiceLocation
{
    internal class Node
    {
        public Node(Type serviceType, Type mappedType, bool isSingleton)
        {
            ServiceType = serviceType;
            MappedType = mappedType;
            IsSingleton = isSingleton;
        }

        public bool IsSingleton { get; set; }

        public Type ServiceType { get; set; }

        public Type MappedType { get; set; }

        public object Instance { get; set; }

    }
}