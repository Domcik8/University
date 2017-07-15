using System;
using System.Collections.Generic;
using Domain;

namespace DomainImp
{
    class InstituteOfMagic : IInstitute
    {
        private string element;
        private Random random = new Random();
        private List<IInstituteObserver> observers = new List<IInstituteObserver>();
        public int ID { get; set; }
        public string Name { get; set; }

        private int influence;

        public int Influence
        {
            get { return influence; }
            set
            {
                if (element == "Fire")
                {
                    influence = value + value * random.Next(0, 5)/10;
                }
                else influence = value * 125 / 100;

                NotifyObservers();
            }
        }
        public InstituteOfMagic(string name)
        {
            this.Name = name;
            this.Influence = random.Next(0, 100);
            if (random.Next(0, 5) > 0)
                element = "Water";
            else element = "Fire";
        }

        public void AddObserver(IInstituteObserver observer)
        {
            observers.Add(observer);
        }

        public void NotifyObservers()
        {
            foreach (IInstituteObserver observer in observers)
            {
                observer.Notify(this);
            }
        }
    }
}