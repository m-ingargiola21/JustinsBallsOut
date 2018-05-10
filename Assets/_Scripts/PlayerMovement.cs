using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public int playerNumber = 1;
    public int localID = 1;
    public float speed = 10f;
    public float jumpControlReduction = 4f;

    float jumpThrust = 1.5f;
    string moveHorizontal;
    string moveVertical;
    string jumpInput;
    string fireInput;
    Rigidbody rigidBody;
    float horizontalInputValue;
    float verticalInputValue;
    float originalPitch;
    float distToGround;
    bool hasPowerUp;
    public bool HasPowerUp { get { return hasPowerUp; } }

    private GameObject jumpButton;
    private GameObject mobileJoystick;
    private GameObject fireButton;

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }


    void OnEnable()
    {
        rigidBody.isKinematic = false;
        horizontalInputValue = 0f;
        verticalInputValue = 0f;
    }


    void OnDisable()
    {
        rigidBody.isKinematic = true;
    }
    
    void Start()
    {
        if(!isLocalPlayer)
        {
            Destroy(this);
            return;
        }

        distToGround = GetComponent<Collider>().bounds.extents.y;

        InputSetup();
    }

    void InputSetup()
    {
        moveVertical = "Vertical";
        moveHorizontal = "Horizontal";
        jumpInput = "Jump";
        fireInput = "Fire";

        //GameManager.instance.PlayerControls[playerNumber - 1].SetActive(true);

    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (CrossPlatformInputManager.GetButtonDown(jumpInput) && IsGrounded())
        {
            Jump();
        }

        horizontalInputValue = CrossPlatformInputManager.GetAxis(moveHorizontal);
        verticalInputValue = CrossPlatformInputManager.GetAxis(moveVertical);
        
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        // Move and turn the player.
        Move();
    }

    void Move()
    {
        // Adjust the position of the tank based on the player's input.
        Vector3 movement = new Vector3(horizontalInputValue, 0.0f, verticalInputValue);

        if(IsGrounded())
        {
            rigidBody.AddForce(movement * speed);
        }
        else
        {
            rigidBody.AddForce(movement * (speed / jumpControlReduction));
        }
        
    }

    void Jump()
    {
        this.rigidBody.AddForce(0, jumpThrust * speed, 0, ForceMode.Impulse);
    }

    public void SetDefaults()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        horizontalInputValue = 0f;
        verticalInputValue = 0f;
    }

    public void GetPowerUp(PowerUp.PowerUpTypes powerUpType)
    {
        switch(powerUpType)
        {
            case PowerUp.PowerUpTypes.OilSlick:
                //OilSlick setup here
                break;
            case PowerUp.PowerUpTypes.AirControl:
                //AirControl setup here
                break;
            case PowerUp.PowerUpTypes.MetalBall:
                //MetalBall setup here
                break;
            case PowerUp.PowerUpTypes.SuperJump:
                //SuperJump setup here
                break;
            default:
                Debug.Log("Got wrong PowerUpType!");
                break;
        }

        hasPowerUp = true;
    }
}
