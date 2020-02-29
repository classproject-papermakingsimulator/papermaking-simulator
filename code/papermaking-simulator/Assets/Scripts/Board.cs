using Photon.Pun;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 画板
/// </summary>
public class Board : MonoBehaviour
{
    //当画笔移动速度很快时，为了不出现断断续续的点，所以需要对两个点之间时行插值，lerp就是插值系数
    [Range(0, 1)]
    public float lerp = 0.05f;
    //初始化背景的图片
    public Texture2D initailizeTexture;
    //当前背景的图片
    private Texture2D currentTexture;
    //画笔所在的位置 
    private Vector2 paintPos;

    private bool isDrawing = false;//当前画笔是不是正在画板上
    //离开时画笔所在的位置 
    private int lastPaintX;
    private int lastPaintY;
    //画笔所代表的色块的大小
    private int painterTipsWidth = 8;
    private int painterTipsHeight = 8;
    //当前画板的背景图片的尺寸
    private int textureWidth;
    private int textureHeight;

    //画笔的颜色
    private Color32[] painterColor;

    private Color32[] currentColor;
    private Color32[] originColor;
    public GameObject counter;
    private bool isDone;


    private void Start()
    {
        //获取原始图片的大小 
        Texture2D originTexture = GetComponent<MeshRenderer>().material.mainTexture as Texture2D;
        textureWidth = originTexture.width;//1920   
        textureHeight = originTexture.height;//1080

        //设置当前图片
        currentTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false, true);
        currentTexture.SetPixels32(originTexture.GetPixels32());
        currentTexture.Apply();

        //赋值给黑板
        GetComponent<MeshRenderer>().material.mainTexture = currentTexture;

        //初始化画笔的颜色
        painterColor = Enumerable.Repeat<Color32>(new Color32(255, 0, 0, 255), painterTipsWidth * painterTipsHeight).ToArray<Color32>();

        isDone = false;
    }

    private void LateUpdate()
    {
        //计算当前画笔，所代表的色块的一个起始点
        int texPosX = (int)(paintPos.x * (float)textureWidth - (float)(painterTipsWidth / 2));
        int texPosY = (int)(paintPos.y * (float)textureHeight - (float)(painterTipsHeight / 2));
        if (isDrawing)
        {
            if(texPosX > 0 && texPosY > 0 && texPosX < (float)textureWidth - (float)painterTipsWidth && texPosY < (float)textureHeight - (float)painterTipsHeight)
            //改变画笔所在的块的像素值
            currentTexture.SetPixels32(texPosX, texPosY, painterTipsWidth, painterTipsHeight, painterColor);
            //如果快速移动画笔的话，会出现断续的现象，所以要插值
            if (lastPaintX != 0 && lastPaintY != 0)
            {
                int lerpCount = (int)(1 / lerp);
                for (int i = 0; i <= lerpCount; i++)
                {
                    int x = (int)Mathf.Lerp((float)lastPaintX, (float)texPosX, lerp);
                    int y = (int)Mathf.Lerp((float)lastPaintY, (float)texPosY, lerp);
                    currentTexture.SetPixels32(x, y, painterTipsWidth, painterTipsHeight, painterColor);
                }
            }
            currentTexture.Apply();
            lastPaintX = texPosX;
            lastPaintY = texPosY;
        }
        else
        {
            lastPaintX = lastPaintY = 0;
        }
        

    }

    /// <summary>
    /// 设置当前画笔所在的UV位置
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetPainterPositon(float x, float y)
    {
        paintPos.Set(x, y);
    }

    /// <summary>
    /// 画笔当前是不是在画画
    /// </summary>
    public bool IsDrawing
    {
        get
        {
            return isDrawing;
        }
        set
        {
            isDrawing = value;
        }
    }

    /// <summary>
    /// 使用当前正在画板上的画笔的颜色
    /// </summary>
    /// <param name="color"></param>
    public void SetPainterColor(Color32 color)
    {
        if (!painterColor[0].IsEqual(color))
        {
            for (int i = 0; i < painterColor.Length; i++)
            {
                painterColor[i] = color;
            }
        }
    }

    public void save()
    {
        if (isDone)
        {
            byte[] dataBytes = currentTexture.EncodeToPNG();
            string strSaveFile = Application.dataPath + "/" + System.DateTime.Now.Minute + "_" + System.DateTime.Now.Second + ".jpg";
            FileStream fs = File.Open(strSaveFile, FileMode.OpenOrCreate);
            //fs.Write(dataBytes, 0, dataBytes.Length);
            BinaryWriter writer = new BinaryWriter(fs);
            writer.Write(dataBytes);
            fs.Flush();
            fs.Close();
            //gameObject.SetActive(false);
            StartCoroutine(Upload(strSaveFile));

        }

    }

    IEnumerator Upload(string path)
    {
        byte[] dataBytes = currentTexture.EncodeToPNG();
        counter.transform.position = new Vector3(counter.transform.position.x, counter.transform.position.y, 0);
        WWWForm form = new WWWForm();
        UnityWebRequest file = new UnityWebRequest();
        file = UnityWebRequest.Get(path);
        form.AddBinaryData("file", dataBytes, Path.GetFileName(path));
        UnityWebRequest request = UnityWebRequest.Post("http://papermakingshare.cn:8900/api/share?token=1", form);
        yield return request.SendWebRequest();
        Debug.Log(request.error);
        Debug.Log(request.responseCode);
        Debug.Log(request.downloadHandler.text);
    }

    public void confirm()
    {
        //print("按钮触发");
        isDone = true; 
    }


}
public static class MethodExtention
{
    /// <summary>
    /// 用于比较两个Color32类型是不是一样
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="compare"></param>
    /// <returns></returns>
    public static bool IsEqual(this Color32 origin, Color32 compare)
    {
        if (origin.g == compare.g && origin.r == compare.r)
        {
            if (origin.a == compare.a && origin.b == compare.b)
            {
                return true;
            }
        }
        return false;
    }
}