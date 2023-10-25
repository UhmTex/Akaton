using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup _uIGroup;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    public void FadeIn()
    {
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }

    void Update()
    {
       if (fadeIn)
        {
            if (_uIGroup.alpha < 1) 
            {
                if (_uIGroup.tag == "DeathText")
                {
                    _uIGroup.alpha += 0.3f * Time.deltaTime;
                    if (_uIGroup.alpha >= 1)
                    {
                        fadeIn = false;
                    }
                }
                else
                {
                    _uIGroup.alpha += Time.deltaTime;
                    if (_uIGroup.alpha >= 1)
                    {
                        fadeIn = false;
                    }
                }
            }
        }

        if (fadeOut)
        {
            if (_uIGroup.alpha >= 0)
            {
                _uIGroup.alpha -= (0.5f * Time.deltaTime);
                if (_uIGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }
}
