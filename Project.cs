namespace task2 {
    class Project
    {
        public string name;
        public HoldingProxy ownerHolding;
        public int[] resources = {1, 1, 1, 1, 1, 1};
        public IProjectState State = new NotReadyState();
        public IBuildingTechnology tech;
        protected long selfValue = 0;

        public Project(){}
        public Project(string name, HoldingProxy owner, IBuildingTechnology tech)
        {
            this.name = name;
            this.ownerHolding = owner;
            this.tech = tech;
        }

        public long GetSelfValue()
        {
            return this.selfValue;
        }


        public int DevelopProject()
        {
            return State.DevelopProject(this);
        }

        public bool CompleteResource(int index)
        {
            selfValue+=resources[index];
            resources[index] = 0;
            return true;
        }

        public virtual void PutEverythingTogether(){}
    }
}