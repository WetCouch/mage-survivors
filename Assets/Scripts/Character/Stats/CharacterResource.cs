using System.Collections;
using UnityEngine;

public class CharacterResource : BaseCharacterResource {
    private readonly int regen;

    public CharacterResource(int multiplier, int regen, int level) : base(multiplier, level) {
        this.regen = regen;
    }

    public override void Upgrade(int level) {
        max = multiplier * level;
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