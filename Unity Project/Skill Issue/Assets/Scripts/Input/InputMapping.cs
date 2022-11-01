using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputMapping : MonoBehaviour
{
    public KeyCode[] storedinputs = new KeyCode[1];
    public KeyCode[] storedKeyboardInputs = new KeyCode[1];
    public KeyCode[] storedControllerInputs = new KeyCode[1];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKeyDown)
        {

        }

    }
    public void MapInput(int inputID, bool isController)
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                Debug.Log("KeyCode down: " + kcode);
        }
    }
    public void SendData(int inputID)
    {
        UserData data = new UserData();

        DataManagment.instance.ReWriteData(data);
    }
}
