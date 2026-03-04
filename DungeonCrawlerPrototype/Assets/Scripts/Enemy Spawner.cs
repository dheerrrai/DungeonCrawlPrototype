using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();
    public int EnemyListCount;
    int id;
    public Transform SpawnParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyListCount = EnemyList.Count;
    }
    
    public void SpawnInstance(Vector3 Location)
    {
        id = Random.Range(0, EnemyListCount);
        GameObject newEnemy = Instantiate(EnemyList[id], Location, EnemyList[id].transform.rotation, SpawnParent);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
