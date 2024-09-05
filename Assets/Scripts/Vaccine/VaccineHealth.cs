using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VaccineHealth : MonoBehaviour
{
    [SerializeField] public bool isInvincible;
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject dieParticle;
    [SerializeField] private Animator animator;

    [SerializeField] private AudioClip healAudio;
    [SerializeField] private AudioClip hurtAudio;

    [Header("Materal References")]
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material whiteOutMaterial;
    

    [Header("Stats Setting")]
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;
    [SerializeField] public bool died;
    [Space]
    [SerializeField] private float whiteOutTime;
    [Space]
    [SerializeField ] private float invincibleTime;
    
    [Header("Events")]
    public UnityEvent OnPlayerDied;
    public UnityEvent OnPlayerHealthChanged;
    public UnityEvent OnPlayerOverHealed;

    public object OnPlayerDamaged { get; internal set; }

    private void Awake(){
        
        if (defaultMaterial == null){
            defaultMaterial = sr.material;
        }
    }
    
    public void SetHealth(int target){
        if (isInvincible) return;

        if (target >= health){
            SFXManager.Instance.PlayAudio(healAudio);
        }
        else{
            SFXManager.Instance.PlayAudio(hurtAudio);
        }

        StartCoroutine(WhiteOutPlayer()); // Damage Or Heal
        if (target < health){ StartCoroutine(InvinciblePlayer()); } // Damage

        if (target > maxHealth) { // Over Heal
            OnPlayerOverHealed?.Invoke();
            health = maxHealth;
        }
        else{
            health = target; // Damage Or Heal
        }

        OnPlayerHealthChanged?.Invoke();

        if (health <= 0){
            KillPlayer();
        }
    }

    private IEnumerator WhiteOutPlayer(){
        sr.material = whiteOutMaterial;
        yield return new WaitForSeconds(whiteOutTime);
        sr.material = defaultMaterial;
    }

    private IEnumerator InvinciblePlayer(){
        isInvincible = true;
        animator.Play("Flicker");
        yield return new WaitForSeconds(invincibleTime);
        animator.Play("Normal");
        isInvincible = false;
    }
    
    private void KillPlayer(){
        died = true;
        OnPlayerDied?.Invoke();
        Destroy(gameObject);
    }
}
