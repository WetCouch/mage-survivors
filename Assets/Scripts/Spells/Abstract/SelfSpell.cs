public abstract class SelfSpell : Spell {
    public SelfSpell(int manaCost, float cooldown) : base(manaCost, cooldown) {}

    public override void Cast() {
        SpellEffect(caster);
        Destroy(gameObject);
    }

    protected abstract void SpellEffect(PlayerController player);
}