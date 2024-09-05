using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class VaccineLazerAbility : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] private GameObject LazerPrefab;
    [SerializeField] private AudioClip lazerFireAudio;

    [Space]
    [Header("Stats Setting")]
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletDespawnTime;
    [Space]
    [SerializeField] private float lazerSpeed;


    private float currFireRate;
    private bool mouseDown = false;

    private void Update(){
        if (currFireRate > 0) currFireRate -= Time.deltaTime; 
        ManageLazerFire();
    }

    private void ManageLazerFire(){
        if (!mouseDown || currFireRate > 0) return;
        currFireRate = fireRate;

        FireLazer();
    }

    private void FireLazer(){
        GameObject lazer = Instantiate(LazerPrefab);
        Rigidbody2D lazerRb = lazer.GetComponent<Rigidbody2D>();

        SFXManager.Instance.PlayAudio(lazerFireAudio);
        
        lazer.transform.position = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint((Vector3)Mouse.current.position.value);
        Vector2 fireDir = (mousePos - transform.position).normalized;

        lazerRb.AddForce(fireDir * lazerSpeed, ForceMode2D.Impulse);
    }

    public void OnMouseClick(InputAction.CallbackContext context){
        if (context.started) {
            mouseDown = true;
            return;
        }
        else if (context.canceled){
            mouseDown = false;
            return;
        }
    }
}
