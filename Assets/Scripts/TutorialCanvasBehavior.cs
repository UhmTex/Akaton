using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TutorialCanvasBehavior : MonoBehaviour
{
    public CanvasGroup GeneralCanvas;

    private bool fadeIsPlaying = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(LoadTutorial(true));
        }
    }

    public void TutorialPlayer()
    {
        StartCoroutine(LoadTutorial(false));
    }

    IEnumerator LoadTutorial(bool firstDelay)
    {
        if (!fadeIsPlaying)
        {
            fadeIsPlaying = true;

            if (firstDelay)
            {
                yield return new WaitForSeconds(2);
            }

            GeneralCanvas.DOFade(1, 1.5f);

            yield return new WaitForSeconds(8);

            GeneralCanvas.DOFade(0, 1.5f).OnComplete(() => { fadeIsPlaying = false; print("test"); });
        }
    }
}
