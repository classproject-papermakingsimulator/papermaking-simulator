using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videocontroller : MonoBehaviour
{
    private VideoPlayer video;
    private GameObject targetVideo;
    public GameObject targetVideo1;
    public GameObject targetVideo2;
    public GameObject targetVideo3;
    public GameObject targetVideo4;
    public GameObject targetVideo5;
    public GameObject targetVideo6;

    public void ChangeVideo(int name)
    { 
        switch (name)
        {
            case 1:
                targetVideo = targetVideo1;
                break;
            case 2:
                targetVideo = targetVideo2;
                break;
            case 3:
                targetVideo = targetVideo3;
                break;
            case 4:
                targetVideo = targetVideo4;
                break;
            case 5:
                targetVideo = targetVideo5;
                break;
            case 6:
                targetVideo = targetVideo6;
                break;
            default:break;
        }
        targetVideo.SetActive(true);
        video = targetVideo.GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        if (video.isPaused)
        {
            targetVideo.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
