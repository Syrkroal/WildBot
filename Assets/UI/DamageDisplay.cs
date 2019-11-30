using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour
{
    public Animator animator;
    private Text damageText;
    void OnEnable()
    {
        AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfos[0].clip.length);
        damageText = animator.GetComponent<Text>();
    }

    public void setText(string text)
    {
        damageText.text = text;
    }
}
