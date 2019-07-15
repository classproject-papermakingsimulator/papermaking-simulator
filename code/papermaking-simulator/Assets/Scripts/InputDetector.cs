using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int itemId = 0; itemId < 7; itemId++)
        {   Debug.Log(itemId);
            KnapsackManager.Instance.StoreItem(itemId);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
     /*if(Input.GetMouseButtonDown(2))
        {

            for (int itemId = 0; itemId < 6; itemId++)
            {
                KnapsackManager.Instance.StoreItem(itemId);
                Debug.Log(itemId);
            }
        }*/
    }
}
