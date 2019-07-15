using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class GridPanel : MonoBehaviour
{

    public Transform[] grids;

    public Transform GetEmptyGrid()
    {

        for (int i = 0; i < grids.Length; i++)
        {

            if (grids[i].childCount == 0)
            {

                return grids[i];

            }

        }

        return null;

    }

}