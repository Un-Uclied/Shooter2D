using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour
{
    [SerializeField] private float rotateSpd;
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpd * Time.deltaTime));
    }
}
