using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI text;
    public RectTransform[] uiElements;
    private void Start()
    {
        if (!DataManagment.instance.CheckData())
            OpenUIElement(1);
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
            GetInput();
        }
    }
    public void GetInput()
    {
        text.text = "Press the Key for Light Attack.";
        Event e = Event.current;
        if (e.isKey)
        {
            UserData data = new UserData();
            KeyCode lightButton = e.keyCode;
            data.inputsP1[0] = lightButton;
            data.inputsP1[1] = KeyCode.None;
            data.inputsP1[2] = KeyCode.None;
            DataManagment.instance.ReWriteData(data);
        }
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
