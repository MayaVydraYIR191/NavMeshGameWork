using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DragonController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private float playerSpeed = 5f;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float gravityValue = -9.81f;
    
    private Animator anime;

    public int playerHealth;
    public GameObject RestartButton;
    public Image bar;
    public float fill;
    public GameObject shot;
    public Transform shotPoint;
    [SerializeField] private GameObject Finishrestart;
    public Camera cam;
    public NavMeshAgent nav;
    

    private void Start()
    {
        anime = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        fill = 1f;
        playerHealth = 5;
        controller.enabled = true;
        RestartButton.SetActive(false);
    }

    private void Update()
    {
        //Movement();
        if (Input.GetMouseButtonDown(0))
        {
            anime.SetBool("isWalking",true);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                nav.SetDestination(hit.point);
            }
        }

        if (playerHealth <= 0)
        {
            anime.SetBool("isDead", true);
            controller.enabled = false;
            RestartButton.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            anime.SetBool("isAttacked",true);
            Instantiate(shot, shotPoint.position, shotPoint.rotation);
            anime.SetBool("isAttacked",false);
        }

    }

    void Movement()
    {
        jumpHeight = 10f;
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        float horizonInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0,transform.eulerAngles.y,0) * new Vector3(horizonInput,0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        controller.Move(movementDirection * playerSpeed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotate = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotate, rotationSpeed * Time.deltaTime);
            anime.SetBool("isWalking", true);
        }
        else
        {
            anime.SetBool("isWalking", false);
        }
             

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            anime.SetBool("isJumping", true);
        }
        else
        {
            anime.SetBool("isJumping", false);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Danger"))
        {
            fill -= 0.2f;
            bar.fillAmount = fill;
            anime.SetBool("isAttacked", true);
            playerHealth = (playerHealth - 1);
        }

        if (other.CompareTag("Finish"))
        {
            Finishrestart.SetActive(true);
        }
    }
}
