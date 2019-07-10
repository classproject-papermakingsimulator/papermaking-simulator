using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cartTelController : MonoBehaviour
{
    public static cartTelController Instance
    {
        get { return s_Instance; }
    }

    protected static cartTelController s_Instance;

    protected HighlightableObject ho;
    protected bool tel;

    public GameObject player;

    public void teleport(GameObject destanition)
    {
        print(destanition.GetComponent<Transform>().position);
        player.transform.position = destanition.GetComponent<Transform>().position;
        s_Instance = this;
        tel = false;
    }

    void Awake()
    {
        ho = gameObject.AddComponent<HighlightableObject>();
    }

    void Update()
    {
        AfterUpdate();
    }

    public bool getTel
    {
        get { return tel; }
    }

    void OnMouseEnter()
    {
        ho.ConstantSwitch();
    }

    void OnMouseExit()
    {
        ho.ConstantOff();
    }

    void OnMouseDown()
    {
        tel = true;
        s_Instance = this;
    }

    protected virtual void AfterUpdate() { }
}
