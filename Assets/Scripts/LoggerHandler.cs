using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoggerHandler : MonoBehaviour
{
    public Transform HMD_Transform;

    public int participantID = 0;

    private float hmdMovement = 0f;
    private float hmdRotation = 0f;
    private Vector3 previousPosition;
    private Quaternion previousRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (participantID != 0)
        {
            Logger.SetID(participantID);
        }
        else
        {
            ReadParticipantIDFromFile();
        }
            
        Logger.Log("APPLICATION", "Started");

    }

    private void ReadParticipantIDFromFile()
    {
        try
        {
            string file = Application.persistentDataPath + "/participantID.txt";

            StreamReader reader = new StreamReader(file);

            string content = reader.ReadLine();

            int idResult;
            if (int.TryParse(content, out idResult))
            {
                Debug.Log("Success: " + idResult);
                Logger.SetID(idResult);
            }
            else
            {
                Debug.Log("Failure: " + idResult);
                Logger.SetID(0);
            }

            reader.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Exception reading participant ID from file: " + e.ToString());
            Logger.SetID(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HMD_Transform != null)
        {
            // HMD data is logged into a separate file.
            Logger.Log("HMD", "POSITION", getHMDPosition());         
            Logger.Log("HMD", "ROTATION", getHMDRotation());

            if (previousPosition != null) {
                hmdMovement += Vector3.Distance(HMD_Transform.position, previousPosition);
            }
            if (previousRotation != null)
            {
                hmdRotation += Quaternion.Angle(HMD_Transform.rotation, previousRotation);
            }

            previousPosition = HMD_Transform.position;
            previousRotation = HMD_Transform.rotation;
        }
    }

    private string getHMDPosition()
    {
        return HMD_Transform.position.x + "," + HMD_Transform.position.y + "," + HMD_Transform.position.z;
    }

    private string getHMDRotation()
    {
        return HMD_Transform.rotation.eulerAngles.ToString();
    }

    private void OnApplicationQuit()
    {
        Logger.Log("HMD", "TOTAL_MOVEMENT", hmdMovement.ToString());
        Logger.Log("HMD", "TOTAL_ROTATION", hmdRotation.ToString());
        Logger.Log("APPLICATION", "Quit");
    }
}
