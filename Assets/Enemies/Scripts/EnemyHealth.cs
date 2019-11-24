using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int healthPoints = 3;
    public bool deathPlaying = false;
    public Material hitMaterial;
    private Animator anim;
    private Material oldMaterial;
    private bool takingDamage = false;
    private SkinnedMeshRenderer childRenderer;

    void Start() {
        anim = GetComponent<Animator>();
        childRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if  (childRenderer)
            oldMaterial = childRenderer.material;
    }

    private IEnumerator PlayAnimation(string animName)
    {
        anim.Play(animName, 0);
        deathPlaying = true;
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage) {
        healthPoints -= damage;
        // renderer.material = hitMaterial;
        // for (var i = 0; i < renderer.materials.Length; i++)
        // {
        //     renderer.materials[i].color = new Color(hitMaterial.color.r, hitMaterial.color.g, hitMaterial.color.b, 1);
        // }
        // Color color = renderer.material.color;
        // renderer.material.color = color;
        if (childRenderer) {
            childRenderer.material.SetColor("_Color", new Color(255, 0, 0, 1));
        }
        takingDamage = true;
        // oldMaterial = hitMaterial;
    }

    void Update() {
        if (healthPoints <= 0 && !deathPlaying) {
            // StartCoroutine(PlayAnimation("Death"));
            Destroy(gameObject);
        }
        // if (takingDamage) {
        //     // for (var i = 0; i < renderer.materials.Length; i++)
        //     // {
        //     //     Color color = renderer.material.color;
        //     //     color.a -= 0.1f;
        //     //     renderer.materials[i].color = color;
        //     //     if (color.a <= 0)
        //     //         takingDamage = false;
        //     // }
        //     Material tmpMaterial = oldMaterial;
        //     Color color = tmpMaterial.color;
        //     color.g += 1f;
        //     color.b += 1f;
        //     tmpMaterial.color = color;
        //     renderer.material = tmpMaterial;
        //     // if (color.a <= 0)
        //     //     takingDamage = false;
        // }
    }
}
