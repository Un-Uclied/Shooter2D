using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{   

    [Header("References")]
    [SerializeField] private int level0Score;
    [SerializeField] private int level1Score;
    [SerializeField] private int level2Score;
    [SerializeField] private int level3Score;
    [SerializeField] private int overHealBonusScore;
    [SerializeField] private int timeScore;
    [SerializeField] private int levelUpScore;
    public long score;
    float scoreTime = 1;

    public UnityEvent<int, string> ScoreUp;

    private void Start(){
        FindAnyObjectByType<VaccineHealth>().OnPlayerOverHealed.AddListener(OnPlayerOverHealed);
        EnemySpawner.Instance.OnLevelChanged.AddListener(OnLevelChanged);
    }

    private void Update() {
        scoreTime -= Time.deltaTime;
        if (scoreTime <= 0){
            AddScore(timeScore, "Time Score");
            scoreTime = 1;
        }   
    }

    private void OnPlayerOverHealed(){
        AddScore(overHealBonusScore, "Over Healed Bonus!");
    }

    public void OnEnemyDied(int enemyNum){
        switch(enemyNum){
            case 0: // Bacteria
                AddScore(level0Score, "Destroyed Vacteria Lv.1");
                break;
            case 1: // Lazer
                AddScore(level1Score, "Destroyed Vacteria Lv.2");
                break;
            case 2: // CrazyLazer
                AddScore(level2Score, "Destroyed Vacteria Lv.3");
                break;
            case 3: // Follower
                AddScore(level3Score, "Destroyed Vacteria Lv.4");
                break;
            case 4: // Boss
                FindAnyObjectByType<UIManager>().OnBossDed();
                break;
        }
    }

    public void OnLevelChanged(){
        AddScore(levelUpScore, "Level Up!");
    }

    private void AddScore(int amt, string reason){
        score += amt;
        ScoreUp?.Invoke(amt, reason);
    }
}
