using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingIslandBehavior : MonoBehaviour
{
    [SerializeField] Vector3 Destination;
    [SerializeField] float Time = 4f;

    Tween tween;

    private void Start()
    {
        tween = transform.DOMove(Destination, Time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        tween.Play();
    }
}
