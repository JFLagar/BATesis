using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.CharacterSpace;

public class CameraManager : MonoBehaviour
{
    private Camera cam;
    public Character[] characters;
    public Vector3 pos = new Vector3(0,0,-10);
    public float distance;
    public float testmiddle;
    public bool check;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        testmiddle = characters[0].transform.position.x + (characters[1].transform.position.x - characters[0].transform.position.x) / 2;
        check = CheckForWalls();
        pos.x = testmiddle;
        if (!CheckForWalls())

        gameObject.transform.position = pos;
    }
    bool CheckForWalls()
    {
        if (characters[0].x == characters[1].x)
        {
            foreach (Character character in characters)
            {
                if (character.wallx == character.x)
                {
                    character.x = -character.wallx;
                }
            }
                return false;
        }
            
        foreach (Character character in characters)
        {
            if (character.wall && character.x == character.wallx)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
        return false;
    }
}
