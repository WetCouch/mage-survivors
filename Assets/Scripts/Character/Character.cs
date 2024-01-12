using UnityEngine;

public class Character : MonoBehaviour {
    internal CharacterResource mana;
    internal CharacterResource hp;

    private readonly float movementSpeed;
    private readonly float jumpForce;

    private Rigidbody rbody;
    private Collider coll;

    public Character(
        (int multiplier, int regen) hp,
        (int multiplier, int regen) mana,
        int jumpForce = 300,
        int movementSpeed = 15
    ) {
        this.movementSpeed = movementSpeed;
        this.jumpForce = jumpForce;

        this.mana = new CharacterResource(mana.multiplier, mana.regen);
        this.hp = new CharacterResource(hp.multiplier, hp.regen);
    }

    protected virtual void Start() {
        coll = GetComponent<Collider>();
        rbody = GetComponent<Rigidbody>();
    }

    protected void Move(Vector3 direction) {
        rbody.MovePosition(transform.position + transform.TransformDirection(direction * Time.deltaTime * movementSpeed));
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