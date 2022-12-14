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
    public int p1rounds;
    public int p2rounds;
    public UIBehaviour uIBehaviour;
    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        QualitySettings.vSyncCount = 0;

        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update

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
        p1rounds = ScoreTracker.instance.p1score;
        p2rounds = ScoreTracker.instance.p2score;

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
        ScoreTracker.instance.ResetAll();
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
