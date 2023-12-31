using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float WalkSpeed;
    public float ChaseSpeed = 6f;

    public AudioSource AgroSound;
    public AudioSource CawSound;
    public AudioSource RestartSound;
    public AudioSource BackgroundMusic;
    public AudioSource BackgroundSounds;

    public Transform[] WalkPoints;
    public bool _playerIsDead = false;
    [SerializeField] int _numFound;
    [SerializeField] Transform _interactionPoint;
    [SerializeField] float _interactionPointRadius = 1f;
    [SerializeField] LayerMask _interactableMask;
    [SerializeField] DeathScript _deathScript;
    [SerializeField] Animator _animator;

    private readonly Collider[] _colliders = new Collider[3];

    private Transform _playerTarget;
    private Vector3 _starterPos;
    private Transform CurrentWalkPoint;

    private bool isPlayerDetected = false;
    private bool aggroRemovedRecently = false;
    private float aggroTimerCount = 3f;
    private float aggroTime = 0f;
    private bool _playedAgroSound = false;


    private float randomGeneratorNumberCount = 5f;
    private float randomGeneratorNumberTimer;
    private float randomCawTimerCount;
    private float cawTimer;

    private bool Aggrod = false;

    private void Start()
    {
        CurrentWalkPoint = WalkPoints[0];
        _starterPos = Vector3.Lerp(WalkPoints[0].position, WalkPoints[1].position, 0.5f);
    }

    private void Update()
    {
        if (!_playerIsDead)
        {
            randomGeneratorNumberTimer += Time.deltaTime;

            if (randomGeneratorNumberTimer > randomGeneratorNumberCount)
            {
                randomCawTimerCount = Random.Range(10, 35);
                randomGeneratorNumberTimer = 0;
            }

            if (aggroRemovedRecently)
            {
                aggroTime += Time.deltaTime;

                if (aggroTime > aggroTimerCount)
                {
                    aggroRemovedRecently = false;
                    aggroTime = 0f;
                }
            }

            if (!aggroRemovedRecently)
            {
                _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);
            }

            DetectAndAttack();
            DeAggro();

            if (!isPlayerDetected)
            {
                cawTimer += Time.deltaTime;

                if (cawTimer > randomCawTimerCount)
                {
                    CawSound.Play();
                    cawTimer = 0;
                }

                LookAt(CurrentWalkPoint);
                transform.position = Vector3.MoveTowards(transform.position, CurrentWalkPoint.position, WalkSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, CurrentWalkPoint.position) < 1f)
                {
                    CurrentWalkPoint = WalkPoints[Random.Range(0, WalkPoints.Length)];
                }
            }
        }
        else
        {
            AgroSound.Stop();
        }
    }

    private void DeAggro()
    {
        if (isPlayerDetected) {
            if (Vector3.Distance(_starterPos, _playerTarget.position) > 20f)
            {
                _animator.SetBool("isChasing", false);
                StartCoroutine(Fade(false, AgroSound, 2f, 0f));
                isPlayerDetected = false;
                aggroRemovedRecently = true;
                _numFound = 0;
            }
        }
    }

    private void DetectAndAttack()
    {
        if (_numFound > 0)
        {
            var interactable = _colliders[0].gameObject;
            if (interactable.CompareTag("Player"))
            {
                if (!_playedAgroSound)
                {
                    _animator.SetBool("isChasing", true);
                    AgroSound.Play();
                    StartCoroutine(Fade(true, AgroSound, 2f, 0.3f));

                    _playedAgroSound = true;
                }

                _playerTarget = interactable.transform;

                isPlayerDetected = true;

                Vector3 interpolatedPos = Vector3.Lerp(transform.position, _playerTarget.position + new Vector3(0, 1, 0), 0.7f);
                Debug.DrawLine(transform.position, interpolatedPos, Color.blue);

                LookAt(_playerTarget, 1.5f);
                var step = ChaseSpeed * Time.deltaTime;
                if (Vector3.Distance(transform.position, interpolatedPos) > 1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, interpolatedPos, step);
                }
                else
                {
                    //AgroSound.mute = true;
                    //AgroSound.Stop();
                    BackgroundMusic.Stop();
                    BackgroundSounds.Stop();
                    AgroSound.Stop();
                    CawSound.Stop();
                    RestartSound.Play();
                    _deathScript.PlayDeath();
                    _playerIsDead = true;
                }
            }
            else
            {
                _playerTarget = null;
                isPlayerDetected = false;
                _playedAgroSound = false;
                AgroSound.mute = false;
            }
        }
        else
        {
            isPlayerDetected = false;
            _playedAgroSound = false;
            AgroSound.mute = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }

    private void LookAt(Transform Target)
    {
        Vector3 relativePos = Target.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
    }

    private void LookAt(Transform Target, float BonusHeight)
    {
        Vector3 relativePos = (Target.position + new Vector3(0, BonusHeight, 0)) - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
    }

    IEnumerator Fade(bool FadeIn, AudioSource source, float duration, float targetVolume)
    {
        if (FadeIn && !Aggrod)
        {
            Aggrod = true;

            float time = 0f;
            float startVolume = source.volume;

            while (time < duration)
            {
                time += Time.deltaTime;
                source.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
                yield return null;
            }

            yield break;
        }
        else if (!FadeIn && Aggrod)
        {
            Aggrod = false;

            float time = 0f;
            float startVolume = source.volume;

            while (time < duration)
            {
                time += Time.deltaTime;
                source.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
                yield return null;
            }

            yield break;
        }
    }
}