using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance;
    public int p1score = -1;
    public int p2score = -1;
    // Start is called before the first frame update
    private void Awake()
    {
        if(ScoreTracker.instance != null)
        {
            DestroyImmediate(this);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        
        
    }

    // Update is called once per frame
   public void AddP1score()
    {
        if(p1score == -1)
        {
            p1score = 0;
        }
        else
        {
            p1score = 1;
            p1score = -1;
            p2score = -1;
        }
    }
    public void AddP2score()
    {
        if (p2score == -1)
        {
            p2score = 0;
        }
        else
        {
            p2score = 1;
            p2score = -1;
            p1score = -1;
        }
    }
    public void ResetAll()
    {
        p1score = -1;
        p2score = -1;
    }
}