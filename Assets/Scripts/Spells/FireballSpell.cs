public class FireballSpell : ProjectileSpell {
    private readonly int damage = 100;

    public FireballSpell() : base(50, 0.5f, 5) {}

    protected override void SpellEffect(Enemy enemy) {
        enemy.ChangeHP(-damage);
        DetectDeadEnemy(enemy);
    }
}