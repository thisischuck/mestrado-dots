using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;

public class RotationSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        Entities.ForEach((ref Rotation rotation,in RotationSpeedData rotationData) => {

            rotation.Value = math.mul(rotation.Value,quaternion.RotateX(math.radians(rotationData.speed * deltaTime)));
            rotation.Value = math.mul(rotation.Value,quaternion.RotateY(math.radians(rotationData.speed * deltaTime)));
            rotation.Value = math.mul(rotation.Value,quaternion.RotateZ(math.radians(rotationData.speed * deltaTime)));

        }).Run();

        return default;
    }
}
