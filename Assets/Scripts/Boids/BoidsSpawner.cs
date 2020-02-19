using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoidsSpawner : MonoBehaviour
{
    public GameObject boidsPrefab;

    public float spawnRadius = 10;

    [Button("Spawn Boids")]
    public void SpawnBoids(int amount = 10)
    {
        StartCoroutine(SpawnBoidsCoroutine(amount));
    }

    private IEnumerator SpawnBoidsCoroutine(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            Instantiate(boidsPrefab, Random.insideUnitSphere * spawnRadius, Random.rotation);
            yield return 0;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color32(0, 100, 100, 50);
        Gizmos.DrawSphere(transform.position, spawnRadius);
    }
}
