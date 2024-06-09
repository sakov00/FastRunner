namespace Assets._Project.Scripts.Components.Abilities.Interfaces
{
    internal interface IProlongedAbilityComponent : IAbilityComponent
    {
        public float CurrentWorkTime { get; set; }
        public bool IsActive { get; set; }
    }
}
