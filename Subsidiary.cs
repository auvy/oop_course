using System;
using System.Threading.Tasks;

namespace task2 {


    interface IObservable
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver();
        void NotifyObserver();
        void UpdateObserverShare(decimal share);
    }

    class Subsidiary : IObservable
    {
        public decimal parentShare;

        //CHAIN OF RESPONSIBILITY
        public Subsidiary Successor;
        public bool available = true;

        //OBSERVER
        public IObserver observer;

        public string name;
        public Material material;
        public decimal freeShare;
        public decimal experience;

        public void RegisterObserver(IObserver observer)
        {
            this.observer = observer;
        }

        public void RemoveObserver()
        {
            this.observer = null;
        }

        public Subsidiary(string name, Material mat, decimal freeShare, decimal eff, decimal parentShare, Manufactor observer)
        {
            this.name = name;
            this.material = mat;
            this.freeShare = freeShare;
            this.experience = eff;
            this.parentShare = parentShare;
            this.observer = observer;
        }
        public void NotifyObserver()
        {
            experience += 0.1m;
            observer.Update();
        }

        public void Manufacturing(int amount)
        {
            int time = (int) (amount * (2 - experience));
            Console.WriteLine($"This will take {time} milliseconds for {name}.");
            Task.Delay(time).ContinueWith(t=> {
                available = true;
                Console.WriteLine($"{name} is available again! \n> ");  
                if(experience < 1) NotifyObserver();
            });

        }

        public bool ManufactureMaterial(int amount)
        {
            if (amount == 0)
            {
                Console.WriteLine("There's nothing to do. Abort...");
                return true;
            }
            if (available == true)
            {
                available = false;
                Manufacturing(amount);
                return true;
            }
            else if (Successor != null)
            {
                Console.WriteLine($"{name} is busy, looking for successor...");
                return Successor.ManufactureMaterial(amount);
            }
            else if (Successor == null)
            {
                Console.WriteLine($"{name} is busy and has no successor.");
                return false;
            } 
            return false;
        }
        public void PrintSuccessor()
        {
            if(Successor!=null) Console.WriteLine($"{Successor.name}");
            else Console.WriteLine($"No successor");
        }
        public void UpdateObserverShare(decimal share)
        {
            freeShare += share;
            observer.UpdateShare(share); 
        }
    }
}