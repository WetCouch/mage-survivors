using UnityEngine;

public abstract class Spell : MonoBehaviour {    
    public readonly int manaCost;
    public readonly float cooldown;

    internal PlayerController caster;

    public Spell(
        int manaCost,
        float cooldown
    ) {
        this.manaCost = manaCost;
        this.cooldown = cooldown;
    }

    public abstract void Cast();
}
