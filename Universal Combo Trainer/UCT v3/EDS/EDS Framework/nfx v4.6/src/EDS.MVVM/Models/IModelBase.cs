using System;
using System.Runtime.CompilerServices;

namespace EDS.MVVM.Models
{
    public interface IModelBase
    {
        object GetValue([CallerMemberName] string propertyName = null);

        void SetValue<T>(T value, [CallerMemberName] string propertyName = null);
    }
}
