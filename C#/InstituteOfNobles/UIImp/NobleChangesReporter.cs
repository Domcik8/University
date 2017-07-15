using System;
using Domain;

namespace UIImp
{
    public class NobleChangesReporter : INobleObserver
    {
        public void Notify(INoble noble)
        {
            Console.WriteLine(noble.ID + ". " + noble.Title + " skill: " + noble.Skill + ", works published: " + noble.WorksPublished + "\n");
        }
    }
}
