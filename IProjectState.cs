using System;
using System.Linq;

namespace task2
{
    //STATE
    interface IProjectState
    {
        int DevelopProject(Project project);
        bool CheckState(Project project);
        void PrintState();
        void PrintCommand();
    }
    
    class NotReadyState : IProjectState
    {
        public void PrintState()
        {
            Console.Write("not ready yet.\n");
        }
        public void PrintCommand()
        {
            Console.Write("It lacks resources, you can gather them by pressing 1");
        }
        public int DevelopProject(Project project)
        {
            project.tech.BuildingUpHouses(project);
            if(CheckState(project)) Console.WriteLine ("It's ready for building!");
            return 0;
        }
        public bool CheckState(Project Project)
        {
            
            bool allZero = Project.resources.All(amount => amount == 0);
            if(allZero) 
            {
                Project.State = new ReadyForBuildingState();
                return true;
            }
            return false;
        }
    

    }
    class ReadyForBuildingState : IProjectState
    {
        public void PrintState()
        {
            Console.Write("ready for building.\n");
        }
        public void PrintCommand()
        {
            Console.Write("It's ready for building, you can build it by pressing 1");
        }

        public int DevelopProject(Project project)
        {
            project.PutEverythingTogether();
            project.State = new ReadyForSaleState();
            return 0;
        }
        public bool CheckState(Project Project)
        {
            return false;
        }


    }
    class ReadyForSaleState : IProjectState
    {
        public void PrintState()
        {
            Console.Write("ready for sale.\n");
        }
        public void PrintCommand()
        {
            Console.Write("It's ready for sale, you can sell it by pressing 1");
        }

        public int DevelopProject(Project project)
        {
            return 3;
        }
        public bool CheckState(Project Project)
        {
            return false;
        }

    }
}