using UnityEngine;

public class TeleportSpell : SelfSpell {
    private readonly float distance = 25;

    public TeleportSpell() : base(25, 5) {}

    protected override void SpellEffect(PlayerController player) {
        player.transform.Translate(Vector3.forward * distance);
    }
}