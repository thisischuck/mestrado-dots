using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Mathematics;
using UnityEngine;

public class MovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        float2 curInput = new float2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        float jump = Input.GetAxis("Jump");
        Entities.ForEach((ref PhysicsVelocity vel,in SpeedData speedData) => {
            float2 newVel = vel.Linear.xz;

            newVel += curInput * speedData.speed * deltaTime;
            vel.Linear.y += jump * speedData.jump *deltaTime;
            vel.Linear.xz =  newVel;
        }).Run();

        return default;
    }
}
