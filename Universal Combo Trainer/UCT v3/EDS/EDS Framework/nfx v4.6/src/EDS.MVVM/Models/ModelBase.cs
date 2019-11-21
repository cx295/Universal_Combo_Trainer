using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace EDS.MVVM.Models
{
    public abstract class ModelBase : IModelBase
    {
        public virtual object GetValue([CallerMemberName] string propertyName = null)
        {
            object value = null;

            var propertyInfo = this.GetType().GetProperty(propertyName);

            value = propertyInfo?.GetValue(this);

            return value;
        }

        public virtual void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            var propertyInfo = this.GetType().GetProperty(propertyName);

            if(propertyInfo != null)
            {
                if(propertyInfo.PropertyType == typeof(T))
                {
                    propertyInfo.SetValue(this, value);
                }
                else
                {
                    if (value == null || Convert.IsDBNull(value))
                    {
                        propertyInfo.SetValue(this, null);
                    }
                    else
                    {
                        var converter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);

                        propertyInfo.SetValue(this, converter.ConvertFromString(value.ToString()));
                    }
                }
            }
        }
    }
}
