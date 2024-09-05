using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EnemyHealth : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject dieParticle;
    [SerializeField] private AudioClip hurtAudio;
    [SerializeField] private AudioClip dieAudio;

    [Header("Materal References")]
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material whiteOutMaterial;
    

    [Header("Stats Setting")]
    [SerializeField] public int health;
    [Space]
    [SerializeField] private float whiteOutTime;

    private void Awake(){
        
        if (defaultMaterial == null){
            defaultMaterial = sr.material;
        }
    }
    public void ChangeHealth(int amt){
        health += amt;
        StartCoroutine(WhiteOutEnemy());
        //WindowShaker.Instance.ShakeWindowForSeconds(5, .06f);
        FindAnyObjectByType<UIManager>().OnHurted();
        if (health <= 0){
            SFXManager.Instance.PlayAudio(dieAudio);
            KillEnemy();
            return;
        }
        SFXManager.Instance.PlayAudio(hurtAudio);
    }

    private IEnumerator WhiteOutEnemy(){
        sr.material = whiteOutMaterial;
        yield return new WaitForSeconds(whiteOutTime);
        sr.material = defaultMaterial;
    }
    
    private void KillEnemy(){
        var particle = Instantiate(dieParticle, transform.position, Quaternion.identity);
        ScoreManager.Instance.OnEnemyDied(int.Parse(name[^1].ToString()));
        Destroy(gameObject);
    }
}
