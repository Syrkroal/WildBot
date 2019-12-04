using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public int healthPoints = 3;
    public bool deathPlaying = false;
    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    private IEnumerator PlayAnimation(string animName)
    {
        anim.Play(animName, 0);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage) {
        healthPoints -= damage;
    }

    void Update() {
        if (healthPoints <= 0 && !deathPlaying) {
            deathPlaying = true;
            StartCoroutine(PlayAnimation("Death"));
        }
    }
}
