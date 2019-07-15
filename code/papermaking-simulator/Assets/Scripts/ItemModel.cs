using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemModel
{
    public static Dictionary<string, Item> gridItem =
        new Dictionary<string, Item>();
    public static void StoreItem(string name, Item item)
    {
        if (gridItem.ContainsKey(name))
        {
            DeleteItem(name);
        }
        gridItem.Add(name, item);
    }
    public static Item GetItem(string name)
    {
        if (gridItem.ContainsKey(name))
        {
            return gridItem[name];
        }
        else
        {
            return null;
        }
    }
    public static void DeleteItem(string name)
    {
        if (gridItem.ContainsKey(name))
        {
            gridItem.Remove(name);
        }
    }
}