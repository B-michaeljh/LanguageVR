using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ButtonTest : MonoBehaviour
{
    string savedPath;
    AudioClip clip;
    
    void Start()
    {
        // Mono default behavior does not trust any server;
        // the following is a workaround (there is a better solution to this issue which can be found in the Internet.  
        System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => {
            return true;
        };

        // Output the device details available in the system, helpful when want to check capability of each device.
        //foreach (string device in Microphone.devices)
        //{
        //    Debug.Log("Name: " + device);
        //}
    }

    // Trigger for Recording Speech
    public void OnTriggerEnter(Collider other)
    {
        //ADD SOME COUNTDOWN HERE
        RecordSpeech();
    }

    void RecordSpeech()
    {
        // Enables recording from microphone
        Debug.Log("Recording has started");
        clip = Microphone.Start("Headset Microphone (Rift S)", true, 2, 16000);

        // Need pause for recording
        System.Threading.Thread.Sleep(3000);

        // Stops recording 
        Debug.Log("Recording has stopped");

        Microphone.End("Headset Microphone (Rift S)");
        SavWav.Save("myfile", clip);
        savedPath = Path.Combine(Application.persistentDataPath, "myfile.wav");

    }
}
