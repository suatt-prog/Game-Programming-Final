using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void yeni_oyun()
    {
        StartCoroutine(yeni_oyuna_gec());
    }

    public void devam()
    {
        int sahneindex=PlayerPrefs.GetInt("sahne",0);
        print(sahneindex);
        SceneManager.LoadScene(sahneindex);
    }

    IEnumerator yeni_oyuna_gec()
    {
        yield return new WaitForSeconds(1.0f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
