using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {    
    public readonly int damage = 50;
    public readonly int manaCost = 25;
    public readonly float cooldown = 2;

    private readonly float projectileSpeed = 30;

    void FixedUpdate() {
        HandleFlight();
    }

    private void HandleFlight() {
        transform.Translate(Vector3.forward * Time.deltaTime  * projectileSpeed);

        DestroyOutOfWorld();
    }

    private void DestroyOutOfWorld() {
        if (!GameManager.isInBoundaries(transform.position)) Destroy(gameObject);
    }
}
