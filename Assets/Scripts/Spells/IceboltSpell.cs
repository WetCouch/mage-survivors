public class IceboltSpell : ProjectileSpell {
    private readonly int damage = 50;

    public IceboltSpell() : base(25, 0.5f, 0.5f) {}

    protected override void SpellEffect(Enemy enemy) {
        enemy.hp.Change(-damage);
        DetectDeadEnemy(enemy);
    }
}