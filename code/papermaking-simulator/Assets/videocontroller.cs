using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videocontroller : MonoBehaviour
{
    public VideoPlayer video;

    private void Update()
    {
        if (video.isPaused)
            gameObject.SetActive(false);
    }
}
