using System;
using System.Collections.Generic;
using Domain;
using Repository;

namespace Commands
{
    public class TeachNobles : IUndoableCommand
    {
        private Queue<int> oldSkills = new Queue<int>();  
        private Dictionary<int, INoble>.ValueCollection nobles;
        private INobleRepository nobleRepository;
        private Random random = new Random();

        public TeachNobles(INobleRepository nobleRepository)
        {
            this.nobleRepository = nobleRepository;
        }

        public int Execute()
        {
            nobles = nobleRepository.GetAll();

            foreach (INoble noble in nobles)
            {
                oldSkills.Enqueue(noble.Skill);
                int newSkill = random.Next(0, 100);
                noble.Skill += newSkill;
            }
            return 0;
        }

        public void Undo()
        {
            foreach (INoble noble in nobleRepository.GetAll())
            {
                noble.Skill = oldSkills.Dequeue();
            }
        }
    }
}