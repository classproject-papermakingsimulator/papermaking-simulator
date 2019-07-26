using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dryInteract : MonoBehaviour
{
    bool isDry;
    public GameObject drypaper;
    public GameObject paper;
    // Start is called before the first frame update
    void Start()
    {
        isDry = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isDry && collision.collider.tag == "wet")
        {
            isDry = true;
            drypaper.SetActive(true);
            Timer.Register(5f, () => dry());
        }
    }

    private void dry()
    {
        isDry = false;
        drypaper.SetActive(false);
        paper.GetComponent<paperinteract>().add();
    }
}
