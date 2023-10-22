using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromtUI : MonoBehaviour
{
    [SerializeField] Camera _mainCam;
    [SerializeField] TextMeshProUGUI _enterractText;
    [SerializeField] GameObject _uiPannel;
    public bool IsDisplayed = false;

    private void Start()
    {
        _uiPannel.SetActive(false);
    }
    private void LateUpdate()
    {
        var rotation = _mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public void SetUp(string promptText)
    {
        _enterractText.text = promptText;
        _uiPannel?.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        _uiPannel.SetActive(false);
        IsDisplayed = false;
    }
}
