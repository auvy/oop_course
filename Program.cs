using System;
using System.Collections.Generic;

namespace task2 {
    class Program
    {
        public enum action
        {
            exit, main, stocks, stocks_purchase, stocks_sell, projects, projects_start, projects_manage
        }

        protected static void prepare()
        {
            Console.Clear();
            Console.WriteLine("");
        }

        public static action MenuSwitchChar(char input, action menupart)
        {
            if(menupart == action.main)
            {
                if (input == '0')       return action.exit;
                else if (input == '1')  return action.stocks;
                else if (input == '2')  return action.projects;
            }
            else if(menupart == action.stocks)
            {
                if (input == '0')       return action.main;
                else if (input == '1')  return action.stocks_purchase;
                else if (input == '2')  return action.stocks_sell;
            }
            else if(menupart == action.projects)
            {
                if (input == '0')       return action.main;
                else if (input == '1')  return action.projects_start;
                else if (input == '2')  return action.projects_manage;
            }
            return action.main;
        }

        public static int IndexChoice(int upperBound, int lowerBound)
        {
            string inputString = Console.ReadLine();
            int iee = 0;
            bool check = int.TryParse(inputString, out iee);

            while(!check || (iee != 0 && (iee > upperBound || iee < lowerBound)))
            {
                Console.Write("Enter correct index.\n> ");
                check = int.TryParse(Console.ReadLine(), out iee);
            }
            return iee;
        }

        static void Main()
        {
            AllManufactors mdb = new AllManufactors(new Manufactor[]
            {
                new Manufactor("Heavy Buns",               Material.metal,    0.97m, 0.03m),
                new Manufactor("Rock and Roll",            Material.rock,     0.00m, 0.43m),
                new Manufactor("Rocksteady",               Material.rock,     0.34m, 0.13m),
                new Manufactor("Weeping Forests",          Material.wood,     0.98m, 0.01m),
                new Manufactor("Ceramicools",              Material.brick,    0.50m, 0.43m),
                new Manufactor("Bricks and Mortars",       Material.brick,    0.43m, 0.51m),
                new Manufactor("Pickle Brick",             Material.brick,    0.21m, 0.67m),
                new Manufactor("Passione Concrete",        Material.concrete, 0.66m, 0.34m),
                new Manufactor("Kobayashi Heavy Minerals", Material.concrete, 0.42m, 0.17m),
                new Manufactor("Concrete Incorporated",    Material.concrete, 0.69m, 0.98m),
                new Manufactor("Eagles of Death Metal",    Material.metal,    0.77m, 0.87m),
                new Manufactor("Harder than Rock",         Material.metal,    0.44m, 0.23m),
                new Manufactor("Softer that Metal",        Material.rock,     0.82m, 0.56m),
                new Manufactor("Okalands",                 Material.wood,     0.82m, 0.16m)
            });

            Console.Write("Name your holding company: ");

            string cname = Console.ReadLine();

            Console.WriteLine("Welcome! Press any key to continue.");

            Holding holding = new Holding(cname, 999999999, new List<Subsidiary>(){}, new List<ProjectLocation>(){});
            HoldingProxy proxy = new HoldingProxy(holding);

            char i = Console.ReadKey().KeyChar;
            char input;
            action curr = action.main;

            while (curr != action.exit)
            {
                while (curr == action.main)
                {                
                    prepare();
                    proxy.holding.PrintInfo();
                    Console.Write("Press 0 for exit, 1 for stocks and 2 for projects\n> ");
                    curr = MenuSwitchChar(Console.ReadKey().KeyChar, curr);
                }
                while (curr == action.stocks)
                {
                    prepare();
                    proxy.holding.PrintInfo();
                    proxy.holding.PrintSubs();
                    Console.Write("Stocks in possession\nPress 1 to buy more\n      2 to sell some\n      0 to go back\n> ");
                    curr = MenuSwitchChar(Console.ReadKey().KeyChar, curr);
                }
                while (curr == action.projects)
                {
                    prepare();
                    proxy.holding.PrintProjects();
                    Console.Write("All your current projects\nPress 1 to start a new project\n      2 to manage projects\n      0 to go back\n> ");
                    curr = MenuSwitchChar(Console.ReadKey().KeyChar, curr);
                }

                while (curr == action.stocks_purchase)
                {
                    prepare();
                    proxy.holding.PrintInfo();
                    Manufactor chosen;
                    mdb.printManufactors();
                    Console.Write("All available companies. Print an index to choose, print 0 to go back and press Enter.\n> ");
                    
                    string inputString = Console.ReadLine();
                    int index;
                    bool check = int.TryParse(inputString, out index);

                    while(!check || (index!= 0 && ((index > mdb.GetAccess().Length || index < 0 || mdb.GetAccess()[index-1].freeShare == 0.00m))))
                    {
                        Console.Write("Enter correct index.\n> ");
                        check = int.TryParse(Console.ReadLine(), out index);
                    }
                    if(index == 0)
                    {
                        curr = action.stocks;
                        break;
                    }

                    chosen = mdb.GetAccess()[index-1];

                    Console.Write($"You chose {chosen.name}. Now print % of stocks and press Enter. (available: {chosen.freeShare})\n> "); 
                    
                    index = IndexChoice((int) (chosen.freeShare * 100), 0);

                    if(index == 0)
                    {
                        curr = action.stocks;
                        break;
                    }             

                    long cost = index * MaterialsPrice.getPrice(chosen.material) * (int) (1 + chosen.experience) * 1000;
                    Console.Write($"This will cost you {cost} money. Press 0 for NO, 1 for YES.\n> ");
                    
                    input = Console.ReadKey().KeyChar;
                    if(input == '0')
                    {
                        curr = action.stocks;
                        break;
                    }
                    else if (input == '1')
                    {
                        Subsidiary sub = chosen.generateShare((decimal) index / 100);
                        if(proxy.TakeFromTreasury(cost))
                        {
                            proxy.holding.AddNewSuccessor(sub);
                            proxy.holding.AddShare(sub);
                        }
                        else Console.WriteLine("I guess you're too poor.");
                    }

                    Console.WriteLine("\nPress any key to continue.");
                    i = Console.ReadKey().KeyChar;

                }

                while (curr == action.stocks_sell)
                {
                    prepare();
                    proxy.holding.PrintInfo();
                    Subsidiary chosen;
                    proxy.holding.PrintSubs();
                    Console.Write("All available companies. Print an index to choose, print 0 to go back and press Enter.\n> ");

                    int index = IndexChoice(proxy.holding.GiveSubLength(), 0);

                    if(index == 0)
                    {
                        curr = action.stocks;
                        break;
                    }

                    chosen = proxy.holding.GetShareByIndex(index - 1);

                    Console.Write($"You chose {chosen.name}. Now print % of stocks and press Enter. (available: {chosen.parentShare}).\n> "); 

                    index = IndexChoice((int) (chosen.parentShare * 100), 0);

                    if(index == 0)
                    {
                        curr = action.stocks;
                        break;
                    }                    

                    long cost = index * MaterialsPrice.getPrice(chosen.material) * (int) (1 + chosen.experience) * 1000;
                    Console.Write($"This will bring you {cost} money. ");
                    if((decimal) index / 100 == chosen.parentShare) Console.Write($"Warning: You will lose possession of all your '{chosen.name}' stocks. ");
                    Console.Write("Press 0 for NO, 1 for YES.\n> ");
                    input = Console.ReadKey().KeyChar;
                    if (input == '0') curr = action.stocks;
                    else if (input == '1')
                    {
                        decimal soldShare = (decimal) index / 100;
                        chosen.parentShare -= soldShare;
                        chosen.UpdateObserverShare(soldShare);

                        if(chosen.parentShare == 0)
                        {                                
                            proxy.holding.RemoveShare(chosen);
                            proxy.holding.RewireSuccessor(chosen);
                        }

                        proxy.holding.GiveToTreasury(cost);
                    }

                    Console.WriteLine("\nPress any key to continue.");
                    i = Console.ReadKey().KeyChar;
                }


                while (curr == action.projects_start)
                {
                    ProjectCreator factory;
                    IBuildingTechnology techy;
                    ProjectLocation plocation;
                    prepare();
                    
                    string name;
                    Console.Write("Enter name> ");

                    name = Console.ReadLine();
                    
                    Console.Write("Office, Zone or Complex? (1, 2, 3) or 0 for exit.\n> ");
                    input = Console.ReadKey().KeyChar;

                    while(Convert.ToInt32(input) - 48 < 0 || Convert.ToInt32(input) - 48 > 3)
                    {
                        Console.WriteLine(Convert.ToInt32(input));
                        Console.Write("Office, Zone or Complex? (1, 2, 3) or 0 for exit.\n> ");
                        input = Console.ReadKey().KeyChar;
                    }
                    if (input == '0') 
                    {
                        curr = action.projects;
                        break;
                    }
                    if(input == '1') factory = new OfficeCreator();
                    else if(input == '2') factory = new ZoneCreator();
                    else factory = new ComplexCreator();

                    Console.Write("\nSmallpart, Cast-in-place or Wireframe? (1, 2, 3) or 0 for exit.\n> ");
                    input = Console.ReadKey().KeyChar;

                    while(Convert.ToInt32(input) - 48 < 0 || Convert.ToInt32(input) - 48 > 3)
                    {
                        Console.Write("Smallpart, Cast-in-place or Wireframe? (1, 2, 3) or 0 for exit.\n> ");
                        input = Console.ReadKey().KeyChar;
                    }
                    if (input == '0') 
                    {
                        curr = action.projects;
                        break;
                    }
                    if(input == '1') techy = new SmallpieceTechology();
                    else if(input == '2') techy = new CastInPlaceTechnology();
                    else techy = new FrameworkTechnology();

                    Console.Write("\nOutskirts, Commute or Downtown? (1, 2, 3) or 0 for exit.\n> ");
                    input = Console.ReadKey().KeyChar;
            
                    while(Convert.ToInt32(input) - 48 < 0 || Convert.ToInt32(input) - 48 > 3)
                    {
                        Console.Write("Outskirts, Commute or Downtown? (1, 2, 3) or 0 for exit.\n> ");
                        input = Console.ReadKey().KeyChar;
                    }
                    if (input == '0') 
                    {
                        curr = action.projects;
                        break;
                    }
                    if(input == '1') plocation = new OutskirtsLocation();
                    else if(input == '2') plocation = new CommuterLocation();
                    else plocation = new DowntownLocation();

                    Project proj = factory.Create(name, proxy, techy);
                    plocation.AssignProject(proj);
                    proxy.holding.AddProject(plocation);
                    curr = action.projects;

                    Console.WriteLine("\nPress any key to continue.");
                    i = Console.ReadKey().KeyChar;
                }
                while (curr == action.projects_manage)
                {
                    prepare();
                    proxy.holding.PrintProjects();
                    Console.Write("All your projects. Print an index to choose, print 0 to go back and press Enter.\n> ");

                    int index = IndexChoice(proxy.holding.GiveProjLength(), 0);

                    if(index == 0)
                    {
                        curr = action.stocks;
                        break;
                    }

                    ProjectLocation chosen = proxy.holding.GetProjectByIndex(index - 1);
                    Console.Write($"You chose {chosen.project.name}. ");
                    chosen.project.State.PrintCommand();
                    Console.Write(" or press 0 to go back.\n> ");

                    input = Console.ReadKey().KeyChar;

                    while(input != '0' && input != '1')
                    {
                        Console.Write("Please, enter correct value.\n> ");
                        input = Console.ReadKey().KeyChar;
                    }
                    if(input == '0')
                    {
                        curr = action.projects;
                        break;
                    }             
                    if(chosen.DevelopProject() == 3)
                    {
                        proxy.holding.GiveToTreasury(chosen.GetSelfValue());
                        proxy.holding.RemoveProject(chosen);
                        Console.WriteLine($"{chosen.project.name} was sold!");
                    }
                    
                    Console.WriteLine("\nPress any key to continue.");
                    i = Console.ReadKey().KeyChar;
                }
            }
        }
    }
}

