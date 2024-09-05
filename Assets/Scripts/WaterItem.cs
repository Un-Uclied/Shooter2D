using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WaterItem : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject particle;
    

    [Header("Stats Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int healAmount;

    Transform plr;
    Vector2 moveDir;

    private void Start() {
        plr = FindAnyObjectByType<VaccineHealth>().transform;
    }

    private void Update(){
        if (plr == null) return;
        moveDir = (plr.position - transform.position).normalized;
    }

    private void FixedUpdate() {
        rb.AddForce(moveDir * moveSpeed, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Vaccine") && other.gameObject.layer == 3 && other.gameObject.GetComponent<VaccineHealth>() != null){
            //GameObject clone = Instantiate(particle, transform.position, Quaternion.identity);
            if (other.gameObject.GetComponent<VaccineHealth>().isInvincible) return;
            Destroy(gameObject);
            VaccineHealth plr = other.gameObject.GetComponent<VaccineHealth>();
            plr.SetHealth(plr.health + healAmount); // Add Health
        }
    }
}
