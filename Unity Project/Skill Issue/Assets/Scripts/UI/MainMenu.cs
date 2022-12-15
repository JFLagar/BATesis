using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI text;
    public RectTransform[] uiElements;

    public InputMapping inputMapping;
    public Button[] buttons;


    private void Start()
    {

    }

    private void Update()
    {
     
    }
    public void OpenUIElement(int id)
    { 
    foreach (RectTransform transform in uiElements)
        {
            transform.gameObject.SetActive(false);
        }
        uiElements[id].gameObject.SetActive(true);
        buttons[id].Select();
    }
    public void StartButton(bool training)
    {
        ScoreTracker.instance.training = training;
        SceneManager.LoadScene("Main");
    }
    public void QuitButton()
    {
        Application.Quit();
    }

}
