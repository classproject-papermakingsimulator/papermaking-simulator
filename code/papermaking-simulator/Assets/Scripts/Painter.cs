using UnityEngine;

public class Painter : MonoBehaviour
{
    /// <summary>
    /// 画笔的颜色
    /// </summary>
    public Color32 penColor;

    public Transform rayOrigin;

    private RaycastHit hitInfo;
    //这个画笔是不是正在被手柄抓着
    private bool IsGrabbing;
    private static Board board;//设置成类型的成员，而不是类型实例的成员，因为所有画笔都是用的同一个board

    private void Start()
    {
        //将画笔部件设置为画笔的颜色，用于识别这个画笔的颜色
        foreach (var renderer in GetComponentsInChildren<MeshRenderer>())
        {
            if (renderer.transform == transform)
            {
                continue;
            }
            renderer.material.color = penColor;
        }
        if (!board)
        {
            board = FindObjectOfType<Board>();
        }
      
    }

    private void Update()
    {
        Ray r = new Ray(rayOrigin.position, rayOrigin.forward);
        if (Physics.Raycast(r, out hitInfo, 0.1f))
        {
            if (hitInfo.collider.tag == "Board")
            {
                //设置画笔所在位置对应画板图片的UV坐标 
                board.SetPainterPositon(hitInfo.textureCoord.x, hitInfo.textureCoord.y);
                //当前笔的颜色
                board.SetPainterColor(penColor);
                board.IsDrawing = true;
                IsGrabbing = true;
            }
        }
        else if(IsGrabbing)
        {
            board.IsDrawing = false;
            IsGrabbing = false;
        }
    }

}
