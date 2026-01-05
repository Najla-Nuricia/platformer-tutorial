using System.Reflection.Metadata;
using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class Gem : MonoBehaviour, IItem
{
    public static event Action<int> OnGemCollect;
    public int worth = 5;

    public void collect()
    {
        OnGemCollect.invoke(worth);
        Destroy(gameObject);
    }
}
