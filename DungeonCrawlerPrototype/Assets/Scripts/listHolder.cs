using System.Collections.Generic;
using UnityEngine;
namespace Stats
{
public class listHolder : MonoBehaviour
{
    public GameObject explosionEffect;
    public List<GameObject> explosions;
    public Transform explosionParent;


    public void Start()
    {
        for (int i = 0; i < explosionParent.childCount; i++)
        {
            explosions.Add(explosionParent.GetChild(i).gameObject);
            Debug.Log("Added Child");
        }
    }

}
}