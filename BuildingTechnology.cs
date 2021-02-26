namespace task2 {
    //STRATEGY
    interface IBuildingTechnology
    {
        decimal[] getResourcePercentage();
        void BuildingUpHouses(Project p);

    }
    class CastInPlaceTechnology : IBuildingTechnology
    {
        public decimal[] getResourcePercentage()
        {
            return new decimal[] {0, 0, 0.6m, 0.05m, 0.35m, 1};
        }
        public void BuildingUpHouses(Project p)
        {
            if(p.ownerHolding.TakeFromTreasury(p.resources[5])) p.CompleteResource(5);
            if(p.ownerHolding.GiveWorktoSub(Material.concrete, p.resources[2])) p.CompleteResource(2);
            if(p.ownerHolding.GiveWorktoSub(Material.metal, p.resources[4])) p.CompleteResource(4);
            if(p.ownerHolding.GiveWorktoSub(Material.rock, p.resources[3])) p.CompleteResource(3);
        }
    }

    class SmallpieceTechology : IBuildingTechnology
    {
        public decimal[] getResourcePercentage()
        {
            return new decimal[] {0.65m, 0, 0.2m, 0.05m, 0.1m, 1};
        }
        public void BuildingUpHouses(Project p)
        {
            if(p.ownerHolding.GiveWorktoSub(Material.concrete, p.resources[2])) p.CompleteResource(2);
            if(p.ownerHolding.GiveWorktoSub(Material.brick, p.resources[0])) p.CompleteResource(0);
            if(p.ownerHolding.GiveWorktoSub(Material.metal, p.resources[4])) p.CompleteResource(4);
            if(p.ownerHolding.GiveWorktoSub(Material.rock, p.resources[3])) p.CompleteResource(3);
            if(p.ownerHolding.TakeFromTreasury(p.resources[5])) p.CompleteResource(5);
        }
    }

    class FrameworkTechnology : IBuildingTechnology
    {
        public decimal[] getResourcePercentage()
        {
            return new decimal[] {0.3m, 0.3m, 0.25m, 0, 0.15m, 1};
        }
        public void BuildingUpHouses(Project p)
        {
            if(p.ownerHolding.GiveWorktoSub(Material.wood, p.resources[1])) p.CompleteResource(1);
            if(p.ownerHolding.GiveWorktoSub(Material.concrete, p.resources[2])) p.CompleteResource(2);
            if(p.ownerHolding.GiveWorktoSub(Material.metal, p.resources[4])) p.CompleteResource(4);
            if(p.ownerHolding.GiveWorktoSub(Material.brick, p.resources[0])) p.CompleteResource(0);
            if(p.ownerHolding.TakeFromTreasury(p.resources[5])) p.CompleteResource(5);
        }
    }
    


}