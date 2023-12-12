using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f; 
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    //Variables for game over
    public float interactionRadius = 1.5f;
    public LayerMask treeHouseLayer;
    public GameObject treeFoundText;
    public GameObject restartText;
    private bool treeHouseFound = false;
    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        if (!treeHouseFound)
        {
            Collider2D treeHouseCollider = Physics2D.OverlapCircle(transform.position, interactionRadius, treeHouseLayer);

            if (treeHouseCollider != null)
            {
                // Enable the UI TextMeshPro object
                treeFoundText.SetActive(true);
                restartText.SetActive(true);
                treeHouseFound = true;
            }

            //if tree house is not found, allow player movement

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;

            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }
        else
        { // if tree house is found, set animation to idle
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsCrouching", false);
            animator.SetFloat("Speed", 0f);
            animator.Play("Player_idle");
            horizontalMove = 0f;

            // Check for Space key press to restart the game
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Restart the game
                gameManager.RestartGame();
            }
        }

    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }


    public void OnCrouching (bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
 