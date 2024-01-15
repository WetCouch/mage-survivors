using System;
using System.Linq;
using UnityEngine;

public abstract class ProjectileSpell : Spell {

    private readonly float spellRadius;
    private readonly float spellSpeed;
    private readonly float spellDistance;

    public ProjectileSpell(
        int manaCost,
        float cooldown,
        float spellRadius,
        float spellSpeed = 30,
        float spellDistance = 100
    ) : base(manaCost, cooldown) {
        this.spellRadius = spellRadius;
        this.spellSpeed = spellSpeed;
        this.spellDistance = spellDistance;
    }

    public override void Cast() {
        state = SpellState.Casted;
    }

    protected abstract void SpellEffect(Enemy enemy);

    protected void DetectDeadEnemy(Enemy enemy) {
        if (!enemy.hp.IsMinimum(1)) {
            Destroy(enemy.gameObject);
            caster.UpdateExp(enemy.expValue);
        }
    }

    private void FixedUpdate() {
        HandleFlight();
    }

    private void OnTriggerEnter(Collider other) {
        EffectEnemies(DetectEnemies());
        Destroy(gameObject);
    }

    private void EffectEnemies(Enemy[] enemies) {
        foreach (Enemy enemy in enemies) {
            SpellEffect(enemy);
        }
    }

    private Enemy[] DetectEnemies() {
        if (spellRadius == 0) return Array.Empty<Enemy>();

        return Physics
            .OverlapSphere(transform.position, spellRadius)
            .Select(collider => collider.gameObject.GetComponent<Enemy>())
            .Where(component => component != null)
            .ToArray();
    }

    private void HandleFlight() {
        if (state == SpellState.Casted) {
            transform.Translate(Vector3.forward * Time.deltaTime  * spellSpeed);
            DestroyOutOfWorld();
        }
    }

    private void DestroyOutOfWorld() {
        if (!GameManager.isInBoundaries(transform.position)) Destroy(gameObject);
    }
}