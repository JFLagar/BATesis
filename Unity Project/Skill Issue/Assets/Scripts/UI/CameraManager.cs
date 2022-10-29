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
    public bool check = false;
    public bool check2 = false;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        pos.y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
      
        distance = Mathf.Abs(characters[0].transform.position.x - characters[1].transform.position.x);
        cam.orthographicSize = distance / 2;
        if (cam.orthographicSize < 1.25)
        {
            cam.orthographicSize = 1.25f;
        }
        if (cam.orthographicSize > 1.75)
        {
            cam.orthographicSize = 1.75f;
        }
        testmiddle = characters[0].transform.position.x + (characters[1].transform.position.x - characters[0].transform.position.x) / 2;
        pos.x = testmiddle;
        //if (Check())
        //    return;
        gameObject.transform.position = pos;
    }
    public bool Check()
    {

        if (!check && !check2)
            return false;
       
        return true;
    }

}
