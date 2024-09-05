using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerLazerType{
    Constant, Switch
}

public enum Direction{
    Horizontal, Vertical
}

public class EnemyPowerLazerAbility : MonoBehaviour
{   
    public bool canAttack;

    [Header("References")]
    [SerializeField] private GameObject lazer;

    [Header("Stats Setting")]
    [SerializeField] private PowerLazerType attackType;
    [SerializeField] private float fireRate;

    private void OnEnable() {
        if (attackType == PowerLazerType.Constant){
            AttackConstant();
            AttackConstant();
            return;
        }
        else StartCoroutine(AttackLoop());
    }

    private IEnumerator AttackLoop(){
        yield return new WaitForSeconds(fireRate);
        bool temp = true; // true = vertical, false = horizontal
        while (canAttack){
            temp = !temp;
            AttackOnce(temp ? Direction.Vertical : Direction.Horizontal);
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void AttackOnce(Direction dir){
        GameObject clone = Instantiate(lazer);
        clone.transform.SetParent(transform);
        clone.transform.localPosition = Vector3.zero;

        Animator anim = clone.GetComponent<Animator>();
        anim.Play($"Attack{dir.ToString()}");
    }

    private void AttackConstant(){
        GameObject clone = Instantiate(lazer);
        clone.transform.SetParent(transform);
        clone.transform.localPosition = Vector3.zero;
    }
}
