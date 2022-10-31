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
    public bool buttonPressed = true;
    Event e;
    public KeyCode lightButton;
    public bool waitingforButton = false;
    private void Start()
    {
        if (!DataManagment.instance.CheckData())
            OpenUIElement(1);
    }

    private void Update()
    {
        if(!buttonPressed)
        {
            GetInput();
        }
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                Debug.Log("KeyCode down: " + kcode);
        }

    }
    public void OpenUIElement(int id)
    { 
    foreach (RectTransform transform in uiElements)
        {
            transform.gameObject.SetActive(false);
        }
        uiElements[id].gameObject.SetActive(true);
        if (id == 1)
        {
            buttonPressed = false;
        }
    }
    public void GetInput()
    {
        waitingforButton = false;
        text.text = "Press the Key for Light Attack.";
        if(Input.anyKeyDown)
        {
            waitingforButton = true;
        
            buttonPressed = true;
        }
        if (lightButton != KeyCode.None)
        {
       
            OpenUIElement(0);

        }

    }
    public void StartButton()
    {
        SceneManager.LoadScene("Main");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void SendData()
    {
        UserData data = new UserData();
        data.inputsP1[0] = lightButton;
        data.inputsP1[1] = KeyCode.None;
        data.inputsP1[2] = KeyCode.None;
        DataManagment.instance.ReWriteData(data);
    }
}
