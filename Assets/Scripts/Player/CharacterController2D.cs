using UnityEngine;
using UnityEngine.Serialization;

public class CharacterController2D : MonoBehaviour
{

	public Player.Player Player;
	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
	[SerializeField] private bool airControl = true;
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private Transform groundCheck;

	const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool grounded;            // Whether or not the player is grounded.
	public Rigidbody2D Rigidbody2D;  // For determining which way the player is currently facing.
	
	
	private void FixedUpdate()
	{
		grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				grounded = true;
		}
	}


	public void Move(float move, bool jump)
	{
		if (grounded || airControl)
		{
			Vector3 targetVelocity = new Vector2(move * 10f, Rigidbody2D.velocity.y);
			Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);
		}
		if (grounded && jump)
		{
			grounded = false;
			Rigidbody2D.AddForce(new Vector2(0f, Player.JumpForce));
		}

		if (grounded && move != 0)
		{
			AudioManager.Instance.RunSoundPlay(true);
		}
		else
		{
			AudioManager.Instance.RunSoundPlay(false);
		}
		
	}
	
	private static Vector3 velocity = Vector3.zero;

}
