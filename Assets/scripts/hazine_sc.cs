using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hazine_sc : MonoBehaviour
{
    Collider2D collider2d;
    int sahne_sayisi;
    bool son_bolumde_miyiz;
    private void Start()
    {
        collider2d = gameObject.GetComponent<Collider2D>();
        sahne_sayisi = SceneManager.sceneCountInBuildSettings;

    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            print("hazine alan�na girildi");
            GoNextLevel();
        }
    }

    public void GoNextLevel()
    {
        print("sahne sayisi" + sahne_sayisi);
        print("build index" + SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex == sahne_sayisi - 1)
        {
            StartCoroutine(yeni_oyuna_gec(0));
        }
        StartCoroutine(yeni_oyuna_gec(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator yeni_oyuna_gec(int index)
    {
        yield return new WaitForSeconds(1.0f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}