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
    public static bool DidntDie = false;

    [SerializeField] FadeScript _fadeScriptCanvas;
    [SerializeField] FadeScript _fadeScriptText;
    [SerializeField] FadeScript _fadeOutImage;
    [SerializeField] MovementHandler _playersMovement;
 
    private bool _playerIsDead = false;
    private float _timer = 11;
    private bool _fadeOut = false;

    private void Start()
    {
        if (FirstlyDied)
        {
            _fadeOutImage.GetComponent<CanvasGroup>().alpha = 0f;
        }
        if (FirstlyDied || SceneManager.GetActiveScene().buildIndex > 1 && !DidntDie)
        {
            GetComponent<CanvasGroup>().alpha = 1.0f;
            _fadeScriptCanvas.FadeOut();
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex != 1)
                _fadeOutImage.GetComponent<CanvasGroup>().alpha = 1.0f;
            _fadeOutImage.FadeOut();
        }
    }

    private void Update()
    {
        if (_playerIsDead)
        { 
            _timer -= Time.deltaTime;

            if (_timer < 8)
            {
                _fadeScriptText.FadeIn();
            }
            if(_timer < 5)
            {
                _fadeScriptText.FadeOut();
            }
            if (_timer < 0)
            {
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
        DidntDie = true;
    }

    public void PassedLevel()
    {
        DidntDie = true;
        FirstlyDied = false;
    }
}
