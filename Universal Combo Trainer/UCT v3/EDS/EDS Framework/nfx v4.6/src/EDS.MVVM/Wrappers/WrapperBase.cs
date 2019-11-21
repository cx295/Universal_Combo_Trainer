using System;
using System.Linq;
using System.Collections.Generic;

using EDS.MVVM.Models;


namespace EDS.MVVM.Wrappers
{
    public abstract class WrapperBase<T> : ObservableObject, IWrapperBase<T>
        where T : ModelBase
    {
        public WrapperBase(T model)
        {
            Model = model;
        }

        private T _model;
        public T Model
        {
            get
            {
                return _model;
            }
            private set
            {
                _model = value;
            }
        }

        private Dictionary<string, object> _trackedProperties;
        protected Dictionary<string, object> TrackedProperties
        {
            get
            {
                if (_trackedProperties == null)
                {
                    _trackedProperties = new Dictionary<string, object>();
                }

                return _trackedProperties;
            }
            set
            {
                _trackedProperties = value;
            }
        }

        public virtual object GetValue(string propertyName)
        {
            object value = Model.GetValue(propertyName);

            if (TrackedProperties.ContainsKey(propertyName))
            {
                value = TrackedProperties[propertyName];
            }

            return value;
        }

        public virtual void SetValue(string propertyName, object value)
        {
            var originalValue = Model.GetValue(propertyName);

            if (!Equals(originalValue, value))
            {
                if (!TrackedProperties.ContainsKey(propertyName))
                {
                    TrackedProperties.Add(propertyName, value);
                }
                else
                {
                    TrackedProperties[propertyName] = value;
                }
            }
            else
            {
                if (TrackedProperties.ContainsKey(propertyName))
                {
                    TrackedProperties.Remove(propertyName);
                }
            }

            OnPropertyChanged(propertyName);
            OnPropertyChanged("HasChanges");
        }

        public virtual void AcceptChanges()
        {
            foreach (var property in TrackedProperties)
            {
                var propertyName = property.Key;
                var propertyValue = property.Value;

                Model.SetValue(propertyValue, propertyName);
            }

            ClearProperties();
        }

        public virtual void RejectChanges()
        {
            var properties = TrackedProperties.Keys.ToList();

            ClearProperties();

            foreach(var property in properties)
            {
                OnPropertyChanged(property);
            }
        }

        private void ClearProperties()
        {
            TrackedProperties.Clear();
            OnPropertyChanged("HasChanges");
        }

        public bool HasChanges
        {
            get
            {
                return TrackedProperties.Any();
            }
        }

    }

}
