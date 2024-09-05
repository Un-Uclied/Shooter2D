using UnityEngine;
using System.Collections;

public class ItemSpawner : Singleton<ItemSpawner>
{   
    [Header("References")]
    [SerializeField] private GameObject waterItem;

    [Header("Water Settings")]
    [SerializeField] private Float2D[] levelWaterSpawnRate;

    [Header("Found Points")]
    [SerializeField] private Vector3 topLeft;
    [SerializeField] private Vector3 topRight;
    [SerializeField] private Vector3 bottomLeft;
    [SerializeField] private Vector3 bottomRight;

    Coroutine itemSpawnLoop;

    private void Start(){
        topLeft = Camera.main.ScreenToWorldPoint(Camera.main.ViewportToScreenPoint(new Vector3(0, 1, 0)));
        topRight = Camera.main.ScreenToWorldPoint(Camera.main.ViewportToScreenPoint(new Vector3(1, 1, 0)));
        bottomLeft = Camera.main.ScreenToWorldPoint(Camera.main.ViewportToScreenPoint(new Vector3(0, 0, 0)));
        bottomRight = Camera.main.ScreenToWorldPoint(Camera.main.ViewportToScreenPoint(new Vector3(1, 0, 0)));
        itemSpawnLoop = StartCoroutine(SpawnItemTime());
    }

    private IEnumerator SpawnItemTime(){
        while(true){
            SpawnAtRandPosItem(waterItem);
            yield return new WaitForSeconds(Random.Range(levelWaterSpawnRate[EnemySpawner.Instance.currLevel].array[0], levelWaterSpawnRate[EnemySpawner.Instance.currLevel].array[1]));
        }   
    }

    private void SpawnAtRandPosItem(GameObject item){
        Vector3[] positions = {topLeft, topRight, bottomLeft, bottomRight};
        Vector3 spawnPos = positions[Random.Range(0, positions.Length)];
        GameObject clone = Instantiate(item, new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
    }
}
