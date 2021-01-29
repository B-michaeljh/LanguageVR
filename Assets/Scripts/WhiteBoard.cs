using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBoard : MonoBehaviour
{
    
    public GameObject Brush;
    public float inkRate;
    float nextInk;
    // Start is called before the first frame update
    void Start()
    {
        nextInk = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 13 && nextInk < Time.time)
        {
            Vector3 SpawnHere = other.transform.position;
            //Debug.Log("Trigger On Layer 13");
            nextInk = Time.time + inkRate;
            Instantiate(Brush, SpawnHere, transform.rotation * Quaternion.Euler(0f,0f,0f));
        }
    }

}
