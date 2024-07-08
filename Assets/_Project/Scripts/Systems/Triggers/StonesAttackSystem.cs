using Assets._Project.Scripts.Components.Object;
using Assets._Project.Scripts.Components.Player;
using Leopotam.Ecs;

public class StonesAttackSystem : IEcsRunSystem
{
    private readonly EcsFilter<TriggerComponent> filter = null;

    public void Run()
    {
        foreach (var entityIndex in filter)
        {
            ref var triggerComponent = ref filter.Get1(entityIndex);

            if (!triggerComponent.TargetEntity.HasValue)
                continue;

            if (!triggerComponent.SourceEntity.Value.Has<SpawnerComponent>() ||
                !triggerComponent.TargetEntity.Value.Has<PlayerComponent>())
                continue;

            ref var sourceSpawnerComponent = ref triggerComponent.SourceEntity.Value.Get<SpawnerComponent>();
            ref var targetPlayerComponent = ref triggerComponent.TargetEntity.Value.Get<PlayerComponent>();
        }
    }
}
