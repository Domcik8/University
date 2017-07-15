namespace Domain
{
    public interface IInstitute
    {
        int ID { get; set; }
        string Name { get; set; }
        int Influence { get; set; }
        void NotifyObservers();
        void AddObserver(IInstituteObserver observer);
    }
}
