using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : Singleton<MainMenuHandler>
{   
    [Header("References")]
    [SerializeField] private TextMeshProUGUI levelModeHighScore;
    [SerializeField] private TextMeshProUGUI endlessModeHighScore;
    [SerializeField] private AudioClip resumeAudio;
    [SerializeField] private AudioClip gameQuitAudio;
    [SerializeField] private AudioClip enterEndlessAudio;

    bool action = false;

    private void Start(){
        Debug.Log(HighScoreManager.Instance.LoadHighScore()[0]);
        levelModeHighScore.text = $"LV MODE HIGHSCORE: {HighScoreManager.Instance.LoadHighScore()[0]}";
        endlessModeHighScore.text = $"ENDLSS MODE HIGHSCORE: {HighScoreManager.Instance.LoadHighScore()[1]}";
    }
    
    public async void StartGame(){
        if (action) return;
        action = true;
        SFXManager.Instance.PlayAudio(resumeAudio);
        await Task.Delay(200);
        SceneManager.LoadScene("MainGame");
    }

    public async void ExitGame(){
        if (action) return;
        action = true;
        SFXManager.Instance.PlayAudio(gameQuitAudio);
        await Task.Delay(200);
        Application.Quit();
    }

    public async void EnterEndlessModeInLevelMode(){
        if (action) return;
        action = true;
        SFXManager.Instance.PlayAudio(enterEndlessAudio);
        await Task.Delay(200);
        SceneLoader.Instance.LoadEndLess();
    }
}
