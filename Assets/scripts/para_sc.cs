using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class para_sc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            // collision.gameobject para ile �arp��maya giren gameobject
            //�arp��an gameobject in scriptine eri�ip score u de�i�tirmemiz gerekiyor
            playerMove playerMove1 = collision.gameObject.GetComponent<playerMove>();
            playerMove1.score++;
            //Console.WriteLine("carp��ma");
            Destroy(gameObject);
        }
    }



}
