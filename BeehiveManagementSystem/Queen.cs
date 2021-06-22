using System;
namespace BeehiveManagementSystem
{
    class Queen : Bee
    {
        public const float EGGS_PER_SHIFT = 0.45f;
        public const float HONEY_PER_UNASSIGNED_WORKER = 0.5f;

        public override float CostPerShift { get{ return 2.15f; } }
        private IWorker[] workers;
        private float eggs;
        private float unassignedWorkers = 0;
        public string StatusReport { get { return UpdateStatusReport(); } private set { } }


        public Queen() : base("Queen")
        {
            workers = new IWorker[]{
                new NectarCollector(),
                new HoneyManufacturer(),
                new EggCare(this)
                    };
        }

        public void AssignBee(string jobName)
        {
            switch(jobName)
            {
                case ("Egg Care"):
                    AddWorker(new EggCare(this));
                    break;
                case ("Nectar Collector"):
                    AddWorker(new NectarCollector());
                    break;
                case ("Honey Manufacturer"):
                    AddWorker(new HoneyManufacturer());
                    break;
            }
            
        }

        public void AddWorker(IWorker bee)
        {
            if(unassignedWorkers >= 1)
            {
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = bee;
                unassignedWorkers--;
            }
        }

        protected override void DoJob()
        {
            eggs += EGGS_PER_SHIFT;
            foreach(IWorker bee in workers)
            {
                bee.WorkTheNextShift();
            }
            HoneyVault.ConsumeHoney(HONEY_PER_UNASSIGNED_WORKER * unassignedWorkers);
            StatusReport = UpdateStatusReport();
        }

        private string UpdateStatusReport()
        {
            int nectarCollectorCount = 0;
            int honeyManufacturerCount = 0;
            int eggCareCount = 0;

            string status = HoneyVault.StatusReport;
            status += $"\n\nEgg count: {eggs}\nUnassigned workers: {unassignedWorkers}";

            foreach(IWorker bee in workers)
            {
                switch(bee.Job)
                {
                    case ("Nectar Collector"):
                        nectarCollectorCount++;
                        break;
                    case ("Honey Manufacturer"):
                        honeyManufacturerCount++;
                        break;
                    case ("Egg Care"):
                        eggCareCount++;
                        break;
                }
            }

            status += $"\n{nectarCollectorCount} Nectar Collecter bee";
            if (nectarCollectorCount > 1) status += "s";
            status += $"\n{honeyManufacturerCount} Honey Manufacturer bee";
            if (honeyManufacturerCount > 1) status += "s";
            status += $"\n{eggCareCount} Egg Care bee";
            if (eggCareCount > 1) status += "s";
            status += $"\nTOTAL WORKERS: {eggCareCount + honeyManufacturerCount + nectarCollectorCount}";

            return status;
        }

        public void CareForEggs(float eggsToConvert)
        {
            if(eggs >= eggsToConvert)
            {
                eggs -= eggsToConvert;
                unassignedWorkers += eggsToConvert;
            }
        }
    }
}
