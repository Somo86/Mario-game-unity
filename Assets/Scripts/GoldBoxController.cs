using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBoxController : MonoBehaviour
{
    public Transform ContainedItem;

    private bool isActive = true;
    private Animator animator;

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    private void setInactiveState()
    {
        isActive = false;
        animator.runtimeAnimatorController = null;
    }
    
    // Mario collides the base
    private void OnTriggerEnter2D(Collider2D other)
    {
        var isMarioHead = other.tag == "MarioHead" || other.tag == "SuperMarioHead";
        if(isMarioHead && isActive)
        {
            var position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            Instantiate(ContainedItem, position, transform.rotation);
            setInactiveState();
        }
    }
}
