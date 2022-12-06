using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.EnableTrainingMode();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GameManager.instance.ResetRound();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.EndGame();
        }
    }
}
