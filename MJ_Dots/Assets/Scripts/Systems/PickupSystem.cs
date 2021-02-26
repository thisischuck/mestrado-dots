using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;

public class PickupSystem : JobComponentSystem
{
    private BeginFixedStepSimulationEntityCommandBufferSystem bufferSystem;
    private BuildPhysicsWorld buildPhysicsWorld;
    private StepPhysicsWorld stepPhysicsWorld;

    protected override void OnCreate()
    {
        bufferSystem = World.GetOrCreateSystem<BeginFixedStepSimulationEntityCommandBufferSystem>();
        buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        TriggerJob triggerJob = new TriggerJob{
            speedEntities = GetComponentDataFromEntity<SpeedData>(),
            entitiesToDelete = GetComponentDataFromEntity<DeleteTag>(),
            commandBuffer = bufferSystem.CreateCommandBuffer()
        };
        return triggerJob.Schedule(stepPhysicsWorld.Simulation,ref buildPhysicsWorld.PhysicsWorld,inputDeps);
    }

    private struct TriggerJob : ITriggerEventsJob{
        
        public ComponentDataFromEntity<SpeedData> speedEntities;
        [ReadOnly] public ComponentDataFromEntity<DeleteTag> entitiesToDelete;

        public EntityCommandBuffer commandBuffer;
        public void Execute(TriggerEvent triggerEvent){
            if(TestEntityTrigger(triggerEvent.EntityA,triggerEvent.EntityB)){
                commandBuffer.AddComponent(triggerEvent.EntityB,new DeleteTag());
            }
            if(TestEntityTrigger(triggerEvent.EntityB,triggerEvent.EntityA)){
                commandBuffer.AddComponent(triggerEvent.EntityA,new DeleteTag());
            }
        }

        private bool TestEntityTrigger(Entity entityA,Entity entityB){
            if(speedEntities.HasComponent(entityA)){
                if (entitiesToDelete.HasComponent(entityB))
                {
                    return false;
                }
                return true;
            }
            return false;

        }
    }
}
