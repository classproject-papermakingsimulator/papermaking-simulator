using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startpanel : MonoBehaviour
{
    public GameObject Mpanel1;
    public GameObject Mpanel2;


    public void OnStartGameClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnMulGameClick()
    {
        Mpanel1.SetActive(true);
        Mpanel2.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnExitGameClick()
    {
        Application.Quit();
    }
}
