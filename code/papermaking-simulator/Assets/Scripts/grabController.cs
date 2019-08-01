using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.ArtificialBased;

public class grabController : MonoBehaviour
{
    public GameObject board;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<VRTK_ArtificialRotator>().GetStepValue(gameObject.GetComponent<VRTK_ArtificialRotator>().GetValue()) == 1)
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
        