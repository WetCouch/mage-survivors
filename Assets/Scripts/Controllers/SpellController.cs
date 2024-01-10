using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {    
    public readonly int damage = 50;

    private  readonly float flyingSpeed = 30;

    void FixedUpdate() {
        HandleFlight();
    }

    private void HandleFlight() {
        transform.Translate(Vector3.forward * Time.deltaTime  * flyingSpeed);

        DestroyOutOfWorld();
    }

    private void DestroyOutOfWorld() {
        if (!GameManager.isInBoundaries(transform.position)) Destroy(gameObject);
    }
}
