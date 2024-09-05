using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class EnemySpawner : Singleton<EnemySpawner>
{    
    public bool isEndless;
    [Header("Spawned Enemies")]
    [SerializeField] public List<GameObject> spawnedEnemies;
    [Header("References")]
    [SerializeField] private GameObject[] Modules;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private AudioClip levelUpAudio;

    [Header("Enemy Spawn Time")]
    [SerializeField] public float elapsedTime;

    [Space]
    [SerializeField] private float[] levelTime;

    [Space]
    [SerializeField] public int currLevel;
    
    [Header("Found Points")]
    [SerializeField] public Vector3 topLeft;
    [SerializeField] public Vector3 topRight;
    [SerializeField] public Vector3 bottomLeft;
    [SerializeField] public Vector3 bottomRight;

    [Header("Boss")]

    [SerializeField] private bool bossSpawned = false;
    public UnityEvent<GameObject> OnBossSpawned;
    public UnityEvent OnLevelChanged;

    private void Update(){
        if (isEndless) return;
        elapsedTime += Time.deltaTime;
        
        ManageLevel();
        if (currLevel == 4 && !bossSpawned){
            bossSpawned = true;
            SpawnBoss();
        }
    }

    private void Start(){
        topLeft = Camera.main.ScreenToWorldPoint(Camera.main.ViewportToScreenPoint(new Vector3(0, 1, 0)));
        topRight = Camera.main.ScreenToWorldPoint(Camera.main.ViewportToScreenPoint(new Vector3(1, 1, 0)));
        bottomLeft = Camera.main.ScreenToWorldPoint(Camera.main.ViewportToScreenPoint(new Vector3(0, 0, 0)));
        bottomRight = Camera.main.ScreenToWorldPoint(Camera.main.ViewportToScreenPoint(new Vector3(1, 0, 0)));
    }
    
    private void ManageLevel(){
        for(int i = 0; i < levelTime.Length; i++){
            if (elapsedTime >= levelTime[i] && currLevel <= i){
                if (currLevel != i){
                    SFXManager.Instance.PlayAudio(levelUpAudio);
                    currLevel = i;
                    OnLevelChanged?.Invoke();
                }
                
                
            }
        }
    }

    private void SpawnBoss(){
        GameObject boss = Instantiate(bossPrefab, Vector3.zero, Quaternion.identity);
        boss.name = bossPrefab.name;
        OnBossSpawned?.Invoke(boss);
    }
}
