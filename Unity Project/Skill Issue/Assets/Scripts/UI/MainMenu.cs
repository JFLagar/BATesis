using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SkillIssue.CharacterSpace;
using System;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI text;
    public RectTransform[] uiElements;

    public InputMapping inputMapping;
    public Button[] buttons;

    public Slider[] elementSliders;
    public Image[] elementIcon;
    public Sprite[] elementSprites;

    private void Start()
    {

            elementSliders[0].value = (float)ScoreTracker.instance.p1Element;
            elementIcon[0].sprite = elementSprites[(int)ScoreTracker.instance.p1Element];
            elementSliders[1].value = (float)ScoreTracker.instance.p2Element;
            elementIcon[1].sprite = elementSprites[(int)ScoreTracker.instance.p2Element];

    }

    private void Update()
    {
     
    }
    public void OpenUIElement(int id)
    {
        AudioManager.instance.PlaySoundEffect(1);
        foreach (RectTransform transform in uiElements)
        {
            transform.gameObject.SetActive(false);
        }
        uiElements[id].gameObject.SetActive(true);
        buttons[id].Select();
    }
    public void StartButton(bool training)
    {
        ScoreTracker.instance.vsCPU = false;
        ScoreTracker.instance.training = training;
        AudioManager.instance.PlaySoundEffect(1);
        OpenUIElement(3);
    }
    public void StartButtonVSCPU()
    {
        ScoreTracker.instance.vsCPU = true;
        ScoreTracker.instance.training = false;
        AudioManager.instance.PlaySoundEffect(1);
        OpenUIElement(3);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void OnSliderChange(bool p2)
    {
        AudioManager.instance.PlaySoundEffect(0);
        if (!p2)
        {
            switch (elementSliders[0].value)
            {
                case 0:
                    ScoreTracker.instance.p1Element = Element.Fire;
                    break;
                case 1:
                    ScoreTracker.instance.p1Element = Element.Water;
                    break;
                case 2:
                    ScoreTracker.instance.p1Element = Element.Wind;
                    break;
                case 3:
                    ScoreTracker.instance.p1Element = Element.Earth;
                    break;
            }
            elementIcon[0].sprite = elementSprites[(int)ScoreTracker.instance.p1Element];
        }
        else
        {
            switch (elementSliders[1].value)
            {
                case 0:
                    ScoreTracker.instance.p2Element = Element.Fire;
                    break;
                case 1:
                    ScoreTracker.instance.p2Element = Element.Water;
                    break;
                case 2:
                    ScoreTracker.instance.p2Element = Element.Wind;
                    break;
                case 3:
                    ScoreTracker.instance.p2Element = Element.Earth;
                    break;
            }
            elementIcon[1].sprite = elementSprites[(int)ScoreTracker.instance.p2Element];
        }
    }

}
