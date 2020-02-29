using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class filterInteract : MonoBehaviour
{
    private AudioSource audio;
    public GameObject uper;
    public GameObject downer;
    public GameObject l1;
    public GameObject l2;
    public GameObject l3;
    public GameObject l4;
    public GameObject wetpapers;
    private bool isWater = false;
    private bool isPaper = false;
    private int count = 0;
    private bool hasCollided = false;

    private void OnTriggerEnter(Collider other)
    {
        if (this.hasCollided == true) { return; }
        this.hasCollided = true;
        if (other.tag == "tub")         
        {
            audio.Play();
            if (!isWater)
            {
                isWater = true;
            }
            countjudge();
        }
        if (other.tag == "wets" && isPaper)
        {
            wetpapers.GetComponent<wetpaper>().add();
            isPaper = false;
            uper.SetActive(false);
            downer.SetActive(false);
        }
        Timer.Register(0.4f, () => { this.hasCollided = false; });
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.collider.tag == "wets")
        //{
        //    wetpapers.GetComponent<wetpaper>().add();
        //}
    }

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isWater)
        {
            GameObject target = minPoint();
            if(!target.transform.Find("Rain Basic").GetComponent<ParticleSystem>().isPlaying)
                target.transform.Find("Rain Basic").GetComponent<ParticleSystem>().Play();
        }
        if(count >= 1)
        {
            count = 0;
            isPaper = true;
            uper.SetActive(true);
            downer.SetActive(true);
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

    private void countjudge()
    {
        Transform point1 = l1.transform.Find("point").GetComponent<Transform>();
        Transform point2 = l2.transform.Find("point").GetComponent<Transform>();
        Transform point3 = l3.transform.Find("point").GetComponent<Transform>();
        Transform point4 = l4.transform.Find("point").GetComponent<Transform>();
        double angle;
        if (point1.position.y == Mathf.Min(point1.position.y, point2.position.y, point3.position.y, point4.position.y))
        {
            angle = Mathf.Abs((point2.position.y - point1.position.y) / (point2.position.x - point1.position.x));
            if (angle < 1 && angle > 0.267)
            {
                count++;
            }
            return;
        }
        if (point2.position.y == Mathf.Min(point1.position.y, point2.position.y, point3.position.y, point4.position.y))
        {
            angle = Mathf.Abs((point2.position.y - point1.position.y) / (point2.position.x - point1.position.x));
            if (angle < 1 && angle > 0.267)
            {
                count++;
            }
            return;
        }
        angle = Mathf.Abs((point3.position.y - point4.position.y) / (point3.position.x - point4.position.x));
        if (angle < 1 && angle > 0.267)
        {
            count++;
        }
        return;
    }
}
