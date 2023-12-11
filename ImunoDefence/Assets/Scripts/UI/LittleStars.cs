using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;

public class LittleStars : MonoBehaviour

{
    public GameObject[] littleStar1;
    public static LittleStars instance;
    public bool filled = false;
    void Start()
    {
        littleStar1[1].SetActive(false);
        littleStar1[2].SetActive(false);
        littleStar1[3].SetActive(false);
        littleStar1[3].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void estrelas()
    {
        if (LifeManager.instance.getHP() == 10)
        {
            littleStar1[1].SetActive(true);
            littleStar1[2].SetActive(true);
            littleStar1[3].SetActive(true);
            littleStar1[3].SetActive(true);
        }
        if (LifeManager.instance.getHP() >= 7 && LifeManager.instance.getHP() <= 9)
        {
            littleStar1[1].SetActive(true);
            littleStar1[2].SetActive(true);
        }
        if (LifeManager.instance.getHP() >= 3 && LifeManager.instance.getHP() <= 6)
        {
            littleStar1[1].SetActive(true);
        }
        if (LifeManager.instance.getHP() <= 3)
        {

        }
    }
}
