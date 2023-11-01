using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingIslandBehavior : MonoBehaviour
{
    [SerializeField] Transform DestinationObject;
    [SerializeField] float Time = 4f;

    Tween tween;

    Vector3 Destination;

    private void Start()
    {
        Destination = DestinationObject.position;

        Sequence s = DOTween.Sequence();
        s.SetDelay(2f);
        s.Append(transform.DOMove(Destination, Time).SetEase(Ease.InOutSine));
        s.AppendInterval(1f);
        s.SetLoops(-1, LoopType.Yoyo);

        s.Play();

        /*tween = transform.DOMove(Destination, Time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        tween.Play();*/
    }
}
