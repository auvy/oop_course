namespace task2 {


    interface IObserver 
    {   
        void Update();
        void UpdateShare(decimal share);
    }


    class Manufactor : IObserver
    {
        public string name;
        public Material material;
        public decimal freeShare;
        public decimal experience;
        public Manufactor(string name, Material mat, decimal freeShare, decimal eff)
        {
            this.name = name;
            this.material = mat;
            this.freeShare = freeShare;
            this.experience = eff;
        }

        public void Update()
        {
            experience += 0.1m;
        }

        public void UpdateShare(decimal share)
        {
            freeShare+=share;
        }

        public Manufactor()
        {}
    
        public Subsidiary generateShare(decimal share)
        {
            this.freeShare -= share;
            return new Subsidiary(this.name, this.material, this.freeShare, this.experience, share, this);
        }   

        public void RaiseExperience(decimal exp)
        {
            experience += exp;
        }


    }
}