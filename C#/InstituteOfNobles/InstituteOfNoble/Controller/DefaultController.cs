using System;
using System.Collections.Generic;
using Domain;
using Repository;
using UI;
using Commands;

namespace Controller
{
    public class DefaultController : IController
    {
        private IDomainFactory domainFactory;
        private IUIFactory uiFactory;
        private INobleRepository nobleRepository;
        private IInstituteRepository instituteRepository;
        private ICommandProcessor commandProcessor;

        public DefaultController(IDomainFactory domainFactory, INobleRepository nobleRepository, IInstituteRepository instituteRepository, 
                                 IUIFactory uifactory, ICommandProcessor commandProcessor)
        {
            this.domainFactory = domainFactory;
            this.uiFactory = uifactory;
            this.nobleRepository = nobleRepository;
            this.instituteRepository = instituteRepository;
            this.commandProcessor = commandProcessor;
        }

        public void StartCourotine()
        {
            while (ShowMenu());
        }

        private bool ShowMenu()
        {
            string option = uiFactory.uidialog.ShowMenu();
            switch (option)
            {
                case "1":
                    CreateInstitute();
                    break;
                case "2":
                    RegisterNoble();
                    break;
                case "3":
                    ListInstitutes();
                    break;
                case "4":
                    ListNobles();
                    break;
                case "5":
                    TeachNobles();
                    break;
                case "6":
                    WriteDocuments();
                    break;
                case "7":
                    UndoLastOperation();
                    break;
                case "0":
                    return false;
                default:
                    Console.WriteLine("There is no command: " + option);
                    break;
            }
            return true;
        }

        private void CreateInstitute()
        {
            string name = uiFactory.uidialog.ShowDialog("Enter name of the new departament:");
            commandProcessor.Execute(new CreateInstitute(domainFactory, instituteRepository, uiFactory.instituteObserver, name));
        }

        private void RegisterNoble()
        {
            string title = uiFactory.uidialog.ShowDialog("Enter title of the new noble:");
            int id; 
            int.TryParse(uiFactory.uidialog.ShowDialog("Enter id of the institute:"), out id);
            if(1 == commandProcessor.Execute(new RegisterNoble(domainFactory, instituteRepository, nobleRepository, uiFactory.nobleObserver, title, id)))
            {
                uiFactory.uidialog.ShowMonolog("Error: Could not register a noble in specified institute.");
            }
        }

        private void ListInstitutes()
        {
            string text = "";
            Dictionary<int, IInstitute>.ValueCollection institutes = instituteRepository.GetAll();
            foreach (IInstitute institute in institutes)
            {
                text += (institute.ID + ". " + institute.Name + " influence " + institute.Influence + "\n");
            }
            uiFactory.uidialog.ShowMonolog(text);
        }

        private void ListNobles()
        {
            string text = "";
            Dictionary<int, INoble>.ValueCollection nobles = nobleRepository.GetAll();
            foreach (INoble noble in nobles)
            {
                text += (noble.ID + ". " + noble.Title + " from institute of " + instituteRepository.Get(noble.InstituteId).Name
                    + ", skill " + noble.Skill + ", works published " + noble.WorksPublished + "\n");
            }
            uiFactory.uidialog.ShowMonolog(text);
        }

        private void TeachNobles()
        {
            commandProcessor.Execute(new TeachNobles(nobleRepository));
        }

        private void WriteDocuments()
        {
            commandProcessor.Execute(new WriteDocuments(nobleRepository, instituteRepository));
        }

        private void UndoLastOperation()
        {
            commandProcessor.Undo();
        }
    }
}