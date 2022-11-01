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



    private void Start()
    {
        if (!DataManagment.instance.CheckData())
            OpenUIElement(1);
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

    }
    public void GetInput()
    {
        text.text = "Press the Key for Light Attack.";      
            OpenUIElement(0);

    }
    public void StartButton()
    {
        SceneManager.LoadScene("Main");
    }
    public void QuitButton()
    {
        Application.Quit();
    }

}
