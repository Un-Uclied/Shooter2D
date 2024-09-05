using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class VaccineMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;

    [Space]
    [Header("Stats Setting")]
    [SerializeField] private float movementSpd;
    [SerializeField] private float maxMoveableArea;
    private Vector2 inputDir;

    private void Update(){
        ClampPosition();
    }

    private void FixedUpdate(){
        rb.AddForce(inputDir * movementSpd * Time.fixedDeltaTime * 100);
    }

    private void ClampPosition(){
        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x ,-maxMoveableArea, maxMoveableArea);
        clampedPos.y = Mathf.Clamp(clampedPos.y ,-maxMoveableArea, maxMoveableArea);
        transform.position = clampedPos;
    }

    public void OnInputMove(InputAction.CallbackContext context){
        inputDir = context.ReadValue<Vector2>().normalized;
    }
}
