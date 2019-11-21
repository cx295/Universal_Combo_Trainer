using System;

using EDS.MVVM.Events;

using EDS.Runner.Events;

namespace EDS.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            var eventAggregator = Services.EventAggregator;


            eventAggregator.GetEvent<MySuperCoolEvent>().Publish(default(int));


            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        public Program()
        {
            Services.EventAggregator.GetEvent<MySuperCoolEvent>().Subscribe(DoSomethingElse);
            Services.EventAggregator.GetEvent<MySuperCoolEvent>().Subscribe(DoSomething);
        }

        private void DoSomething(int args)
        {

        }

        private void DoSomethingElse(int args)
        {

        }
    }
}
