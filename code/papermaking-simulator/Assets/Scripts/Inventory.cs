using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private GameObject obj;
    private bool state;
    ItemImage itemImage;
    // Start is called before the first frame update
    void Start()
    {
        obj=GameObject.Find("GridPanel");
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInventory()
    {
        Debug.Log("OnMouseDown response");

        if (state == true)
        {
            obj.SetActive(false);
            state = false;

        }
        else
        {
            if (state == false)
            {
                obj.SetActive(true);
                state = true;
            }
        }
    }


    public void UpdateNumWhenAdd(Transform target)//以指定“待去皮竹子”为例
    {


        Transform go = GameObject.Find("GridImage").transform;
        //更新ItemModel，数量加一
        Item item = ItemModel.GetItem("待栽泡");
        item.AddNum();
        ItemModel.gridItem["待栽泡"] = item;
        //更新UI
        GetChild(go, 0).gameObject.GetComponent<ItemImage>().UpdateNum(item.Num);


    }

    private Transform GetChild(Transform tr, int index)
    {

        return tr.GetChild(index);

    }
}
