using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CheckWord : MonoBehaviour
{
    string POST_URL = @"http://127.0.0.1:5000/predict";
    string savedPath = "C:/Users/Brandon/AppData/LocalLow/DefaultCompany/LanguageVR/myfile.wav";
    string predictedKeyword = "init";

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("PostingSpeech");
        StartCoroutine (PostSpeech());
        
    }

    public IEnumerator PostSpeech()
    {
        string filePath = savedPath;
        byte[] postData = File.ReadAllBytes(filePath);

        WWWForm form = new WWWForm();
        form.AddBinaryData("file", postData, "Speech", "audio/wav");
        WWW www = new WWW(POST_URL, form);
        yield return www;

        //THE STRING NEEDED
        predictedKeyword = convertString(www.text);
        Debug.Log(predictedKeyword);
    }

    public string convertString(string input)
    {
        string startString = ":";
        string endString = "}";
        int startIndex = input.IndexOf(startString) + 2;
        int endIndex = input.IndexOf(endString) - 1;

        return input.Substring(startIndex, endIndex - startIndex);
    }
}
