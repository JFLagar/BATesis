
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.Inputs;
using SkillIssue.CharacterSpace;

public class CharacterAI : MonoBehaviour
{
    InputHandler handler;
    Character character;
    public bool initiated = false;
    public Vector2 dir;
    public int attackInput;
    public bool buttonUp;
    private int directionMaxRange;
    private int attackMaxRange;
    public bool directionBool;
    public int x;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!initiated)
            return;
        CheckGameState();    
    }

    public void Initiate(InputHandler inputHandler)
    {
        handler = inputHandler;
        initiated = true;
        character = handler.character;
    }
    //Try to get closer to the oponent, when close enough do 5L if not 5H
    void CheckGameState()
    {
        //Check Distance Between them
        distance = ((character.xDiff + character.xDiff) / 2) * character.xDiff;
        switch(distance)
        {
            //close by should move more away and less often and attack more often
            case <= 0.5f:
                directionMaxRange = 50;
                attackMaxRange = 8;
                directionBool = false;
                break;
            case >= 1.5f:
                directionMaxRange = 2;
                attackMaxRange = 50;
                directionBool = true;
                break;
            default:
                directionMaxRange = 16;
                attackMaxRange = 50;
                directionBool = true;
                break;
        }
        SimulateAttackInput();
        SimulateMovementInput();
      
    }
    void SimulateAttackInput()
    {
        float attackRange = Random.Range(0, 101);
        if (attackRange <= 100 / attackMaxRange)
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
    void SimulateMovementInput()
    {
        int randomX = 0;
       
        int dirRange = Random.Range(0, 101);
        if (dirRange <= 100 / directionMaxRange)
        {
            if (directionBool)
            {
                randomX = Random.Range(-100, 401);
            }
            else
            {
                randomX = Random.Range(-300, 201);
            }
            switch (randomX)
            {
                case < -200:
                    x = -1;
                    break;
                case > 300:
                    x = 1;
                    break;
                default:
                    x = 0;
                    break;
            }
            //check if jump
            int randomY = Random.Range(-1000, 1000);
            int y = 0;
            switch (randomY)
            {
                //case > 900:
                //    y = 1;
                //    break;
                //case < -800:
                //    y = -1;
                //    break;
                default:
                    y = 0;
                    break;
            }
            dir = new Vector2(x,y);
        }

    
    }
}
