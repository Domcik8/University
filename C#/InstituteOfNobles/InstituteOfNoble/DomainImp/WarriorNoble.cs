using System;
using System.Collections.Generic;
using Domain;

namespace DomainImp
{
    class WarriorNoble : INoble
    {
        private List<INobleObserver> observers = new List<INobleObserver>();
        private int worksPublished;
        private int skill;
        private Random random = new Random();

        public int ID { get; set; }

        public int InstituteId { get; set; }

        public string Title { get; set; }

        public int Skill
        {
            get { return skill; }
            set
            {
                skill = value;
                NotifyObservers();
            }
        }

        public int WorksPublished
        {
            get { return worksPublished; }
            set
            {
                worksPublished = value;
                NotifyObservers();
            }
        }

        public WarriorNoble(string title, int instituteId)
        {
            this.Title = title;
            this.InstituteId = instituteId;
            this.Skill = random.Next(25, 75);
            this.WorksPublished = 0;
        }

        public void AddObserver(INobleObserver observer)
        {
            observers.Add(observer);
        }

        public void NotifyObservers()
        {
            foreach(INobleObserver observer in observers)
            {
                observer.Notify(this);
            }
        }
    }
}
