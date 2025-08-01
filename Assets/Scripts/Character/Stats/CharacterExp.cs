public class CharacterExp : BaseCharacterResource {
    public CharacterExp(int multiplier, int level) : base(multiplier) {
        Upgrade(level);
    }

    public override void Upgrade(int level) {
        max = multiplier * (level * level);
        EmitChange();
    }
}