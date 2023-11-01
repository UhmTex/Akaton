using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup _uIGroup;
    [SerializeField] private CanvasGroup _uIGroupSister;
    [SerializeField] private CanvasGroup _uIGroupMother;
    [SerializeField] private CanvasGroup _uIGroupFather;
    [SerializeField] private CanvasGroup _uIGroupThePicture;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;
    private bool _thePictureFade = false;

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
        if (_thePictureFade)    
        {
            FadePictureOut();
        }
        else
        {
            if (fadeIn)
            {
                if (_uIGroup.tag == "DeathCanvas")
                {
                    _uIGroup.alpha += 0.5f * Time.deltaTime;
                    if (_uIGroup.alpha >= 1)
                    {
                        fadeIn = false;
                    }
                }
                if (_uIGroup.tag == "DeathText")
                {
                    _uIGroup.alpha += 0.3f * Time.deltaTime;
                    if (_uIGroup.alpha >= 1)
                    {
                        fadeIn = false;
                    }
                }
                if (_uIGroup.tag == "FamilyPicture")
                {
                    _uIGroup.alpha += Time.deltaTime;
                    if (_uIGroup.alpha >= 1)
                    {
                        _uIGroupThePicture.alpha += 0.3f * Time.deltaTime;
                        if (_uIGroupThePicture.alpha >= 1)
                        {
                            if (SceneManager.GetActiveScene().buildIndex == 0)
                            {
                                _uIGroupSister.alpha += (0.3f * Time.deltaTime);

                                if (_uIGroupSister.alpha >= 1)
                                {
                                    fadeIn = false;
                                    _thePictureFade = true;
                                }
                            }
                            else if (SceneManager.GetActiveScene().buildIndex == 1)
                            {
                                _uIGroupMother.alpha += (0.3f * Time.deltaTime);

                                if (_uIGroupMother.alpha >= 1)
                                {
                                    fadeIn = false;
                                    _thePictureFade = true;
                                }
                            }
                            else
                            {
                                _uIGroupFather.alpha += (0.3f * Time.deltaTime);

                                if (_uIGroupFather.alpha >= 1)
                                {
                                    fadeIn = false;
                                    _thePictureFade = true;
                                }
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
            }
            if (fadeOut)
            {
                if (_uIGroup.alpha > 0)
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
    
    private void FadePictureOut()
    {
        _uIGroupThePicture.alpha -= 0.5f * Time.deltaTime; 
    }   
}
