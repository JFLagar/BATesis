
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.Inputs;

public class CharacterAI : MonoBehaviour
{
    InputHandler handler;
    public bool initiated = false;
    public Vector2 dir;
    public int attackInput;
    public bool buttonUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!initiated)
            return;
        float dirRange = Random.Range(0, 40);
        if (dirRange > 30)
        {
            int x = Random.Range(-1, 2);
            int y = Random.Range(-1, 2);
            dir = new Vector2(x, y);
        }
        float attackRange = Random.Range(0, 80);
        if (attackRange > 60)
        {
            attackInput = Random.Range(0, 3);
            int isButtonUp = Random.Range(0, 2);
            if (isButtonUp == 0)
            {
                buttonUp = false;
            }
            else
            {
                buttonUp = true;
            }
        }
    
    }

    public void Initiate(InputHandler inputHandler)
    {
        handler = inputHandler;
        initiated = true;
    }

}
