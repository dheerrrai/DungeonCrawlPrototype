using System;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Events;

namespace Stats
{
    public class BasicController : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] PlayerStats playerStats;
        [SerializeField] public Transform player;
        [SerializeField] public Rigidbody playerRb;
        [Header("Movement Stats")]
        public float dashForce = 10f;
        public float dashCD = 0f;


        [SerializeField] private float moveSpeed = 25f;
        [SerializeField] private float rotateSpeed = 50f;
        [SerializeField] private float dampAmt = 8f;
        [SerializeField] private float jumpForce = 100f;
        public bool CanJump = false;

        public bool lockedOn = false;

        public Transform targetLock;

        [Header("Events")]
        public UnityEvent<int> attackAction;
        public UnityEvent<int> takeDamage;
        public UnityEvent<int> heal;
        //public Action jumpAction;
        //S

        [Header("References for functions")]

        public string groundTag = "Ground";

        public GameObject projectileParticle;
        public GameObject projectileParent;
        public List<GameObject> projectile = new();
        public Transform projectileLocation;

        public LockOnLogic lockOnScript;


        float horInput;
        float verInput;
        Vector3 inputDir;

        public int lockOnIndex = 0;

        void Start()
        {
            
            
            for (int i = 0; i < projectileParent.transform.childCount; i++)
            {
                projectile.Add(projectileParent.transform.GetChild(i).gameObject);
                Debug.Log("Added Child");
            }
        }


        void FixedUpdate()
        {

            // Camera Values
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            // Movement Calcs
            inputDir = (camForward * verInput) + (camRight * horInput);
            Vector3 targetVel = inputDir.normalized * moveSpeed;

            // Rotation
            if (inputDir.sqrMagnitude > 0.01f && !lockOnScript.isLockedOn)
            {
                Quaternion targetRot = Quaternion.LookRotation(inputDir);
                playerRb.MoveRotation(Quaternion.RotateTowards(
                    playerRb.rotation,
                    targetRot,
                    rotateSpeed * Time.deltaTime
                ));
            }
            else if (lockOnScript.isLockedOn)
            {
                if (targetLock != null)
                {
                    transform.LookAt(lockOnScript.lookAtTarget);
                }
                else
                {
                    Debug.Log("TargetNotGiven");
                }
            }

            // Movement going Camera Relative
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
                // Apply velocity force

                Vector3 velError = targetVel - playerRb.linearVelocity;
                playerRb.AddForce(velError, ForceMode.Force);
            }

        }
        private void Update()
        {
            horInput = Input.GetAxisRaw("Horizontal");
            verInput = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.F) && CheckforMana(playerStats))
            {
                attackAction.Invoke(playerStats.BaseDamage * playerStats.level);

            }
            if (Input.GetKeyDown(KeyCode.Space) && CanJump)
            {
                Jump();
            }
            if (playerRb.linearVelocity.y > 0)
            {
                CanJump = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && dashCD > 5f)
            {
                dashCD = 0f;
                playerRb.AddForce(inputDir * dashForce, ForceMode.VelocityChange);
            }
            else
            {
                dashCD += Time.deltaTime;
            }
        }

        public void Jump()
        {
            playerRb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Force);
        }


        public bool CheckforMana(PlayerStats stats)
        {
            if (stats != null)
            {
                if(stats.mana.currentVal >= stats.attackManaReq)
                {
                    stats.mana.ChangeValue(-stats.attackManaReq);
                    return true;
                }
                else
                {
                    Debug.Log("NotEnoughMana");
                    return false;
                }

                
            }
            else
            {
                return false;
            }
        }


        public void ParticleHandle()
        {

            if (lockOnIndex >= projectile.Count)
            {
                lockOnIndex = 0;
            }

            projectile[lockOnIndex].transform.position = projectileLocation.position;
            projectile[lockOnIndex].transform.rotation = projectileLocation.rotation;
            projectile[lockOnIndex].SetActive(true);
            lockOnIndex += 1;
        }

    }
}