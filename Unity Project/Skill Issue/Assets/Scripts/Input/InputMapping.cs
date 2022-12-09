using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InputMapping : MonoBehaviour
{
    public KeyCode[] storedinputs = new KeyCode[3];
    public KeyCode[] storedKeyboardInputs = new KeyCode[3];
    public KeyCode[] storedControllerInputs = new KeyCode[3];
    public KeyCode[] currentControllerInputs = new KeyCode[2];
    public bool gotInput = false;
    public bool waitingForInput = false;
    public TextMeshProUGUI text;
    public string[] texts;
    public int buttonId;
    private bool gettingControllerInputs = false;
    public UserData data = new UserData();
    // Start is called before the first frame update
    void Start()
    {
        //INFO
        /// 330+ Controller Inputs; each controller has 20 buttons
        /// 350+ Controller 1
        /// 370+ Controller 2, etc.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            waitingForInput = true;
        if (waitingForInput)
        {
            MapInput(buttonId);
        }
        
    }
    public void MapInput(int inputID)
    {
        
        int i = 0;
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                KeyCode currentCode = kcode;
                currentControllerInputs[i] = currentCode;
                i++;
                waitingForInput = false;
                if(((int)currentCode) >= 330)
                {
                    gettingControllerInputs = true;
                }
                else
                {
                    gettingControllerInputs = false;
                   
                }

                if (inputID >= 0)
                {
                    if (gettingControllerInputs)
                    {
                        storedControllerInputs[inputID] = currentControllerInputs[1];
                        if(((int)currentCode) > 349)
                        {
                            buttonId++;
                            if (buttonId <= 2)
                                StartMappingInputs(buttonId);
                            else
                                SendData(2);
                        }
                    }
                    else
                    {
                        storedKeyboardInputs[inputID] = currentCode;
                        buttonId++;
                        if (buttonId <= 2)
                            StartMappingInputs(buttonId);
                        else
                            SendData(0);
                    }
                }
                else
                {
                    buttonId = 0;
                    StartMappingInputs(buttonId);
                }


            }
      
        }
    }
    public void WaitForInput()
    {
        waitingForInput = true;
    }
    public void StartMappingInputs(int currentButton =-1)
    {
        text.text = texts[currentButton+1];
        buttonId = currentButton;
         WaitForInput();
        
    }
    public void SendData(int id)
    {
        text.text = "Sending Data.";
        switch (id)
        {
            case 0:
                data.inputsP1 = storedKeyboardInputs;
                break;
            case 1:
                break;
            case 2:
                data.controllerP1 = storedControllerInputs;
                break;
            case 4:
                break;

        }
       
        DataManagment.instance.ReWriteData(data);
    }
    public bool GetInput()
    {
        return gotInput;
    }
}
