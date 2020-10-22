using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public Transform Coin;
    IEnumerator KillObject()
    {
        yield return new WaitForSeconds(0.004f);
        Destroy(gameObject);
    }

    // Mario collides the base
    private void OnTriggerEnter2D(Collider2D other)
    {
        var isSuperMarioHead = other.tag == "SuperMarioHead";
        if(isSuperMarioHead)
        {
            CounterManager.addScore(10);
            var position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            Instantiate(Coin, position, transform.rotation);
            StartCoroutine(KillObject());
        }
    }
}
