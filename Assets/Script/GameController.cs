using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       progressAmount = 0;
       progressSlider.value = 0;
       Gem.OnGemCollect += IncreaseProgressAmount;
    }

    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount; 
        if(progressAmount >= 100)
        {
            Debug.Log("level complete");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
