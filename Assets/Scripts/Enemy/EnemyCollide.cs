using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyCollide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Vaccine") && other.gameObject.layer == 3 && other.gameObject.GetComponent<VaccineHealth>() != null){
            if (other.gameObject.GetComponent<VaccineHealth>().isInvincible) return;
            VaccineHealth plr = other.gameObject.GetComponent<VaccineHealth>();
            plr.SetHealth(plr.health - 1);
        }
    }
}
