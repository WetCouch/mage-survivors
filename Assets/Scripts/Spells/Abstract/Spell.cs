using UnityEngine;

public abstract class Spell : MonoBehaviour {
    internal static readonly Vector3 HandOffset = new Vector3(0.75f, -0.25f, 0.25f);
    internal static readonly Vector3 CenterOffset = new Vector3(0, 0, 0.5f);

    public readonly int manaCost;
    public readonly float cooldown;

    internal PlayerController caster;

    public static (Vector3 position, Quaternion rotation) GetTransform(Character caster, Vector3 offset) {
        Transform trasnform = caster.HeadTransform();

        return (trasnform.TransformPoint(Vector3.forward + offset), trasnform.rotation);
    }

    public Spell(
        int manaCost,
        float cooldown
    ) {
        this.manaCost = manaCost;
        this.cooldown = cooldown;
    }

    public abstract void Cast();

    protected virtual void HandleMovement() {
        FollowPlayer();
    }

    protected void FollowPlayer() {
        var (position, rotation) = GetTransform(caster, HandOffset);
        transform.SetPositionAndRotation(position, rotation);
    }

    private void LateUpdate() {
        HandleMovement();
    }
}
