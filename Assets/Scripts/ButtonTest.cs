using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ButtonTest : MonoBehaviour
{
    float pressRate = 3f;
    float nextPress;
    string POST_URL = @"http://127.0.0.1:5000/predict";
    string savedPath = "C:/Users/Brandon/Desktop/Speech/Chinese/ZaoShangHao/zaoshanghao003.wav";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (nextPress < Time.time)
        {
            StartCoroutine(PostSpeech());
            nextPress = Time.time + pressRate;
            
        }
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
        Debug.Log(convertString(www.text));


    }
    public string convertString(string input)
    {
        string startString = ":";
        string endString = "}";
        int startIndex = input.IndexOf(startString) +2;
        int endIndex = input.IndexOf(endString) -1;
        
        return input.Substring(startIndex, endIndex - startIndex);
    }

}
