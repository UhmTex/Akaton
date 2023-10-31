using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrystalBehavior : MonoBehaviour
{
    public static CrystalBehavior Instance { get; private set; }

    public Transform[] Cells;

    private Animator animator;
    private Transform player;

    private bool isNear = false;
    private bool isCollected = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (!isCollected)
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
    }

    public void PlayExplosion()
    {
        isCollected = true;

        animator.Play("Explode");
        transform.DORotate(new Vector3(0, 360, 0), 2.5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.InOutSine, 7, 2);
    }

    public void AbsorbShards()
    {
        foreach (Transform item in Cells)
        {
            item.position = Vector3.MoveTowards(item.position, player.position, 5 * Time.deltaTime);
        }
    }

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
