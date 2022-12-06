using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using SkillIssue.CharacterSpace;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI frameDisplay;
    float deltaTime;
    public bool testing;
    public Character character2;
    public static GameManager instance;
    private void Awake()
    {
        Debug.Log("GameManager Awake");
        QualitySettings.vSyncCount = 0;

        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameManager Start)");
        if (instance != null)
            return;
        instance = this;
    }

    internal void PauseDebug()
    {
        Debug.Break();
    }

    // Update is called once per frame
    void Update()
    {

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        frameDisplay.text = Mathf.Ceil(fps).ToString();

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartRound()
    {
        if (testing)
            return;
        //for now
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void ResetRound()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void EnableTrainingMode()
    {
        testing = !testing;
        character2.inputHandler.ResetAI();
    }
    public void EndGame()
    {
        Application.Quit();
    }

}
