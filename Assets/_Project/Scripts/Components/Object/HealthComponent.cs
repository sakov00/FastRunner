namespace Assets._Project.Scripts.Components.Object
{
    internal struct HealthComponent
    {
        private float healthPoints;
        public float HealthPoints
        {
            get { return healthPoints; }
            set
            {
                if (value < HealthPointsMin)
                    healthPoints = HealthPointsMin;
                if (value > HealthPointsMax)
                    healthPoints = HealthPointsMax;
                else
                    healthPoints = value;
            }
        }
        public float HealthPointsMin { get; set; }
        public float HealthPointsMax { get; set; }
        public float CurrentDamageCoolDown { get; set; }
        public float DamageCoolDown { get; set; }
    }
}
