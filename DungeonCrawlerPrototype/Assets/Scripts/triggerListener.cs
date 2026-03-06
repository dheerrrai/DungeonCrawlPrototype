using System;
using System.Collections.Generic;
using UnityEngine;
namespace Stats
{
    public class triggerListener : MonoBehaviour
{
    public listHolder ListMan;
    public List<GameObject> explosions;

    public string Tag = "Attack";
    public int index = 0;
    List<ParticleCollisionEvent> PList = new();

    public void Start()
    {
        ListMan = GetComponentInParent<listHolder>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(Tag))
        {
            var ps = other.GetComponent<ParticleSystem>();
            int count = ps.GetCollisionEvents(gameObject, PList);
            Vector3 Pos = PList[0].intersection;
            if (index >= ListMan.explosions.Count)
            {
                index = 0;
            }

            ListMan.explosions[index].transform.position = PList[0].intersection;
            ListMan.explosions[index].SetActive(true);
            { index += 1; }
        }
    }


}}
