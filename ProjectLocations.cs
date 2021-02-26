namespace task2
{
    //DECORATOR
    abstract class ProjectLocation
    {
        public Project project;

        public ProjectLocation(){}
        public ProjectLocation(Project p)
        {
            this.project = p;
        }
        public void AssignProject(Project p)
        {
            this.project = p;
        }
        public abstract long GetSelfValue();
        public int DevelopProject()
        {
            return project.DevelopProject();
        }
    }

    class DowntownLocation : ProjectLocation
    {
        public DowntownLocation(){}
        public DowntownLocation(Project p)
        {
            this.project = p;
        }

    
        public override long GetSelfValue()
        {
            return (long) (project.GetSelfValue() * 1.2);
        }
    }
    class CommuterLocation : ProjectLocation
    {
        public CommuterLocation(){}

        public CommuterLocation(Project p)
        {
            this.project = p;
        }
    
        public override long GetSelfValue()
        {
            return (long) (project.GetSelfValue() * 1.15);
        }
    }

    class OutskirtsLocation : ProjectLocation
    {
        public OutskirtsLocation(){}
        public OutskirtsLocation(Project p)
        {
            this.project = p;
        }
    
        public override long GetSelfValue()
        {
            return project.GetSelfValue();
        }
    }
}