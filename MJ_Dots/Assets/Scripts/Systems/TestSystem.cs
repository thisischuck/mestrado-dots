using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public class TestSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle j)
    {
        Entities.ForEach(( in Translation t) =>
        {
        }).Run();
        return default;
    }
}