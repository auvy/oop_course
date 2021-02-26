using System;
using System.Collections.Generic;
using System.Linq;

namespace task2 {

    //PROXY
    abstract class AbstHolding
    {
        public string name;     
        protected long treasury;
        protected List<Subsidiary> shares = new List<Subsidiary>(){};
        protected List<ProjectLocation> projects = new List<ProjectLocation>(){};
        public abstract bool TakeFromTreasury(long cost);
        public abstract void GiveToTreasury(long money);
        public abstract bool GiveWorktoSub(Material material, int amount);

    }

    class Holding : AbstHolding
    {
        public Holding(){}
        public Holding(string name, long treasures, List<Subsidiary> shares, List<ProjectLocation> projects)
        {   
            this.name = name;
            this.treasury = treasures;
            this.shares = shares;
            this.projects = projects;
        }

        public long GetTreasureSize()
        {
            return treasury;
        }
        public override bool TakeFromTreasury(long cost)
        {

            treasury -= cost;
            return true;
        }
        public override void GiveToTreasury(long money)
        {
            treasury += money;
        }

        public int GiveSubLength()
        {
            return shares.Count;
        }

        public int GiveProjLength()
        {
            return projects.Count;
        }

        public Subsidiary GetShareByIndex(int index)
        {
            return shares.ElementAt(index);
        }

        public ProjectLocation GetProjectByIndex(int index)
        {
            return projects.ElementAt(index);
        }

        public int AddShare(Subsidiary sub)
        {
            if(shares.Count == 0)
            {
                shares.Add(sub);
            }
            else
            {
                int index = 0;
                for(index = 0; index < shares.Count; index++)
                {
                    if(sub.name == shares.ElementAt(index).name) break;
                }
                if(index < shares.Count) shares.ElementAt(index).parentShare += sub.parentShare;
                else shares.Add(sub);
            }
            Console.WriteLine($"\nWelcome {sub.name} to the family!");
            return shares.IndexOf(sub);
        }

        public int AddProject(ProjectLocation p)
        {
            projects.Add(p);
            return projects.IndexOf(p);
        }

        public Subsidiary RemoveShare(Subsidiary sub)
        {
            shares.Remove(sub);
            Console.WriteLine($"\nGoodbye, {sub.name}!");
            return sub;
        }

        public ProjectLocation RemoveProject(ProjectLocation sub)
        {
            projects.Remove(sub);
            return sub;
        }
        public void PrintInfo()
        {
            Console.WriteLine($"'{name}', {treasury} left.");
        }

        public void PrintSubs()
        {
            int i = 1;
            foreach(Subsidiary s in shares)
            {
                Console.Write($"[{i}] '{s.name}' of {s.material}, shared {s.parentShare}, exp of {s.experience}. ");
                if(!s.available) Console.Write("Busy with work.\n");
                else Console.Write("\n");
                i++;
            }
        }
        public void PrintProjects()
        {
            int i = 1;
            foreach(ProjectLocation s in projects)
            {
                Console.Write($"[{i}] '{s.project.name}'; Money to spend: Brick {s.project.resources[0]}, Wood {s.project.resources[1]}, Concrete {s.project.resources[2]}, Rock {s.project.resources[3]}, Metal {s.project.resources[4]}, Workers {s.project.resources[5]}. SellPrice: {s.GetSelfValue()}, ");
                s.project.State.PrintState();
                i++;
            }
        }

        public void AddNewSuccessor(Subsidiary newSub)
        {
            if(shares.Count > 0)
            {
                for(int i = shares.Count - 1; i > -1; i--)
                {
                    if(shares.ElementAt(i).material == newSub.material)
                    {
                        shares.ElementAt(i).Successor = newSub;
                        Console.WriteLine($"Successor of '{shares.ElementAt(i).name}' is now '{shares.ElementAt(i).Successor.name}'.");
                        break;
                    }
                }
            }
            else Console.WriteLine($"It's a new first sub in your list!");
        }
        public void RewireSuccessor(Subsidiary successor)
        {
            Subsidiary childless = null;
            for(int i = 0; i < shares.Count; i++)
            {
                if(shares.ElementAt(i).Successor == successor && childless == null)
                {
                    childless = shares.ElementAt(i);
                }
                else if(childless != null && childless.material == shares.ElementAt(i).material)
                {
                    childless.Successor = shares.ElementAt(i);
                    Console.WriteLine($"Successor of '{childless.name}' is now '{childless.Successor.name}'.");
                    break;
                }
            }
        }
        public override bool GiveWorktoSub(Material material, int amount)
        {
            if(shares == null) Console.WriteLine("You must buy a share first.");
            else if(shares.Count > 0)
            {
                for(int i = 0; i < shares.Count; i++)
                {
                    if(shares[i].material == material)
                    {
                        Console.WriteLine($"Handing job to {shares[i].name}...");
                        return shares[i].ManufactureMaterial(amount);
                    }
                }
                Console.WriteLine($"You don't have a company that works with {material}.");
            }
            return false;
        }



    }


    class HoldingProxy : AbstHolding
    {
        public Holding holding = new Holding();

        public HoldingProxy(Holding hold)
        {
            this.holding = hold;
        }
        public override bool TakeFromTreasury(long cost)
        {
            if(cost > holding.GetTreasureSize())
            {
                Console.WriteLine($"You can't afford getting in debt. Treasury is at {holding.GetTreasureSize()}.");
                return false;
            }
            return holding.TakeFromTreasury(cost);
        }
        public override bool GiveWorktoSub(Material material, int amount)
        {
            return holding.GiveWorktoSub(material, amount);
        }
        public override void GiveToTreasury(long money)
        {
            holding.GiveToTreasury(money);
        }

    }
}