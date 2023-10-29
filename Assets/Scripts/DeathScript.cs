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
    private float _timer = 11;

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
    }
}
