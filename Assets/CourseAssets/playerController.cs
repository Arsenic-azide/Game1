using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float laneSpeed = 15f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public float laneDistance = 4f; 
    
    private CharacterController controller;
    private Animator animator;
    private Vector3 moveDirection = Vector3.zero;
    private int currentLane = 1; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        handleInput();
        movePlayer();
        checkFall();
    }

    void handleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentLane--;
            if (currentLane < 0) currentLane = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentLane++;
            if (currentLane > 2) currentLane = 2;
        }
        
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            moveDirection.y = jumpSpeed;
            animator.SetTrigger("jumpTrigger");
        }
    }

    void movePlayer()
    {
        Vector3 targetPosition = transform.position;
        
        if (currentLane == 0) targetPosition.x = -laneDistance;
        else if (currentLane == 1) targetPosition.x = 0;
        else if (currentLane == 2) targetPosition.x = laneDistance;

        float moveX = Mathf.Lerp(transform.position.x, targetPosition.x, laneSpeed * Time.deltaTime) - transform.position.x;
        
        moveDirection.x = moveX / Time.deltaTime;
        moveDirection.z = forwardSpeed;
        
        if (!controller.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    void checkFall()
    {
        if (transform.position.y < -10f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}