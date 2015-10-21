using UnityEngine;
using System.Collections;


public class PlayerScript : MonoBehaviour
{
	// movement config
    public string CharacterName;
	public float gravity = -25f;
    public float gravitySliding = -10f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;
    public int maxJumps = 1;
    int jumpsLeft = 0;
	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;
    bool airborne;
    bool wallSlide;
	private CharacterController2D _controller;
	private Animator _animator;
	private Vector3 _velocity;
    bool jump;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
	
		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
        
    }

	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
	}


	void onTriggerEnterEvent( Collider2D col )
	{
	}

	void onTriggerExitEvent( Collider2D col )
	{
	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
            // grab our current _velocity to use as a base for all calculations
            _velocity = _controller.velocity;

            if (_controller.isGrounded)
            {
                _velocity.y = 0;
                jumpsLeft = 0;
            }

            if (jump)
            {
                PerformJump();
                jump = false;
                if (wallSlide)
                    wallSlide = false;
            }

            
            // apply horizontal speed smoothing it
            var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
            _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);
           
            if (airborne && _controller.isGrounded)
            {
                airborne = false;
                wallSlide = false;    
            }
            else if (!airborne && !_controller.isGrounded)
            {
                airborne = true;
            }

            // apply gravity before moving
            if(wallSlide)
                _velocity.y += gravitySliding * Time.deltaTime;
            else
            _velocity.y += gravity * Time.deltaTime;
                
            _controller.move(_velocity * Time.deltaTime);

            AlignToGround();

	}

    public void DoJump()
    {
        jump = true;
    }

    void PerformJump()
    {
        if (_controller.isGrounded || (jumpsLeft < maxJumps) || wallSlide)
        {
            _velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            if(!wallSlide)
                jumpsLeft++;
            if (wallSlide)
                normalizedHorizontalSpeed *= -1;

        }
    }

    void AlignToGround()
    {
        RaycastHit hit;
        Quaternion quat = Quaternion.identity;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 20f))
        {
            Debug.DrawRay(transform.position, -transform.up, Color.blue, 0.1f);
            quat = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, quat, Time.deltaTime * 10.0f);
    }

    public void MoveLeft()
    {
        normalizedHorizontalSpeed = -1;

        if (transform.localScale.x < 0f)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void MoveRight()
    {
        normalizedHorizontalSpeed = 1;

        if (transform.localScale.x > 0f)
            transform.localScale = new Vector3(-normalizedHorizontalSpeed, transform.localScale.y, transform.localScale.z);
    }

    public void StayIdle()
    {
        normalizedHorizontalSpeed = 0;
    }

}
