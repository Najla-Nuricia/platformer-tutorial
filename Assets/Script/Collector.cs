using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        IItem item = collision.GetComponent<IItem>();
        if (item != null)
        {
            item.collect();
        }
        
    }
}
