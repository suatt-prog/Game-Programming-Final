using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenu_sc : MonoBehaviour
{
    public int sahne_kontrol=1;

    public void playGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SaveGame()
    {
        try
        {
            print(SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt("sahne",sahne_kontrol);
            print(sahne_kontrol);
        }
        catch (Exception e)
        {
            print("Exception: " + e.Message);
        }
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("StartMenu"); 
    }

    private void Start()
    {
        print("istek"+PlayerPrefs.GetInt("sahne", 0));
        if (PlayerPrefs.GetInt("hold", 0) ==1)
        {
            sahne_kontrol = 2;
        }
        else if(PlayerPrefs.GetInt("hold", 0) == 0)
        {
            sahne_kontrol = 1;
        }

    }
    private void Update()
    { 
        if(Input.GetAxis("Cancel") > 0.0f && sahne_kontrol == 0)
        {
            sahne_kontrol = 1;
            SceneManager.LoadScene("AnaMenu");   
        }
        else 
        {

        }
    }
}
