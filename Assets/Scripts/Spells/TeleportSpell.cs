using UnityEngine;

public class TeleportSpell : SelfSpell {
    private readonly float distance = 25;

    public TeleportSpell() : base(25, 5) {}

    protected override void SpellEffect(Character caster) {
        caster.transform.Translate(Vector3.forward * distance);
    }
}