using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public enum ResultType{
    Won, Lost
}

public class UIManager : MonoBehaviour
{   
    public bool isEndless;
    [Header("References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI plrHealthTxt;
    [SerializeField] private TextMeshProUGUI bossHealthTxt;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private Transform[] hearts;

    [SerializeField] private Transform 한줄요약;
    [SerializeField] private Transform resultMenu;

    [Header("Audio")]
    [SerializeField] private AudioClip pauseAudio;
    [SerializeField] private AudioClip resumeAudio;
    [SerializeField] private AudioClip restartAudio;
    [SerializeField] private AudioClip gameQuitAudio;
    [SerializeField] private AudioClip enterEndlessAudio;

    [Space]
    [SerializeField] private AudioClip lostAudio;
    [SerializeField] private AudioClip wonAudio;


    private Transform plr;
    private bool paused = false;
    
    private void Start(){
        plr = FindAnyObjectByType<VaccineHealth>().transform;
        plr.GetComponent<VaccineHealth>().OnPlayerDied.AddListener(OnPlayerDied);
        plr.GetComponent<VaccineHealth>().OnPlayerHealthChanged.AddListener(OnPlayerHealthChanged);
        plr.GetComponent<VaccineHealth>().OnPlayerOverHealed.AddListener(OnPlayerOverHealed);
        EnemySpawner.Instance.OnBossSpawned.AddListener(OnBossSpawned);
        EnemySpawner.Instance.OnLevelChanged.AddListener(OnLevelChanged);

        ScoreManager.Instance.ScoreUp.AddListener(OnScoreChanged);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (paused) return;
            PauseGame();
            pauseMenu.SetActive(true);
        }
    }

    public void OnPlayerDied(){
        Time.timeScale = 0;
        EndGame(ResultType.Lost);
    }

    public void OnPlayerHealthChanged(){
        plrHealthTxt.text = plr.GetComponent<VaccineHealth>().health.ToString();
        OnHurted();
    }

    public void OnPlayerOverHealed(){
        
    }

    public void OnRestartKeyStarted(InputAction.CallbackContext context){
        // if (!context.started) return;
        // Restart();
    }

    public void OnBossSpawned(GameObject boss){
        EnemyHealth bossHealth = boss.GetComponent<EnemyHealth>();
        StartCoroutine(Loop());

        IEnumerator Loop(){
            while(true){
                if (bossHealth == null) break;
                bossHealthTxt.text = bossHealth.health.ToString();
                yield return null;
            }
            // BossDied
        } 
    }

    private void PauseGame(){
        Camera.main.transform.GetComponent<AudioListener>().enabled = false;
        DisableAllAudios();
        paused = true;
        Time.timeScale = 0;
        SFXManager.Instance.PlayAudio(pauseAudio);
    }

    public void ResumeGame(){
        Camera.main.transform.GetComponent<AudioListener>().enabled = true;
        EnableAllAudios();
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        SFXManager.Instance.PlayAudio(resumeAudio);
    }

    public async void ExitGame(){
        Camera.main.transform.GetComponent<AudioListener>().enabled = true;
        SFXManager.Instance.PlayAudio(gameQuitAudio);
        await Task.Delay(200);
        Application.Quit();
    }

    public async void ResartGame(){
        Camera.main.transform.GetComponent<AudioListener>().enabled = true;
        SFXManager.Instance.PlayAudio(restartAudio);
        await Task.Delay(500);
        paused = false;
        SceneLoader.Instance.LoadScene();
        Time.timeScale = 1;
    }

    public async void EnterEndlessModeInLevelMode(){
        SFXManager.Instance.PlayAudio(enterEndlessAudio);
        await Task.Delay(500);
        paused = false;
        SceneLoader.Instance.LoadEndLess();
        Time.timeScale = 1;
    }

    public async void EnterLevelModeInEndlessMode(){
        SFXManager.Instance.PlayAudio(restartAudio);
        await Task.Delay(500);
        paused = false;
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1;
    } 

    public void OnLevelChanged(){
        levelTxt.text = $"LEVEL{(EnemySpawner.Instance.currLevel + 1).ToString()}";
    }

    public void OnScoreChanged(int amt, string reason){
        scoreTxt.DOComplete();
        scoreTxt.text = ScoreManager.Instance.score.ToString();
        scoreTxt.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        scoreTxt.transform.DOScale(1, .5f);
    }

    public void OnHurted(){
        foreach(Transform heart in hearts){
            heart.DOComplete();
        }

        foreach(Transform heart in hearts){
            heart.transform.localScale = new Vector3(.65f, .65f, .65f);
            heart.transform.DOScale(.45f, .5f);
        }
    }

    public void OnBossDed(){
        Debug.Log("BossDied!");
        Time.timeScale = 0;
        EndGame(ResultType.Won);
    }

    private async void EndGame(ResultType type){
        paused = true;

        if (isEndless){ // isEndless
            if (HighScoreManager.Instance.LoadHighScore()[1] < ScoreManager.Instance.score){
                HighScoreManager.Instance.SaveHighScore(HighScoreManager.Instance.LoadHighScore()[0], ScoreManager.Instance.score);
                resultMenu.Find("NewRecord").gameObject.SetActive(true);
            }
            else{
                resultMenu.Find("NewRecord").gameObject.SetActive(false);
            }
        }
        else{ // levelMode
            if (HighScoreManager.Instance.LoadHighScore()[0] < ScoreManager.Instance.score){
                HighScoreManager.Instance.SaveHighScore(ScoreManager.Instance.score, HighScoreManager.Instance.LoadHighScore()[1]); // Save HighScore
                resultMenu.Find("NewRecord").gameObject.SetActive(true);
            }
            else{
                resultMenu.Find("NewRecord").gameObject.SetActive(false);
            }
        }
        
        if (type == ResultType.Won){ // Won!
            SFXManager.Instance.PlayAudio(wonAudio);
            한줄요약.Find("Claps").gameObject.SetActive(true);
            한줄요약.Find("RESULT").GetComponent<TextMeshProUGUI>().text = "YOU WON!";
        }
        else{ // Ded Lmao
            SFXManager.Instance.PlayAudio(lostAudio);
            한줄요약.Find("Claps").gameObject.SetActive(false);
            한줄요약.Find("RESULT").GetComponent<TextMeshProUGUI>().text = "YOU DIED.\nLMAO";
        }
        한줄요약.gameObject.SetActive(true);
        await Task.Delay(3000);
        한줄요약.gameObject.SetActive(false);

        resultMenu.Find("FinalScore").GetComponent<TextMeshProUGUI>().text = $"SCORE: {ScoreManager.Instance.score}";

        resultMenu.gameObject.SetActive(true);
    }

    private void DisableAllAudios(){
        foreach(Transform audioObj in Camera.main.transform){
            audioObj.GetComponent<AudioSource>().enabled = false;
        }
    }

    private void EnableAllAudios(){
        foreach(Transform audioObj in Camera.main.transform){
            audioObj.GetComponent<AudioSource>().enabled = true;
        }
    }

}