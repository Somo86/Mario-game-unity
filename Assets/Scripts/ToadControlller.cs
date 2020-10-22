using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadControlller : MonoBehaviour
{
    public float LinearSpeed = 1;
    public RuntimeAnimatorController killAnimator;
    public Transform HeadToad;
    
    private AudioClip clipDead;

    private bool Forward = false;
    private bool isAlive = true;
    private bool playedBefore = false;

    private Collider2D collider;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audio;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        clipDead = Resources.Load<AudioClip>("Sounds/smb_bump");
    }

    void Update()
    {
        if(isAlive)
            Move();
        else
            Stop();

        // Check is Mario attack Toad from head
        // If hes does, kill Toad.
        if (isSmashed()){
            if(!playedBefore)
                audio.PlayOneShot(clipDead);
            playedBefore = true;
            Kill();
        }
    }

    private void Move() {
        if(Forward)
            MoveForward();
        else
            MoveBackward();
    }

    private void Kill() {
        if(isAlive)
            CounterManager.addScore(30);
        isAlive = false;
        StartCoroutine(KillToad());
    }

    IEnumerator KillToad(){  
        // change animation to kill animation      
        animator.runtimeAnimatorController = killAnimator;
        transform.position = new Vector2(transform.position.x, -2.8f);
        // Avoid kill Mario
        Destroy(collider);

        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void ChangeDirection() {
        Forward = !Forward;
    }

    private void MoveForward()
    {
        rb.velocity = new Vector2(LinearSpeed, 0);
    }

    private void MoveBackward()
    {
        rb.velocity = new Vector2(-LinearSpeed, 0);
    }

    private void Stop()
    {
        rb.velocity = new Vector2(0, 0);
    }

    // Someone collides Head Toad Area
    private bool isSmashed()
    {
        return Physics2D.OverlapCircleAll(HeadToad.position, 0.3f).Length > 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var isToad = other.gameObject.CompareTag("Toad");
        var isGround = other.gameObject.CompareTag("Ground");
       
        if (!isToad && !isGround)
            ChangeDirection();
    }
}
