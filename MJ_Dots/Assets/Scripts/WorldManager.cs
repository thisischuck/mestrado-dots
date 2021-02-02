using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

[ExecuteInEditMode]
public class WorldManager : MonoBehaviour
{
    EntityManager entityManager;
    World world;

    // Start is called before the first frame update
    void Start()
    {
        world = World.DefaultGameObjectInjectionWorld;
        entityManager = world.EntityManager;
    }
}
