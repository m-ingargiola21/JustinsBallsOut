using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public int playerNumber = 1;
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
        distToGround = GetComponent<Collider>().bounds.extents.y;
        moveVertical = "Vertical" + playerNumber;
        moveHorizontal = "Horizontal" + playerNumber;
        jumpInput = "Jump" + playerNumber;
        fireInput = "Fire" + playerNumber;
        
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown(jumpInput) && IsGrounded())
        {
            Jump();
        }

        horizontalInputValue = CrossPlatformInputManager.GetAxis(moveHorizontal);
        verticalInputValue = CrossPlatformInputManager.GetAxis(moveVertical);
        
    }

    void FixedUpdate()
    {
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
