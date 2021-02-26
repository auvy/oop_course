using System;
namespace task2 {
    

    class OfficeProject : Project
    {   
        public OfficeProject(string name, HoldingProxy owner, IBuildingTechnology tech)
        {
            int price = 2500;
            this.name = name;
            this.ownerHolding = owner;
            this.tech = tech;
            this.resources = new int[] {   
                MaterialsPrice.getPrice(Material.brick) * price,
                MaterialsPrice.getPrice(Material.wood) * price,
                MaterialsPrice.getPrice(Material.concrete) * price,
                MaterialsPrice.getPrice(Material.rock) * price,
                MaterialsPrice.getPrice(Material.metal) * price,
                price * 50};
        }
        public override void PutEverythingTogether()
        {
            Console.WriteLine("Putting foundation...");
            Console.WriteLine("Piecing blocks...");
            Console.WriteLine("Piecing frames...");
            Console.WriteLine("Ready.");
        }  
    }

    class ZoneProject : Project
    {   
        public ZoneProject(string name, HoldingProxy owner, IBuildingTechnology tech)
        {
            int price = 7500;
            this.name = name;
            this.ownerHolding = owner;
            this.tech = tech;
            this.resources = new int[] {   
                MaterialsPrice.getPrice(Material.brick) * price,
                MaterialsPrice.getPrice(Material.wood) * price,
                MaterialsPrice.getPrice(Material.concrete) * price,
                MaterialsPrice.getPrice(Material.rock) * price,
                MaterialsPrice.getPrice(Material.metal) * price,
                price * 50};
        }
        public override void PutEverythingTogether()
        {
            Console.WriteLine("Putting foundation...");
            Console.WriteLine("Piecing blocks...");
            Console.WriteLine("Marking territory...");
            Console.WriteLine("Ready.");
        }
    }

    class ComplexProject : Project
    {   
        public ComplexProject(string name, HoldingProxy owner, IBuildingTechnology tech)
        {
            int price = 12500;
            this.name = name;
            this.ownerHolding = owner;
            this.tech = tech;
            this.resources = new int[] {   
                MaterialsPrice.getPrice(Material.brick) * price,
                MaterialsPrice.getPrice(Material.wood) * price,
                MaterialsPrice.getPrice(Material.concrete) * price,
                MaterialsPrice.getPrice(Material.rock) * price,
                MaterialsPrice.getPrice(Material.metal) * price,
                price * 50};
        }
        public override void PutEverythingTogether()
        {
            Console.WriteLine("Putting foundation...");
            Console.WriteLine("Marking territory...");
            Console.WriteLine("Placing framework...");
            Console.WriteLine("Covering framework...");
            Console.WriteLine("Repeating a bunch of times...");
            Console.WriteLine("Ready.");
        }
    }

    //FACTORY METHOD
    abstract class ProjectCreator
    {    
        public ProjectCreator (){}
        abstract public Project Create(string name, HoldingProxy owner, IBuildingTechnology tech); //factory method
    }
    class ZoneCreator : ProjectCreator
    {
        public ZoneCreator (){}
    
        public override Project Create(string name, HoldingProxy owner, IBuildingTechnology tech)
        {
            ZoneProject p = new ZoneProject(name, owner, tech);
            decimal[] kek = p.tech.getResourcePercentage();
            int[] prices = {1, 1, 1, 1, 1, 1};
            for(int i = 0; i < kek.Length; i++)
            {
                prices[i] = (int) (kek[i] * p.resources[i]);
            }
            p.resources = prices;
            return p;
        }
    }
    class ComplexCreator : ProjectCreator
    { 
        public ComplexCreator(){}
    
        public override Project Create(string name, HoldingProxy owner, IBuildingTechnology tech)
        {
            ComplexProject p = new ComplexProject(name, owner, tech);
            decimal[] kek = p.tech.getResourcePercentage();
            int[] prices = {1, 1, 1, 1, 1, 1};
            for(int i = 0; i < kek.Length; i++)
            {
                prices[i] = (int) (kek[i] * p.resources[i]);
            }
            p.resources = prices;
            return p;
        }
    }

    class OfficeCreator : ProjectCreator
    { 
        public OfficeCreator(){}
    
        public override Project Create(string name, HoldingProxy owner, IBuildingTechnology tech)
        {
            OfficeProject p = new OfficeProject(name, owner, tech);
            decimal[] kek = p.tech.getResourcePercentage();
            int[] prices = {1, 1, 1, 1, 1, 1};
            for(int i = 0; i < kek.Length; i++)
            {
                prices[i] = (int) (kek[i] * p.resources[i]);
            }
            p.resources = prices;
            return p;
        }
    }
}