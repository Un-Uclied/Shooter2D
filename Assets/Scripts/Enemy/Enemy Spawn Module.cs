using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnModule : MonoBehaviour
{   
    
    [Header("Settings")]
    [SerializeField] private GameObject spawnEnemyPrefab;
    [SerializeField] private bool[] spawnableLevels;
    

    [Space]
    [SerializeField] private Float2D[] spawnRateByLevels;

    private void Start(){
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop(){
        yield return new WaitForEndOfFrame();
        while(true){
            yield return null;
            if (spawnableLevels[EnemySpawner.Instance.currLevel] == false){
                continue;
            }
            SpawnAtRandPosEnemy();
            yield return new WaitForSeconds(Random.Range(spawnRateByLevels[EnemySpawner.Instance.currLevel].array[0], spawnRateByLevels[EnemySpawner.Instance.currLevel].array[0]));
        }
    }

    private void SpawnAtRandPosEnemy(){
        Vector3[] positions = {EnemySpawner.Instance.topLeft, EnemySpawner.Instance.topRight, EnemySpawner.Instance.bottomLeft, EnemySpawner.Instance.bottomRight};
        Vector3 spawnPos = positions[Random.Range(0, positions.Length)];
        GameObject clone = Instantiate(spawnEnemyPrefab, new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
        clone.name = spawnEnemyPrefab.name;
        EnemySpawner.Instance.spawnedEnemies.Add(clone);
    }
}
