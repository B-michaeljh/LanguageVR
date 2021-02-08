using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DialogueHandler : MonoBehaviour
{
    public NPC npc;

    bool isTalking = false;

    float distance;
    int responseTracker;
    float dialogueRate = 1f;
    float nextDialogue;

    public GameObject player;
    public GameObject dialogueUI;

    public Text NPC_Name;
    public Text NPC_Dialogue;

    string savedPath;
    AudioSource source;

    string predictedKeyword;

    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false);
        savedPath = Path.Combine(Application.persistentDataPath, "myfile.wav");
        System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => {
            return true;
        };
    }

    void OnTriggerStay(Collider other)
    {
        // Button One   is A
        // Button Two   is B
        // Button Three is X
        // Button Four  is Y

        if (other.gameObject.layer == 10 && nextDialogue < Time.time)
        {
            nextDialogue = Time.time + dialogueRate;
            // Start conversation with NPC
            if (isTalking == false && OVRInput.Get(OVRInput.Button.One))
            {
                responseTracker = 0;
                StartConversation();
            }
            // End conversation with NPC
            else if (isTalking == true && OVRInput.Get(OVRInput.Button.Two))
            {
                responseTracker = 0;
                EndDialogue();
            }


            // Record the phrase
            if (isTalking == true && OVRInput.Get(OVRInput.Button.Three))
            {
                // Record the speech
                Debug.Log("RECORDING STARTED");
                RecordSpeech();
                Debug.Log("RECORDING FINISHED");
            }

            // Play the recorded phrase
            //if (isTalking == true && OVRInput.Get(OVRInput.Button.Four))
            //{

            //}

            // Validate conversation with NPC
            if (isTalking == true && OVRInput.Get(OVRInput.Button.Four))
            {
                // Post the phrase to check
                StartCoroutine(PostSpeech());
                Debug.Log("Phrase Expected: " + npc.playerDialogue[responseTracker]);
                Debug.Log("Phrase Predicted: " + predictedKeyword);
                // If phrase is correct
                if (predictedKeyword == npc.playerDialogue[responseTracker])
                {
                    responseTracker++;
                    NPC_Dialogue.text = npc.dialogue[responseTracker];
                }
                // If phrase is wrong
                else {
                    NPC_Dialogue.text = "Phrase Expected: " + npc.playerDialogue[responseTracker] + "Phrase Predicted: " + predictedKeyword;

                }
            }
        }

    }
    // Initialise dialogue text box
    void StartConversation()
    {
        isTalking = true;
        responseTracker = 0;
        dialogueUI.SetActive(true);
        NPC_Name.text = npc.name;
        NPC_Dialogue.text = npc.dialogue[responseTracker];
    }

    // End dialogue text box
    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
    }


    
    AudioClip clip;

    void RecordSpeech()
    {
        // Enables recording from microphone
        clip = Microphone.Start("Headset Microphone (Rift S)", true, 2, 16000);

        // Need pause for recording
        System.Threading.Thread.Sleep(3000);

        // Stops recording 
        Microphone.End("Headset Microphone (Rift S)");

        // Saves audio clip
        SavWav.Save("myfile", clip);

    }

    string POST_URL = @"http://127.0.0.1:5000/predict";



    public IEnumerator PostSpeech()
    {
        
        byte[] postData = File.ReadAllBytes(savedPath);

        WWWForm form = new WWWForm();
        form.AddBinaryData("file", postData, "Speech", "audio/wav");
        WWW www = new WWW(POST_URL, form);
        yield return www;

        //THE STRING NEEDED
        predictedKeyword = convertString(www.text);
        //Debug.Log("predicted keyword is: " + predictedKeyword);
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
