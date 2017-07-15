namespace Domain
{
    public interface IDomainFactory
    {
        INoble CreateNoble(string title, int instituteID);
        IInstitute CreateInstitute(string name);
    }
}
