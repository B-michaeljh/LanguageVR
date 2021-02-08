using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    public GameObject Brush;
    public float inkRate = 0.05f;
    float nextInk;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 14 && nextInk < Time.time)
        {
            Vector3 SpawnHere = new Vector3(transform.position.x, transform.position.y, other.transform.position.z);
            nextInk = Time.time + inkRate;
            Instantiate(Brush, SpawnHere, other.transform.rotation);
        }
        else if (other.gameObject.layer == 15 && nextInk < Time.time)
        {
            Vector3 SpawnHere = new Vector3(other.transform.position.x, transform.position.y, transform.position.z);
            nextInk = Time.time + inkRate;
            Instantiate(Brush, SpawnHere, other.transform.rotation);
        }

    }
}
