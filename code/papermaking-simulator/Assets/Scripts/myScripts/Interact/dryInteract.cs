using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dryInteract : MonoBehaviour
{
    bool isDry;
    public GameObject drypaper;
    public GameObject paper;
    public GameObject counter;
    // Start is called before the first frame update
    void Start()
    {
        isDry = false;
        counter.transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (counter.transform.localPosition.x == 0)
        {
            isDry = false;
            drypaper.SetActive(false);
        }
        else
        {
            isDry = true;
            drypaper.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isDry && collision.collider.tag == "wet")
        {
            counter.transform.localPosition = new Vector3(1, 0, 0);
            drypaper.SetActive(true);
            Timer.Register(5f, () => dry());
        }
    }

    private void dry()
    {
        counter.transform.localPosition = new Vector3(0, 0, 0);
        drypaper.SetActive(false);
        paper.GetComponent<paperinteract>().add();
    }
}
