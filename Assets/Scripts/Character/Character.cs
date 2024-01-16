using UnityEngine;

public class Character : MonoBehaviour {
    public CharacterStats stats;

    private readonly float movementSpeed;
    private readonly float jumpForce;

    private Rigidbody rbody;
    private Collider coll;
    private CharacterHead characterHead;

    public Character(
        (int multiplier, float regenRate) hp,
        (int multiplier, float regenRate) mana,
        int expMultiplier = 50,
        int jumpForce = 300,
        int movementSpeed = 15
    ) {
        this.movementSpeed = movementSpeed;
        this.jumpForce = jumpForce;

        stats = new CharacterStats(hp, mana, expMultiplier);
    }

    public void ChangeHP(int hp) {
        stats.hp.Change(hp);
    }

    public bool IsDead() {
        return !stats.hp.IsMinimum(1);
    }

    public Transform HeadTransform() {
        return characterHead.transform;
    }

    protected virtual void Awake() {
        coll = GetComponent<Collider>();
        rbody = GetComponent<Rigidbody>();
        characterHead = GetComponentInChildren<CharacterHead>();
    }

    protected void Move(Vector3 direction) {
        rbody.MovePosition(transform.position + transform.TransformDirection(direction * Time.deltaTime * movementSpeed));
    }

    protected void Rotate(Vector2 rotation) {
        transform.Rotate(0, rotation.y, 0);
        characterHead.Rotate(rotation.x);
    }

    protected void Jump() {
        if (IsGrounded()) {
            rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.1f);
    }
}