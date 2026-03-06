using UnityEngine;
namespace Stats
{
public class groundCheck : MonoBehaviour
{
    public string GroundTag;
    public BasicController controller;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GroundTag))
        {
            controller.CanJump = true;
        }
    }
    //public OnTrigger
}
}