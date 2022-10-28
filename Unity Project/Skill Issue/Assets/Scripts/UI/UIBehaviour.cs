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
    public TextMeshProUGUI timerText;
    public float timer = 99;
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
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].value = characters[i].currentHealth;
            if(sliders[i].value <= 0)
            {
                Debug.Log("Round Should Restart");
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
