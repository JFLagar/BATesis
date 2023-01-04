using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillIssue.CharacterSpace;
using TMPro;
using System;

public class UIBehaviour : MonoBehaviour
{
    public static UIBehaviour instance;
    public GameManager manager;
    public Character[] characters;
    public Slider[] sliders;
    public Slider[] elementSliders;
    public Image[] elementIcon;
    public Sprite[] elementSprites;
    public TextMeshProUGUI[] comboDisplays;
    public TextMeshProUGUI timerText;
    public float timer = 99;
    public TextMeshProUGUI debug;
    public Image[] p1Rounds, p2Rounds;
    public RectTransform pauseUI;
    public Button characterSelect;
    public RectTransform characterSelectUI;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    void Start()
    {
        Time.timeScale = 1;
    
        switch (ScoreTracker.instance.p1score)
        {
            case 0:
                p1Rounds[0].enabled = true;
                break;
            case 1:
                p1Rounds[0].enabled = true;
                p1Rounds[1].enabled = true;
                break;
        }
        switch (ScoreTracker.instance.p2score)
        {
            case 0:
                p2Rounds[0].enabled = true;
                break;
            case 1:
                p2Rounds[0].enabled = true;
                p2Rounds[1].enabled = true;
                break;
        }
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].maxValue = characters[i].maxHealth;
            elementSliders[i].value = (float)characters[i].element;
            elementIcon[i].sprite = elementSprites[(int)characters[i].element];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(manager.testing)
        {
            debug.text = "Debug: " + characters[0].comboHit + " State:" + characters[1].currentAction ;
            characterSelect.gameObject.SetActive(true);
        }
        else
        {
            debug.text = "";
            characterSelect.gameObject.SetActive(false);
        }
        for (int i = 0; i < comboDisplays.Length; i++)
        {
            if (characters[i].comboHit <= 1)
            {
                comboDisplays[i].text = "";
            }
            else
            {
                comboDisplays[i].text = characters[i].comboHit + " HIT";
            }
        }
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].value = characters[i].currentHealth;
            if(sliders[i].value <= 0)
            {
                if (!manager.testing)
                {
                    if (i == 0)
                    {
                        ScoreTracker.instance.AddP2score();
                    }
                    else
                    {
                        ScoreTracker.instance.AddP1score();
                    }
                }
                manager.RestartRound();
            }
        }
        timer -= Time.deltaTime;
        timerText.text = Mathf.FloorToInt(timer).ToString();
        if (timer <= 0)
        {
            if(sliders[0].value > sliders[1].value)
            {
                if(!manager.testing)
                ScoreTracker.instance.AddP1score();
            }
            else if(sliders[1].value > sliders[0].value)
            {
                    if (!manager.testing)
                        ScoreTracker.instance.AddP1score();
            }
            manager.RestartRound();
        }
    
    }

    internal void ResetAll()
    {
        foreach(Slider slider in sliders)
        {
            slider.value = slider.maxValue;
            timer = 99;
        }
    }
    public void OpenUI()
    {
        Time.timeScale = 0;
        foreach(Character character in characters)
        {
            character.inputHandler.playerInput.SwitchCurrentActionMap("Menu");
        }
        pauseUI.gameObject.SetActive(true);
    }
    public void CloseUI()
    {
        pauseUI.gameObject.SetActive(false);
        foreach (Character character in characters)
        {
            character.inputHandler.playerInput.SwitchCurrentActionMap("Controls");
        }
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        manager.BackToMenu();
    }

    public void Quit()
    {
        manager.EndGame();
    }
    public void OpenCharacterSelect()
    {
        pauseUI.gameObject.SetActive(false);
        characterSelectUI.gameObject.SetActive(true);
        elementSliders[0].Select();

    }
    public void CloseCharacterSelect()
    {
        pauseUI.gameObject.SetActive(true);
        characterSelectUI.gameObject.SetActive(false);
        characterSelect.Select();
    }
    public void OnSliderChange(bool p2)
    {
        if(!p2)
        {
            switch (elementSliders[0].value)
            {
                case 0: characters[0].element = Element.Fire;
                    break;
                case 1:
                    characters[0].element = Element.Water;
                    break;
                case 2:
                    characters[0].element = Element.Wind;
                    break;
                case 3:
                    characters[0].element = Element.Earth;
                    break;
            }
            elementIcon[0].sprite = elementSprites[(int)characters[0].element];
        }
        else
        {
            switch (elementSliders[1].value)
            {
                case 0:
                    characters[1].element = Element.Fire;
                    break;
                case 1:
                    characters[1].element = Element.Water;
                    break;
                case 2:
                    characters[1].element = Element.Wind;
                    break;
                case 3:
                    characters[1].element = Element.Earth;
                    break;
            }
            elementIcon[1].sprite = elementSprites[(int)characters[1].element];
        }
    }
}
