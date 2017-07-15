namespace Domain
{
    public interface INoble
    {
        int ID { get; set; }
        int InstituteId { get; set; }
        string Title { get; set; }
        int Skill { get; set; }
        int WorksPublished { get; set; }
        void NotifyObservers();
        void AddObserver(INobleObserver observer);
    }
}

