using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class Gem : MonoBehaviour, IItem
{
    public static event Action<int> OnGemCollect;
    public int worth = 5;

    public void collect()
    {
        OnGemCollect.Invoke(worth);
        Destroy(gameObject);
    }
}
