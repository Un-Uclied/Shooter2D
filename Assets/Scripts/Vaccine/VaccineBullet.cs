using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class VaccineBullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;

    private void Start(){
        StartCoroutine(Delay());
    }

    private IEnumerator Delay(){
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy") && other.gameObject.layer == 6 && other.gameObject.GetComponent<EnemyHealth>() != null){
            Destroy(gameObject);
            EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
            enemy.ChangeHealth(-1);
        }
    }
}
