using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_ControllerTest : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            if (OVRInput.Get(OVRInput.Button.Four))
            {
                Debug.Log("BUTTON Y HAS BEEN PRESSED");
            }
            if (OVRInput.Get(OVRInput.Button.Three))
            {
                Debug.Log("BUTTON X HAS BEEN PRESSED");
            }
            if (OVRInput.Get(OVRInput.Button.Two))
            {
                Debug.Log("BUTTON B HAS BEEN PRESSED");
            }
            if (OVRInput.Get(OVRInput.Button.One))
            {
                Debug.Log("BUTTON A HAS BEEN PRESSED");
            }



        }

    }
    //void Update()
    //{
    //    if (OVRInput.Get(OVRInput.Button.Four))
    //    {
    //        Debug.Log("Y HAS BEEN PRESSED");
    //    }
    //    if (OVRInput.Get(OVRInput.Button.Three))
    //    {
    //        Debug.Log("X HAS BEEN PRESSED");
    //    }
    //    if (OVRInput.Get(OVRInput.Button.Two))
    //    {
    //        Debug.Log("B HAS BEEN PRESSED");
    //    }
    //    if (OVRInput.Get(OVRInput.Button.One))
    //    {
    //        Debug.Log("A HAS BEEN PRESSED");
    //    }
    //}
}
