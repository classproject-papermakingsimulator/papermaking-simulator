using System.Collections;

using System.Collections.Generic;
using System.Text;
using UnityEngine;


using UnityEngine.UI;
using System;

using UnityEngine.EventSystems;

public class KnapsackManager : MonoBehaviour
{

    public GridPanel gridPanel;

    private Dictionary<int, Item> itemList;

    private static KnapsackManager instance;

    public static KnapsackManager Instance
    {

        get
        {

            return instance;

        }

    }
    public ToolTip toolTip;
    private bool isShow = false;
    void Awake()
    {

        instance = this;

        this.LoadData();

        ItemImage.OnEnter += GridImage_OnEnter;
        ItemImage.OnExit += GridImage_OnExit;
    }

    private void GridImage_OnEnter(Transform obj)
    {
        Item item =ItemModel.GetItem(obj.name);
        if (item == null)
        {
            return;
        }
        Debug.Log(item.Name);
        Debug.Log("iii");


    }

    private void GridImage_OnExit()
    {
        
        Debug.Log("离开");
        //isShow = false;
        //toolTip.Hide();

    }

    private string GetTooltipText(Item item)
    {
        if (item == null) return "";
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<color = red >{0}</color>\n\n", item.Name);
        return sb.ToString();
    }

    private void Update()
    {
        if (isShow)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GameObject.Find("KnapsackUI").transform as RectTransform,
                Input.mousePosition,
                null,
                out position
                );
        }
    }
    public void StoreItem(int itemId)
    {

        if (!itemList.ContainsKey(itemId))
        {

            return;

        }

        Transform emptyGrid = gridPanel.GetEmptyGrid();

        if (emptyGrid == null)
        {

            Debug.Log("背包已满");

            return;

        }

        Item item = itemList[itemId];

        

        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/ItemImage");

        itemPrefab.GetComponent<ItemImage>().UpdateItem(item.Name);
        itemPrefab.GetComponent<ItemImage>().UpdateNum(item.Num);

        GameObject itemGo = GameObject.Instantiate(itemPrefab);

        itemGo.transform.SetParent(emptyGrid);

        itemGo.transform.localPosition = Vector3.zero;

        itemGo.transform.localScale = Vector3.one;


        ItemModel.StoreItem(item.Name, item);

    }

    //模拟数据库数据加载

    private void LoadData()
    {

        itemList = new Dictionary<int, Item>();

        Item item1 = new Item(0, "待栽泡", 2);

        Item item2 = new Item(1, "待蒸煮", 2);
        Item item3 = new Item(2, "待舂臼", 2);
        Item item4 = new Item(3, "待荡料入帘", 2);
        Item item5 = new Item(4, "待压纸", 2);
        Item item6 = new Item(5, "待烘干", 2);
        Item item7 = new Item(6, "成品纸张", 2);
        itemList.Add(item1.Id, item1);

        itemList.Add(item2.Id, item2);

        itemList.Add(item3.Id, item3);

        itemList.Add(item4.Id, item4);

        itemList.Add(item5.Id, item5);

        itemList.Add(item6.Id, item6);

        itemList.Add(item7.Id, item7);

    }

}