public class FireballSpell : ProjectileSpell {
    private readonly int damage = 75;

    public FireballSpell() : base(50, 0.5f, 5) {}

    protected override void SpellEffect(Enemy enemy) {
        enemy.hp.Change(-damage);
        DetectDeadEnemy(enemy);
    }
}