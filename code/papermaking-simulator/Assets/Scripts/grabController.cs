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
        if (gameObject.GetComponent<VRTK_ArtificialRotator>().GetStepValue(gameObject.GetComponent<VRTK_ArtificialRotator>().GetValue()) >= 0.9)
        {
            counter.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            counter.transform.position = new Vector3(0, 0, 0);
        }
        if (counter.transform.position.x <= 0.5)
            isToggle = false;
        else
            isToggle = true;
        //print(gameObject.GetComponent<VRTK_ArtificialRotator>().GetStepValue(gameObject.GetComponent<VRTK_ArtificialRotator>().GetValue())); 
        if (isToggle)
        {
            if (board.activeSelf)
            {
                print("准备保存");
                board.GetComponent<Board>().confirm();
            } 
            else
            {
                gameObject.GetComponent<VRTK_ArtificialRotator>().SetValue(0);
            }
        }
    }
}
        