using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.ArtificialBased;

public class grabController : MonoBehaviour
{
    public GameObject board;
    public GameObject counter;

    private bool isToggle;
    // Start is called before the first frame update
    void Start()
    {
        counter.transform.position = new Vector3(0, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (counter.transform.position.x == 0)
            isToggle = false;
        else
            isToggle = true;
        if (gameObject.GetComponent<VRTK_ArtificialRotator>().GetStepValue(gameObject.GetComponent<VRTK_ArtificialRotator>().GetValue()) == 1)
        {
            counter.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            counter.transform.position = new Vector3(0, 0, 0);
        }
        if (isToggle)
        {
            if (board.activeSelf)
                board.GetComponent<Board>().confirm();
            else
            {
                gameObject.GetComponent<VRTK_ArtificialRotator>().SetValue(0);
            }
        }
    }
}
        