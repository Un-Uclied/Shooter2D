using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyLazerAbility : MonoBehaviour
{   public bool canFire;

    [Header("References")]
    [SerializeField] private GameObject LazerPrefab;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioClip lazerFireAudio;

    [Space]
    [Header("Stats Setting")]
    [SerializeField] private bool canFireAsLeadShot;
    
    [Space]
    [SerializeField] private float maxFireRate;
    [SerializeField] private float minFireRate;
    
    [Space]
    [SerializeField] private float lazerSpeed;

    private void OnEnable(){
        StartCoroutine(ManageLazerFire());
        IEnumerator ManageLazerFire(){
            while (canFire){
                VaccineMovement vaccine = FindAnyObjectByType<VaccineMovement>();
                if (vaccine == null) {
                    Debug.LogError("Cannot Find Player's Vaccine Movement Script!");
                    Debug.LogError($"{name} will stop lazer fire.");
                    break;
                }

                FireLazer();
                yield return new WaitForSeconds(Random.Range(minFireRate, maxFireRate));
            }
        }
    }

    private void FireLazer(){
        GameObject lazer = Instantiate(LazerPrefab);
        Rigidbody2D lazerRb = lazer.GetComponent<Rigidbody2D>();
        
        SFXManager.Instance.PlayAudio(lazerFireAudio);

        lazer.transform.position = transform.position;
        Vector2 fireDir = canFireAsLeadShot ? GetPredictedDirection() : GetDirection();
        
        lazerRb.AddForce(fireDir * lazerSpeed, ForceMode2D.Impulse);
    }

    private Vector2 GetDirection(){
        return (FindAnyObjectByType<VaccineHealth>().transform.position - transform.position).normalized;
    }

    private Vector2 GetPredictedDirection(){
        Vector2 playerPos = FindAnyObjectByType<VaccineHealth>().transform.position;
        Rigidbody2D playerRb = FindAnyObjectByType<VaccineHealth>().transform.GetComponent<Rigidbody2D>();

        Vector2 playerVelocity = playerRb.velocity;

        float distance = Vector2.Distance(playerPos, transform.position);

        float timeToReachTarget = distance / lazerSpeed;

        Vector2 predictedPlayerPos = playerPos + playerVelocity * timeToReachTarget;

        return (predictedPlayerPos - (Vector2)transform.position).normalized;
    }
    
}