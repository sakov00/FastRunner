using Assets._Project.Scripts.Components.Object;
using Assets._Project.Scripts.Components.Player;
using Leopotam.Ecs;

public class StonesAttackSystem : IEcsRunSystem
{
    private readonly EcsFilter<PlayerComponent, ColliderComponent> filter = null;

    public void Run()
    {
        foreach (var entityIndex in filter)
        {
            ref var playerComponent = ref filter.Get1(entityIndex);
            ref var colliderComponent = ref filter.Get2(entityIndex);
        }
    }
}
