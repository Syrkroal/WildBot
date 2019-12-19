using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int healthPoints = 3;
    private Animator anim;
    private MovePlayer move;
    public float takeDamageRate = 2;
    public float lastDamageTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        move = GetComponent<MovePlayer>();
    }

    private IEnumerator PlayAnimation(string animName)
    {
        anim.Play(animName, 0);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (Time.time > lastDamageTime) {
            lastDamageTime = Time.time + takeDamageRate;
            healthPoints -= damage;
            if (healthPoints <= 0)
            {
                move.enabled = false;
            }
        }
    }
}
