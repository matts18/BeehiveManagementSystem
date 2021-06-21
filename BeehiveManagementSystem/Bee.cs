using System;
namespace BeehiveManagementSystem
{
    abstract class Bee
    {
        public string Job { get; private set; }
        public abstract float CostPerShift { get; }

        public Bee(string job)
        {
            Job = job;
        }

        public void WorkTheNextShift()
        {
            if (HoneyVault.ConsumeHoney(CostPerShift)) DoJob();
        }

        protected abstract void DoJob();

    }
}
