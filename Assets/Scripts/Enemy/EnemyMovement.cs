using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMovementType{
    Follow, OtherSide
}

public class EnemyMovement : MonoBehaviour
{
    private bool moving = false;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Space]
    [Header("Stats Setting")]
    [SerializeField] private float movementSpd;
    [SerializeField] private EnemyMovementType movementType;

    [Space]
    [Header("Follow Type")]
    [SerializeField] [Range(1, 2)] private float maxRandomFollowForce;
    [SerializeField] [Range(1, 1.5f)] private float minRandomFollowForce;

    [Space]
    [SerializeField] [Range(1, 2)] private float maxFollowRestTime;
    [SerializeField] [Range(.1f, 2)] private float minFollowRestTime;

    [Space]
    [Header("OtherSide Type")]
    // OtherSide
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private Vector3 target;

    private void Start() {
        spawnPosition = transform.position;
        target = new Vector3(-spawnPosition.x, -spawnPosition.y, 0);
    }

    private void FixedUpdate() {
        if (movementType == EnemyMovementType.Follow){
            if (moving) return;

            VaccineMovement vaccine = FindAnyObjectByType<VaccineMovement>();
            if (vaccine == null) {
                Debug.LogError("Cannot Find Player's Vaccine Movement Script!");
                Debug.LogError($"{name} will stop moving.");
                return;
            }
            StartCoroutine(MoveEnemy(vaccine));
        }
        else{
            Vector2 dir = (target - transform.position).normalized;
            rb.AddForce(dir * movementSpd * Time.fixedDeltaTime * 100 , ForceMode2D.Force);
            if (Vector2.Distance(transform.position, target) < 0.1f){
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator MoveEnemy(VaccineMovement vaccine){
        moving = true;

        rb.AddForce(GetMovementDirection(vaccine) * movementSpd * Random.Range(minRandomFollowForce, maxRandomFollowForce) * 10 * Time.fixedDeltaTime, ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(Random.Range(minFollowRestTime, maxFollowRestTime));

        moving = false;
    }

    private Vector2 GetMovementDirection(VaccineMovement vaccine){
        if (movementType == EnemyMovementType.Follow){
            Vector2 moveDir = (vaccine.transform.position - transform.position).normalized;
            return moveDir;
        }
        else{
            return Vector2.zero;
        }
        
        // // else if (movementType == EnemyMovementType.RandomZigZag){

        // }
    }
}
