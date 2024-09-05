using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    public void PlayAudio(AudioClip audio)
    {
        GameObject soundObject = new GameObject();
        AudioSource sound = soundObject.AddComponent<AudioSource>();
        sound.clip = audio;
        soundObject.transform.position = Camera.main.transform.position;
        soundObject.transform.SetParent(Camera.main.transform);
        sound.Play();

        soundObject.name = audio.name;

        StartCoroutine(DestroySound(soundObject));

        IEnumerator DestroySound(GameObject obj)
        {
            yield return new WaitForSeconds(sound.clip.length + .1f);
            Destroy(obj);
            yield return null;
        }
    }
}
