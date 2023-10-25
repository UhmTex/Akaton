using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrystalBehavior : MonoBehaviour
{
    private Animator animator;
    private Transform player;

    private bool isNear = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 15f && !isNear)
        {
            isNear = true;
            animator.Play("Transition Fix");
            transform.tag = "Full Crystal";
        }
        else if (Vector3.Distance(transform.position, player.position) >= 15f && isNear) 
        {
            transform.DOComplete();
            isNear = false;
            transform.tag = "Broken Crystal";

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                animator.Play("Transition Break", 0, 1 - animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            } 
            else
            {
                animator.Play("Transition Break");
            }
        }
    }

    // 5.792

    public void PlayTween()
    {
        if (isNear)
        {
            transform.DORotate(new Vector3(0, 360, 0), 2.5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.InOutSine, 7, 2);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 15f);
    }

}
