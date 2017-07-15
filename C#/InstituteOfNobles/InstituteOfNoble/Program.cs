using UIImp;
using Controller;
using DomainImp;
using RepositoryImp;

namespace InstituteOfNoble
{
    class Program
    {
        static void Main(string[] args)
        {
            UIFactory UIFactory = new UIFactory(new UIDialog(), new NobleChangesReporter(), new InstituteChangesReporter());
            IController controller = new DefaultController(new WarFactory(), new NobleRepository(), new InstituteRepository(), 
                                                           UIFactory, new DefaultCommandProcessor());
            controller.StartCourotine();
        }
    }
}