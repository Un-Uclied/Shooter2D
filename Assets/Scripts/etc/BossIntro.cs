using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class BossIntro : Singleton<BossIntro>
{   
    [SerializeField] private AudioClip startAudio;
    public async void PlayAudio(){
        // Time.timeScale = 0;
        SFXManager.Instance.PlayAudio(startAudio);
        // await Task.Delay((int)(startAudio.length * 1000));
        // Time.timeScale = 1;
    }
}