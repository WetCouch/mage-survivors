using System;
using System.Linq;
using UnityEngine;

public enum SpellState {
    Rest,
    Ready,
    Fly
}


public abstract class ProjectileSpell : Spell {
    private SpellState state = SpellState.Rest;

    private readonly float spellRadius;
    private readonly float spellSpeed;
    private readonly float spellDistance;

    private readonly float preparationTime = 0.15f;
    private float preparationTimer = 0;
    private Vector3 preparationPosDiff;

    public ProjectileSpell(
        int manaCost,
        float cooldown,
        float spellRadius,
        float spellSpeed = 75,
        float spellDistance = 100
    ) : base(manaCost, cooldown) {
        this.spellRadius = spellRadius;
        this.spellSpeed = spellSpeed;
        this.spellDistance = spellDistance;
    }

    public override void Cast() {
        preparationPosDiff = GetTransform(caster, CenterOffset).position - transform.position;
        state = SpellState.Ready;
    }

    protected abstract void SpellEffect(Enemy enemy);

    protected void DetectDeadEnemy(Enemy enemy) {
        if (!enemy.hp.IsMinimum(1)) {
            Destroy(enemy.gameObject);
            caster.UpdateExp(enemy.expValue);
        }
    }

    protected override void HandleMovement() {
        switch (state) {
            case SpellState.Rest:
                FollowPlayer();
                break;
            case SpellState.Ready:
                Prepare();
                break;
            case SpellState.Fly:
                FlyForward();
                break;
        }
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

    private void FlyForward() {
        transform.Translate(Vector3.forward * Time.deltaTime  * spellSpeed);
        DestroyOutOfWorld();
    }

    private void Prepare() {
        // On each frame transform position by ratio of time between frames to whole animation time
        if (preparationTimer < preparationTime) {
            preparationTimer += Time.deltaTime;
            float transformPercent = Time.deltaTime / preparationTime;
            transform.position = transform.position + (preparationPosDiff * transformPercent);
        } else {
            state = SpellState.Fly;
        }

    }

    private void DestroyOutOfWorld() {
        if (!GameManager.isInBoundaries(transform.position)) Destroy(gameObject);
    }
}