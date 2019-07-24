using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMash : MonoBehaviour
{
    public void DestroyIt()
    {
        Destroy(gameObject);
    }

    public void changeTrans(Vector3 temp)
    {
        print(temp);
        gameObject.transform.position = temp;
        print(gameObject.transform.position);
    }
}
