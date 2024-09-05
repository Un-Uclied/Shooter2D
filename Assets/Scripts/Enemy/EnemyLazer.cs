using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazer : MonoBehaviour
{   
    [SerializeField] private bool isConstatnt;

    [Space]
    [Header("References")]
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip lazerAudio;
    [SerializeField] private AudioClip warningAudio;
    
    private void Start(){
        if (isConstatnt){ 
            // SFXManager.Instance.PlayAudio(lazerAudio);
            GameObject soundObject = new GameObject();
            AudioSource sound = soundObject.AddComponent<AudioSource>();
            sound.clip = lazerAudio;
            soundObject.transform.position = Camera.main.transform.position;
            soundObject.transform.SetParent(transform);
            sound.Play();

            soundObject.name = lazerAudio.name;
            return;
        }
        StartCoroutine(enumerator());
        IEnumerator enumerator(){
            yield return new WaitForSeconds(0.918f);
            Destroy(gameObject);
        }
    }

    public void PlayerLazerSound(){
        SFXManager.Instance.PlayAudio(lazerAudio);
    }

    public void LazerWarningSound(){
        SFXManager.Instance.PlayAudio(warningAudio);
    }
}
