using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;
using System;

using UnityEngine.EventSystems;
public class ItemImage : MonoBehaviour, IPointerExitHandler, IPointerClickHandler
{

    public Text itemText;
    public Text numText;
    public static Action<Transform> OnEnter;
    public static Action OnExit;
    public void OnPointerClick(PointerEventData eventData)//将物品移出
    {
        OnEnter?.Invoke(transform);
  
        if (eventData.pointerCurrentRaycast.gameObject != null)

        {
            //获取UI信息
            Text info=GetComponent<ItemImage>().itemText;
            Text num = GetComponent<ItemImage>().numText;
            
            //更新ItemModel，数量减一
            Item item = ItemModel.GetItem(info.text);
            if (!item.DeleteNum())
                Debug.Log("数量不足");
            ItemModel.gridItem[info.text] = item;
            
            //更新UI
            GetComponent<ItemImage>().UpdateNum(item.Num);
         

        }


    }


   
    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnExit != null)
        {
            OnExit();
        }
    }

    public void UpdateItem(string name)
    {

        itemText.text = name;

    }

    public void UpdateNum(int num)
    {
        numText.text = num.ToString();
    }

    private object str(int num)
    {
        throw new NotImplementedException();
    }

    //实际多是图片，可以使用这个更新图片

    public Image itemImage;

    public void UpdateItemImage(Sprite icon)

    {

        itemImage.sprite = icon;

    }

    /*public void OnPointerExit(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
    */
}