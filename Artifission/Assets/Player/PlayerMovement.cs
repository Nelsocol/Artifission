using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Retrieved Components
    private Rigidbody2D thisRigidbody;
    private BoxCollider2D thisCollider;
    private Transform thisTransform;
    private PlayerStatBindings playerBindings;
    #endregion

    #region Exposed Members
    public float walkSpeed;
    public float jumpHeight;
    public LayerMask layerMask;
    #endregion

    #region Private Members
    public float mJumpBoost;
    private float mInitialJumpModifier = 1;
    private float mJumpBoostModifier = 3;
    private float mBoostDecayModifier = 2;
    private float mGroundSpeedMultiplier = 1.5f;
    private bool jumpRequested;
    #endregion

    public void Start()
    {
        playerBindings = GetComponent<PlayerStatBindings>();
        thisRigidbody = GetComponent<Rigidbody2D>();
        thisCollider = GetComponent<BoxCollider2D>();
        thisTransform = GetComponent<Transform>();
    }

    public void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && playerBindings.inMenus == false)
        {
            jumpRequested = true;
        }
    }

    public void FixedUpdate()
    {
        if (playerBindings.inMenus == false && !playerBindings.stunned)
        {
            ApplyHorizontalMotion();

            if (IsGrounded())
            {
                EvaluateForJump();
            }
            else
            {
                AirbornBehavior();
            }
        }
    }

    private void ApplyHorizontalMotion()
    {
        if(Keyboard.current.dKey.IsPressed())
        {
            thisRigidbody.AddForce(Vector2.right * walkSpeed * mGroundSpeedMultiplier);
        }

        if(Keyboard.current.aKey.IsPressed())
        {
            thisRigidbody.AddForce(Vector2.left * walkSpeed * mGroundSpeedMultiplier);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(thisRigidbody.position + thisCollider.offset, Vector2.down, ((thisCollider.size.y * thisTransform.localScale.y) / 1.8f), ~layerMask);
        RaycastHit2D leftHit = Physics2D.Raycast(thisRigidbody.position + thisCollider.offset - new Vector2((thisCollider.size.x * thisTransform.localScale.x) / 2, 0), Vector2.down, ((thisCollider.size.y * thisTransform.localScale.y) / 1.8f));
        RaycastHit2D rightHit = Physics2D.Raycast(thisRigidbody.position + thisCollider.offset + new Vector2((thisCollider.size.x * thisTransform.localScale.x) / 2, 0), Vector2.down, ((thisCollider.size.y * thisTransform.localScale.y) / 1.8f));
        return (hit && hit.collider.gameObject.tag == "Ground") || (leftHit && leftHit.collider.gameObject.tag == "Ground") || (rightHit && rightHit.collider.gameObject.tag == "Ground");
    }

    private void EvaluateForJump()
    {
        mGroundSpeedMultiplier = 1.5f;
        if (!Keyboard.current.spaceKey.IsPressed())
        {
            mJumpBoost = jumpHeight;
        }
        if(jumpRequested)
        {
            thisRigidbody.AddForce(Vector2.up * jumpHeight * mInitialJumpModifier, ForceMode2D.Impulse);
            jumpRequested = false;
        }
    }

    private void AirbornBehavior()
    {
        jumpRequested = false;
        mGroundSpeedMultiplier = 1;
        if(Keyboard.current.spaceKey.IsPressed() && mJumpBoost > 0)
        {
            mJumpBoost -= jumpHeight * Time.fixedDeltaTime * mBoostDecayModifier;
            thisRigidbody.AddForce(Vector2.up * jumpHeight * mJumpBoostModifier);
        }
        else
        {
            mJumpBoost = 0;
        }
    }    
}
