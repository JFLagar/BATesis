using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI frameDisplay;
    float deltaTime;
    public bool testing;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (testing)
            return;
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        frameDisplay.text = Mathf.Ceil(fps).ToString();

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            RestartRound();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartRound()
    {
        //for now
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
