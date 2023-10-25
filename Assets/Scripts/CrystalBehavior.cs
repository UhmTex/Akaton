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
        if (Vector3.Distance(transform.position, player.position) < 10f && !isNear)
        {
            isNear = true;
            transform.DORotate(new Vector3(0, 360, 0), 5, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.InOutSine);
            animator.Play("Transition Fix");
        }
        else if (Vector3.Distance(transform.position, player.position) >= 10f && isNear) 
        {
            transform.DOComplete();
            isNear = false;
            animator.Play("Transition Break");
        }
    }

}
