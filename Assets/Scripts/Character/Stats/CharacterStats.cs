using System;
using UnityEngine;

public class CharacterStats {
    public int level;

    public event Action<int> OnLevelChange;

    internal CharacterExp exp;
    internal CharacterResource mana;
    internal CharacterResource hp;

    public CharacterStats(
        (int multiplier, float regenRate) hp,
        (int multiplier, float regenRate) mana,
        int expMultiplier,
        int level = 1
    ) {
        this.level = level;

        this.mana = new CharacterResource(mana.multiplier, mana.regenRate, level);
        this.hp = new CharacterResource(hp.multiplier, hp.regenRate, level);
        this.exp = new CharacterExp(expMultiplier, level);
    }

    public void ChangeExp(int value) {
        exp.Change(value);

        if (exp.IsMax()) {
            level++;

            exp.Upgrade(level);
            mana.Upgrade(level);
            hp.Upgrade(level);

            EmitChange();
        }
    }

    public void EmitStats() {
        EmitChange();
        hp.EmitChange();
        mana.EmitChange();
        exp.EmitChange();
    }

    private void EmitChange() {
        OnLevelChange?.Invoke(level);
    }
}