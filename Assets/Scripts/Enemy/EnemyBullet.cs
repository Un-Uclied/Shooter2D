using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBullet : MonoBehaviour
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
        if (other.CompareTag("Vaccine") && other.gameObject.layer == 3 && other.gameObject.GetComponent<VaccineHealth>() != null){
            if (other.gameObject.GetComponent<VaccineHealth>().isInvincible) return;
            Destroy(gameObject);
            VaccineHealth plr = other.gameObject.GetComponent<VaccineHealth>();
            plr.SetHealth(plr.health - 1);
        }
    }
}
