using UnityEngine;

public enum SpellState {
    Selected,
    Casted
}

public abstract class Spell : MonoBehaviour {
    // public static readonly Vector3 SpellOffset = new Vector3(1, -0.5f, 1.25f);
    public static readonly Vector3 SpellOffset = new Vector3(0.75f, -0.25f, 0.25f);

    public readonly int manaCost;
    public readonly float cooldown;

    protected SpellState state = SpellState.Selected;

    internal PlayerController caster;

    public Spell(
        int manaCost,
        float cooldown
    ) {
        this.manaCost = manaCost;
        this.cooldown = cooldown;
    }

    public Vector3 GetFollowOffset() {
        return new Vector3(SpellOffset.x * caster.transform.forward.x, SpellOffset.y, SpellOffset.z);
    }

    public abstract void Cast();

    protected void FollowPlayer() {
        if (state == SpellState.Selected) {
            Transform targetTransform = caster.HeadTransform();
            // transform.SetPositionAndRotation(caster.transform.position + GetFollowOffset(), caster.transform.rotation);
            transform.SetPositionAndRotation(targetTransform.TransformPoint(Vector3.forward + SpellOffset), targetTransform.rotation);
        }
    }

    private void LateUpdate() {
        FollowPlayer();
    }
}
