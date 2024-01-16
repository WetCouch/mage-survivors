using System.Collections;
using UnityEngine;

public class CharacterResource : BaseCharacterResource {
    private int regen;
    private readonly float regenRate;

    public CharacterResource(int multiplier, float regenRate, int level) : base(multiplier) {
        this.regenRate = regenRate;
        Upgrade(level);
    }

    public override void Upgrade(int level) {
        max = multiplier * level;
        regen = (int)(multiplier * level * regenRate);
        current = max;
        EmitChange();
    }

    public IEnumerator Regenerate() {
        while (regen > 0) {
            current += regen;
            if (current > max) current = max;
            EmitChange();
            yield return new WaitForSeconds(1);
        }
    }
}
