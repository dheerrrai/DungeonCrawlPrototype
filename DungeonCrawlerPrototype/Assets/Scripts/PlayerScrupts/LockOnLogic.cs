using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;


namespace Stats{
public class LockOnLogic : MonoBehaviour
{
    public Transform SearchableObject;
    public Transform behindCam;
    public Transform player;
    public GameObject lockonIcon;
    public List<Transform> lockOns = new List<Transform>();
    public CinemachineCamera cam;
    [SerializeField] private int range;
    [SerializeField] private int index = 0;
    public Transform lookAtTarget;
    public BasicController controller;
    public void Start()
    {
        cam.Target.TrackingTarget = player;
        cam.Target.LookAtTarget = lookAtTarget;
        controller.lockedOn = false;
        lockOns.Clear();
        lockOns.Add(player);

        SearchNAdd();
    }
    public void OnTransformChildrenChanged()
    {
        SearchNAdd();
    }
    public void SearchNAdd()
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
                lookAtTarget = player; // default
                controller.lockedOn = false;
            }
            else {
                lookAtTarget = lockOns[index];
                controller.lockedOn = true;
                controller.targetLock = lookAtTarget;
                // switch enemy
            }
            cam.Target.LookAtTarget = lookAtTarget;

        }
    }
}
}