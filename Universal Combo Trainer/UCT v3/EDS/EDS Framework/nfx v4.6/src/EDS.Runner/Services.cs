using System;

using EDS.MVVM.Events;


namespace EDS.Runner
{
    public static class Services
    {
        private static IEventAggregator _eventAggregator;
        public static IEventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null)
                {
                    _eventAggregator = new EventAggregator();
                }

                return _eventAggregator;
            }
        }
    }
}
