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
        if(uiElements[1].gameObject.activeSelf)
        {
            GetInput();
        }

    }
    public void GetInput()
    {
        inputMapping.StartMappingInputs();    
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
