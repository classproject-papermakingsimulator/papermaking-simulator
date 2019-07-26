using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class filterInteract : MonoBehaviour
{
    public GameObject l1;
    public GameObject l2;
    public GameObject l3;
    public GameObject l4;
    public GameObject wetpapers;
    private bool isWater = false;
    private bool isPaper = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "tub")
        {
            if (!isWater)
            {
                isWater = true;
            }
        }
        if(other.tag == "plane" && isPaper)
        {

        }
        if (other.tag == "wets")
        {
            wetpapers.GetComponent<wetpaper>().add();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.collider.tag == "wets")
        //{
        //    wetpapers.GetComponent<wetpaper>().add();
        //}
    }

    private void Update()
    {
        if (isWater)
        {
            GameObject target = minPoint();
            if(!target.transform.Find("Rain Basic").GetComponent<ParticleSystem>().isPlaying)
                target.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Play();
        }
    }

    private GameObject minPoint()
    {
        Transform point1 = l1.transform.Find("point").GetComponent<Transform>();
        Transform point2 = l2.transform.Find("point").GetComponent<Transform>();
        Transform point3 = l3.transform.Find("point").GetComponent<Transform>();
        Transform point4 = l4.transform.Find("point").GetComponent<Transform>();
        if (point1.position.y == Mathf.Min(point1.position.y, point2.position.y, point3.position.y, point4.position.y))
        {
            l2.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Stop();
            l3.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Stop();
            l4.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Stop();
            return l1;
        }
        if(point2.position.y == Mathf.Min(point1.position.y, point2.position.y, point3.position.y, point4.position.y))
        {
            l1.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Stop();
            l3.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Stop();
            l4.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Stop();
            return l2;
        }
        if (point3.position.y == Mathf.Min(point1.position.y, point2.position.y, point3.position.y, point4.position.y))
        {
            l1.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Stop();
            l2.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Stop();
            l4.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Stop();
            return l3;
        }
        l1.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Stop();
        l2.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Stop();
        l3.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Stop();
        return l4;
    }
}
