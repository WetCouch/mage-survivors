using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {    
    public readonly int damage;
    public readonly int manaCost;
    public readonly float cooldown;
    private readonly float projectileSpeed;

    internal PlayerController caster;

    // Future Additions
    // private readonly float explosionRadius;
    // private readonly float maxDistance
    // private readonly PlayerController maxDistance 0 for effect on self
    // private void OnExplode() {} detect characters in radius

    public Spell(int damage, int manaCost, float cooldown, float projectileSpeed) {
        this.damage = damage;
        this.manaCost = manaCost;
        this.cooldown = cooldown;
        this.projectileSpeed = projectileSpeed;
    }

    void FixedUpdate() {
        HandleFlight();
    }

    void OnTriggerEnter(Collider other) {
        DetectHit(other);
    }

    private void DetectHit(Collider other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy) {
            enemy.hp.Change(-damage);

            if (!enemy.hp.IsMinimum(1)) {
                Destroy(enemy.gameObject);
                caster.UpdateExp(enemy.expValue);
            }
        }
    }

    private void HandleFlight() {
        transform.Translate(Vector3.forward * Time.deltaTime  * projectileSpeed);

        DestroyOutOfWorld();
    }

    private void DestroyOutOfWorld() {
        if (!GameManager.isInBoundaries(transform.position)) Destroy(gameObject);
    }
}
