using System;
namespace BeehiveManagementSystem
{
    static class HoneyVault
    {
        private const float NECTAR_CONVERSION_RATIO = .19f  ;
        private const float LOW_LEVEL_WARNING = 10f;
        private static float honey = 25f;
        private static float nectar = 100f;

        public static string StatusReport
        {
            get
            {
                string statusString = $"Vault Report:\n{honey} unit of Honey\n{nectar} units of Nectar";
                if (honey < LOW_LEVEL_WARNING) statusString += "\nLOW LEVEL WARNING - ADD A HONEY MANUFACTURER";
                if (nectar < LOW_LEVEL_WARNING) statusString += "\nLOW LEVEL WARNING - ADD A NECTAR COLLECTOR";
                return statusString;
            }

        }

        public static void CollectNecar(float amount)
        {
            if (amount > 0f) nectar += amount;

        }

        public static void ConvertNectarToHoney(float amount)
        {
            if(nectar < amount)
            {
                honey += NECTAR_CONVERSION_RATIO * nectar;
                nectar = 0;
            }
            else
            {
                nectar -= amount;
                honey += NECTAR_CONVERSION_RATIO * amount;
            }
        }

        public static bool ConsumeHoney(float amount)
        {
            if(amount <= honey)
            {
                honey -= amount;
                return true;
            }
            return false;
        }

    }
}
