using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleBoom : MonoBehaviour
{   
    private float originalScale;
    [SerializeField] private float targetScale;
    [SerializeField] private float boomRate;
    
    private void Start(){
        originalScale = transform.localScale.x;
        StartCoroutine(BoomLoop());
    }

    private IEnumerator BoomLoop(){
        while (true){
            transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            transform.DOScale(originalScale, .3f);

            yield return new WaitForSeconds(boomRate);
        }
    }
}
