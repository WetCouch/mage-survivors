using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private bool spellOnCooldown = false;

    private int mana = 0;
    private int maxMana = 0;
    private int level = 0;
    private int exp = 0;
    private int nextLevelExp = 0;

    private readonly float movementSpeed = 15;
    private readonly float jumpForce = 300;
    private readonly int expMultiplier = 50;
    private readonly int manaMultiplier = 100;
    private readonly int manaRegen = 10;

    [SerializeField] GameObject spellPrefab;
    [SerializeField] TextMeshProUGUI manaText;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI levelText;

    private (Vector3 movement, bool jump) input = (movement: Vector3.zero, jump: false);

    private Collider playerCollider;
    private Rigidbody playerRb;

    public void UpdateExp(int newExp) {
        exp += newExp;
        
        if (exp >= nextLevelExp) {
            level++;
            nextLevelExp = expMultiplier * level * level;
            maxMana = manaMultiplier * level;
            mana = maxMana;
        };

        UpdateExpText();
    }

    void Start() {
        playerCollider = GetComponent<Collider>();
        playerRb = GetComponent<Rigidbody>();

        UpdateExp(0);
        StartCoroutine(RegenerateMana());
    }

    void FixedUpdate() {
        HandleMovement();
    }

    void Update() {
        UpdateInput();
        CastSpell();
    }

    void LateUpdate() {
        UpdateManaText();
    }

    private void UpdateInput() {
        input.movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space)) input.jump = true;
    }

    private void HandleMovement() {
        playerRb.MovePosition(transform.position + transform.TransformDirection(input.movement * Time.deltaTime * movementSpeed));

        if (input.jump && IsGrounded()) {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            input.jump = false;
        }
    }

    private void CastSpell() {
        SpellController spell = spellPrefab.GetComponent<SpellController>();

        if (Input.GetKeyDown(KeyCode.Mouse0) && mana >= spell.manaCost && !spellOnCooldown) {
            mana -= spell.manaCost;
            Instantiate(spellPrefab, transform.position, transform.rotation);
            StartCoroutine(SpellCooldown(spell));
        }
    }

    private IEnumerator SpellCooldown(SpellController spell) {
        spellOnCooldown = true;
        yield return new WaitForSeconds(spell.cooldown);
        spellOnCooldown = false;
    }
 
    private IEnumerator RegenerateMana() {
        while (true) {
            mana += manaRegen;
            if (mana > maxMana) mana = maxMana;
            yield return new WaitForSeconds(1);

            UpdateManaText();
        }
    }

    private void UpdateExpText() {
        expText.text = $"Exp: { exp } / { nextLevelExp }";
        levelText.text = $"Level: { level }";
    }

    private void UpdateManaText() {
        manaText.text = $"Mana: { mana } / { maxMana }";
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, playerCollider.bounds.extents.y + 0.1f);
    }
}
