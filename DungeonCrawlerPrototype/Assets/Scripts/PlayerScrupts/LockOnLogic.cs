using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;


namespace Stats{
public class LockOnLogic : MonoBehaviour
{
    public Transform SearchableObject;
    public Transform defaultTarget;
    public GameObject lockonIcon;
    public List<Transform> lockOns = new List<Transform>();
    public bool isLockedOn = false;
    public CinemachineCamera cam;
    [SerializeField] private int range;
    [SerializeField] private int index = 0;
    public Transform lookAtTarget;

    //public BasicController controller;
    public void Start()
    {
        cam.Target.TrackingTarget = defaultTarget;
        cam.Target.LookAtTarget = lookAtTarget;
        isLockedOn = false;
            lockOns.Clear();
        lockOns.Add(defaultTarget);

        SearchNAdd();
    }
    public void OnTransformChildrenChanged()
    {
        SearchNAdd();
    }
    public void SearchNAdd() //Searches through Searchable Parent and finds all children and 
    {
        foreach (Transform childTransform in SearchableObject)
        {
            if (!lockOns.Contains(childTransform))
            {
                Transform Child = childTransform.GetComponent<Transform>();

                lockOns.Add(Child);
            }
        }
        range = lockOns.Count;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            index++;

            if (index >= lockOns.Count)
                index = 0;

            if (index == 0) {
                lookAtTarget = defaultTarget; // default
                    isLockedOn = false;
            }
            else {
                lookAtTarget = lockOns[index];
                    isLockedOn = true;
                }
            cam.Target.LookAtTarget = lookAtTarget;

        }
    }
}
}