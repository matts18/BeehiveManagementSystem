using System;
namespace BeehiveManagementSystem
{
    class EggCare : Bee
    {
        public const float CARE_PROGRESS_PER_SHIFT = 0.15f;
        public override float CostPerShift { get { return 1.35f; } }
        private Queen queenBee;

        public EggCare(Queen queen) : base("Egg Care")
        {
            queenBee = queen;
        }

        protected override void DoJob()
        {
            queenBee.CareForEggs(CARE_PROGRESS_PER_SHIFT);
        }
    }
}
