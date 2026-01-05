using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

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
