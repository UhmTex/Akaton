using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    public static bool FirstlyDied = false;

    [SerializeField] FadeScript _fadeScriptCanvas;
    [SerializeField] FadeScript _fadeScriptText;
    [SerializeField] MovementHandler _playersMovement;
    private bool _playerIsDead = false;
    private float _timer = 7;

    private void Start()
    {
        if (FirstlyDied || SceneManager.GetActiveScene().buildIndex > 0)
        {
            GetComponent<CanvasGroup>().alpha = 1.0f;
            _fadeScriptCanvas.FadeOut();
        }
    }

    private void Update()
    {
        if (_playerIsDead)
        { 
            _timer -= Time.deltaTime;

            if (_timer < 4)
            {
                _fadeScriptText.FadeIn();
            }
            if (_timer < 0)
            {
                _fadeScriptText.GetComponent<CanvasGroup>().alpha = 0f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void PlayDeath()
    {
        _playerIsDead = true;
        _playersMovement.PlayerIsDead = true;
        _fadeScriptCanvas.FadeIn();
        FirstlyDied = true;
    }
}
