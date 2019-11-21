using EDS.MVVM.Models;
using System;

namespace EDS.MVVM.Wrappers
{
    public interface IWrapperBase<T>
        where T : IModelBase
    {
        T Model { get; }

        object GetValue(string propertyName);

        void SetValue(string propertyName, object value);
    }
}
