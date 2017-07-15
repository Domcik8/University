using System;
using System.Collections.Generic;
using Domain;

namespace DomainImp
{
    class InstituteOfWar : IInstitute
    {
        private List<IInstituteObserver> observers = new List<IInstituteObserver>();
        public int ID { get; set; }
        public string Name { get; set; }
        
        private int influence;

        public int Influence
        {
            get { return influence; }
            set
            {
                influence = value;
                NotifyObservers();
            }
        }
        public InstituteOfWar(string name)
        {
            this.Name = name;
            this.Influence = 50;
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