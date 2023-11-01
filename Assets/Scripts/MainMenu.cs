using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   private bool _startGame = false;
    private float _timer = 3f;
    [SerializeField] DeathScript DeathScript;
    public void StartGame()
    {
        _startGame = true;
        DeathScript.EntrenceFromStartScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (_startGame)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
