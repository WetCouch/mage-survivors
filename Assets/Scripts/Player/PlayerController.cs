using UnityEngine;

[RequireComponent(typeof(SpellBook))]
public class PlayerController : Character {
    private SpellBook spellBook ;

    private readonly float sensitivity = 15;
    private (
        Vector3 movement,
        Vector2 rotation,
        bool jump
    ) input = (
        movement: Vector3.zero,
        rotation: Vector2.zero,
        jump: false
    );

    public PlayerController() : base((multiplier: 100, regen: 10), (multiplier: 100, regen: 10)) {} 

    protected override void Awake() {
        base.Awake();
        spellBook = GetComponent<SpellBook>();
    }

    private void Start() {
        stats.mana.OnChange += UIManager.UpdateManaText;
        stats.exp.OnChange += UIManager.UpdateExpText;
        stats.OnLevelChange += UIManager.UpdateLevelText;
        stats.EmitStats();

        StartCoroutine(stats.mana.Regenerate());
    }

    private void FixedUpdate() {
        HandleMovement();
    }

    private void Update() {
        UpdateInput();
        ChangeSpell();
        CastSpell();
    }

    private void UpdateInput() {
        input.movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input.rotation += new Vector2(-Input.GetAxis("Mouse Y") * sensitivity, Input.GetAxis("Mouse X") * sensitivity);
        if (Input.GetKeyDown(KeyCode.Space)) input.jump = true;
    }

    private void HandleMovement() {
        Move(input.movement);
        Rotate(input.rotation);

        if (input.jump) {
            Jump();
            input.jump = false;
        }

        input.rotation = Vector2.zero;
    }

    private void ChangeSpell() {
        for (int spellIndex = 0; spellIndex < spellBook.SpellCount(); spellIndex++) {
            if (Input.GetKeyDown(KeyCode.Alpha1 + spellIndex)) spellBook.ChooseSpell(spellIndex);
        }
    }

    private void CastSpell() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) spellBook.Cast(stats.mana);
    }
}
