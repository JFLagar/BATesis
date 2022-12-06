using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillIssue.CharacterSpace;
using TMPro;

public class UIBehaviour : MonoBehaviour
{
    public GameManager manager;
    public Character[] characters;
    public Slider[] sliders;
    public TextMeshProUGUI[] comboDisplays;
    public TextMeshProUGUI timerText;
    public float timer = 99;
    public TextMeshProUGUI debug;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i< sliders.Length; i++)
        {
            sliders[i].maxValue = characters[i].maxHealth;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.testing)
        {
            debug.text = "Debug: " + characters[0].comboHit + " State:" + characters[1].currentAction ;
        }
        else
        {
            debug.text = "";
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
                manager.RestartRound();
            }
        }
        timer -= Time.deltaTime;
        timerText.text = Mathf.FloorToInt(timer).ToString();
        if (timer <= 0)
        {
            manager.RestartRound();
        }
    }
}
