using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    public float LinearSpeed = 1;
    public float jumpForce = 1;
    public int SuperDuration = 30;
    public float LimitVelocity = 2;

    //******* MARIO SPRITES ***********//
    public RuntimeAnimatorController WalkLeftAnimator;
    public RuntimeAnimatorController WalkRightAnimator;
    public Sprite MainSprite;
    public Sprite JumpLeftSprite;
    public Sprite JumpRightSprite;

    //******* DELEGATED ACTIONS *******//
    public Action OnKilled;

    //******* SUPER MARIO SPRITES ***********//
    public RuntimeAnimatorController WalkLeftAnimatorSuper;
    public RuntimeAnimatorController WalkRightAnimatorSuper;
    public Sprite MainSpriteSuper;
    public Sprite JumpLeftSpriteSuper;
    public Sprite JumpRightSpriteSuper;

    //******* SOUNDS ******************//
    private AudioClip clipJump;
    private AudioClip clipPowerUp;
    private AudioClip clipPowerDown;

    //*********************************//
    private bool isGround = true;
    private bool isSuperMario = false;
    public bool allowMovement = true;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Transform MarioBase;
    private BoxCollider2D MarioBaseCollider;
    private BoxCollider2D MarioHeadCollider;
    private AudioSource audio;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        audio = GetComponent<AudioSource>();
        MarioBase = gameObject.transform.GetChild(0).GetComponent<Transform>();
        MarioBaseCollider = gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        MarioHeadCollider = gameObject.transform.GetChild(1).GetComponent<BoxCollider2D>();

        clipJump = Resources.Load<AudioClip>("Sounds/smb_jump-small");
        clipPowerUp = Resources.Load<AudioClip>("Sounds/smb_powerup");
        clipPowerDown = Resources.Load<AudioClip>("Sounds/smb_power_down");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && allowMovement)
        {
            if(isGround){
                MoveBackward();
                if (Input.GetKey(KeyCode.Space) && allowMovement)
                    Jump(Direction.Left);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) && allowMovement)
        {
            if(isGround){
                MoveForward();
                if (Input.GetKey(KeyCode.Space) && allowMovement)
                    Jump(Direction.Right);
            }
        }
        else if (Input.GetKey(KeyCode.Space) && allowMovement)
        {
            if(isGround)
                Jump(Direction.Right);
        }
        else {
            if(isGround) {
                rb.velocity += Vector2.down  * (Time.deltaTime * 4); // reduce velocity
                StaticAnimation();
            }
        }
    }

    //************* Mario States ****************//
    //*******************************************//

    IEnumerator initSuperMario()
    {
        isSuperMario = true;
        audio.PlayOneShot(clipPowerUp);
        // Mario BoxCollider form
        boxCollider.size = new Vector2(0.974515f, 1.98367f);
        boxCollider.offset = new Vector2(-0.003621101f, -0.006592751f);
        // Mario ground detector BoxCollider form
        MarioBaseCollider.size = new Vector2(0.985539f, 0.09100509f);
        MarioBaseCollider.offset = new Vector2(-0.01596999f, -0.06708586f);
        MarioBase.position = new Vector2(MarioBase.position.x, MarioBase.position.y + -0.5f);
        // Mario head detector BoxCollider form
        MarioHeadCollider.size = new Vector2(0.730226f, 0.2793646f);
        MarioHeadCollider.offset = new Vector2(0.01225305f, 0.88311f);
        // Change HeadTagName
        ChangeHeadTagName("SuperMarioHead");

        yield return new WaitForSeconds(SuperDuration); // Wait until loose powers
        if(isSuperMario)
            initStandardMario(); // -> Back to regular Mario :`(
    }

    private void initStandardMario()
    {
        isSuperMario = false;
        audio.PlayOneShot(clipPowerDown);
        // Mario BoxCollider form
        boxCollider.size = new Vector2(0.75f, 1f);
        boxCollider.offset = new Vector2(0f, 0f);
        // Mario ground detector BoxCollider form
        MarioBaseCollider.size = new Vector2(0.7311435f, 0.07187986f);
        MarioBaseCollider.offset = new Vector2(-0.002207518f, -0.07664847f);
        MarioBase.position = new Vector2(MarioBase.position.x, MarioBase.position.y + 0.5f);
        // Mario head detector BoxCollider form
        MarioHeadCollider.size = new Vector2(0.6403713f, 0.2507744f);
        MarioHeadCollider.offset = new Vector2(0, 0.3746128f);
        // Change HeadTagName
        ChangeHeadTagName("MarioHead");
    }

    //************* Movements *****************//
    //*****************************************//

    private void Jump(Direction direction)
    {
        isGround = false;
        if(direction == Direction.Left)
            JumpLeftAnimation();
        else if(direction == Direction.Right)
            JumpRightAnimation();

        rb.velocity += (Vector2.up * jumpForce);
        audio.PlayOneShot(clipJump);
    }

    private void MoveForward()
    {
        WalkRightAnimation();
        var right = transform.right;
        if(rb.velocity.x < LimitVelocity)
            rb.velocity += new Vector2(right.x * LinearSpeed, right.y * LinearSpeed) * Time.deltaTime;
    }

    private void MoveBackward()
    {
        WalkLeftAnimation();
        var right = transform.right;
        if(rb.velocity.x > -LimitVelocity)
            rb.velocity -= new Vector2(right.x * LinearSpeed, right.y * LinearSpeed) * Time.deltaTime;
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircleAll(MarioBase.position, 0.3f).Length > 1;
    }

    //********** Collision Trigger ************//
    //*****************************************//
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject != gameObject)
            isGround = isGrounded();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        var isMushroomItem = other.gameObject.CompareTag("MushroomItem");
        var isToad = other.gameObject.CompareTag("Toad");
        if(isMushroomItem)
            StartCoroutine(initSuperMario());
        else if(isToad)
            if(isSuperMario)
            {
                initStandardMario(); // Back to standart Mario
            } else {
                OnKilled.Invoke(); // Mario dies! Toad collision!
            }
    }

    // *********** Animations and Sprites renderer ******************//
    // **************************************************************//
    private void WalkLeftAnimation()
    {
        var walk = isSuperMario ? WalkLeftAnimatorSuper : WalkLeftAnimator;
        animationRender(walk);
    }

    private void WalkRightAnimation()
    {
        var walk = isSuperMario ? WalkRightAnimatorSuper : WalkRightAnimator;
        animationRender(walk);
    }

    private void JumpLeftAnimation()
    {
        var jumpLeft = isSuperMario ? JumpLeftSpriteSuper : JumpLeftSprite;
        spriteRender(jumpLeft);
    }

    private void JumpRightAnimation()
    {
        var jumpRight = isSuperMario ? JumpRightSpriteSuper : JumpRightSprite;
        spriteRender(jumpRight);
    }

    private void StaticAnimation()
    {
        var main = isSuperMario ? MainSpriteSuper : MainSprite;
        spriteRender(main);
    }

    private void spriteRender(Sprite newSprite)
    {
        animator.runtimeAnimatorController = null;
        spriteRenderer.sprite = newSprite;
    }

    private void animationRender(RuntimeAnimatorController newAnimation)
    {
        animator.runtimeAnimatorController = newAnimation;
    }

    private void ChangeHeadTagName(string name)
    {
        var MarioHead = gameObject.transform.GetChild(1).GetComponent<Transform>();
        MarioHead.gameObject.tag = name;
    }
}

enum Direction 
{
  Left,
  Right
}
