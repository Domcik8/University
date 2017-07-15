using System;
using System.Collections.Generic;
using System.Diagnostics;
using Domain;

namespace DomainImp
{
    class MageNoble : INoble
    {
        private List<INobleObserver> observers = new List<INobleObserver>();
        private int worksPublished;
        private string element;
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
                if (element == "Fire")
                    if (random.Next(0, 10) >= 7)
                        worksPublished = value + 1;
                    else worksPublished = value;
                else if (worksPublished % 3 == 0)
                {
                    worksPublished = value + 1;
                }
                else worksPublished = value;


                NotifyObservers();
            }
        }

        public MageNoble(string title, int instituteId)
        {
            this.Title = title;
            this.InstituteId = instituteId;
            StackTrace s = new StackTrace();
            this.Skill = random.Next(0, 100);
            this.WorksPublished = 0;

            if (random.Next(0, 5) < 3)
                this.element = "Water";
            else this.element = "Fire";
        }

        public void AddObserver(INobleObserver observer)
        {
            observers.Add(observer);
        }

        public void NotifyObservers()
        {
            foreach (INobleObserver observer in observers)
            {
                observer.Notify(this);
            }
        }
    }
}