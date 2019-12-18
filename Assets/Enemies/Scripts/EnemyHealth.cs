using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float healthPoints = 3;
    public bool deathPlaying = false;
    public bool hasDeathAnim = false;
    public Material hitMaterial;
    public float extraDeathTime = 0;
    public GameObject explosion;
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
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + extraDeathTime);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage) {
        healthPoints -= damage;
        // renderer.material = hitMaterial;
        // for (var i = 0; i < renderer.materials.Length; i++)
        // {
        //     renderer.materials[i].color = new Color(hitMaterial.color.r, hitMaterial.color.g, hitMaterial.color.b, 1);
        // }
        // Color color = renderer.material.color;
        // renderer.material.color = color;
        // if (childRenderer) {
        //     childRenderer.material.SetColor("_Color", new Color(255, 0, 0, 1));
        // }
        takingDamage = true;
        // oldMaterial = hitMaterial;
    }

    void Explode()
    {
        GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
        var exp = go.GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(go, exp.main.duration);
        Destroy(gameObject);
    }

    void Update() {
        if (healthPoints <= 0 && !deathPlaying) {
            if (hasDeathAnim)
            {
                deathPlaying = true;
                StartCoroutine(PlayAnimation("Death"));
            }
            else if (explosion) {
                Explode();
            }
            else
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
