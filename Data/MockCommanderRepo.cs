using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void createCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
           var commands = new List<Command>
           {
              new Command{ Id = 1 , HowTo="Start Coding  1", Line = "Start Coding here" , Platform="Windows"},
              new Command{ Id = 2 , HowTo="Start Coding 2", Line = "Start Coding here" , Platform="Windows"},
              new Command{ Id = 3 , HowTo="Start Coding  3", Line = "Start Coding here" , Platform="Windows"},
              new Command{ Id = 4 , HowTo="Start Coding 4", Line = "Start Coding here" , Platform="Windows"}
           };
           return commands;
        }

        public Command GetCommandById(int Id)
        {
           return new Command{ Id = 1 , HowTo="Start Coding", Line = "Start Coding here" , Platform="Windows"};          
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
} 