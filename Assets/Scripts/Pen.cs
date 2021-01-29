using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    public GameObject Brush;
    public float inkRate = 0.05f;
    float nextInk;
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
        if (other.gameObject.layer == 14 && nextInk < Time.time)
        {
            Vector3 SpawnHere = new Vector3(transform.position.x, transform.position.y, other.transform.position.z);
            //Debug.Log("Trigger On Layer 13");
            nextInk = Time.time + inkRate;
            Instantiate(Brush, SpawnHere, other.transform.rotation * Quaternion.Euler(0f, 0f, 0f));
        }
    }
}
