using UnityEngine;


public class BasicController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private int EXP;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] public Transform player;
    [SerializeField] public Rigidbody playerRb;
    [SerializeField] private float rotateSpeed = 50f;
    [SerializeField] private float dampAmt = 8f;
    public GameObject projectileParticle;
    public Transform projectileLocation;
    public bool lockedOn = false;
    public Transform targetLock;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");

        // Camera Values
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        // Movement Calcs
        Vector3 inputDir = (camForward * verInput) + (camRight * horInput);
        Vector3 targetVel = inputDir.normalized * moveSpeed;

        // Rotation
        if (inputDir.sqrMagnitude > 0.01f && !lockedOn)
        {
            Quaternion targetRot = Quaternion.LookRotation(inputDir);
            playerRb.MoveRotation(Quaternion.RotateTowards(
                playerRb.rotation,
                targetRot,
                rotateSpeed * Time.deltaTime
            ));
        }
        else if(lockedOn)
        {
            if(targetLock != null)
            {
                transform.LookAt(targetLock);
            }
            else
            {
                Debug.Log("TargetNotGiven");
            }
        }

        // Movement - Cam Relative
        if (inputDir.sqrMagnitude < 0.01f)
        {
            // Smooth stop
            playerRb.linearVelocity = Vector3.Lerp(
                playerRb.linearVelocity,
                Vector3.zero,
                Time.deltaTime * dampAmt
            );
        }
        else
        {
            // Apply desired velocity force once
            Vector3 velError = targetVel - playerRb.linearVelocity;
            playerRb.AddForce(velError, ForceMode.Force);
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject rangedAttack = Instantiate(projectileParticle, projectileLocation.position, projectileLocation.rotation);

        }
    }

}
