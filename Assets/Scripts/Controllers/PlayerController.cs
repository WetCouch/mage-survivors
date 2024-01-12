using System.Collections;
using UnityEngine;

public class PlayerController : Character {
    private bool spellOnCooldown = false;

    private int level = 0;
    private int exp = 0;
    private int nextLevelExp = 0;

    public PlayerController() : base((multiplier: 100, regen: 10), (multiplier: 100, regen: 10)) {}

    private readonly int expMultiplier = 50;

    [SerializeField] GameObject spellPrefab;

    private (Vector3 movement, bool jump) input = (movement: Vector3.zero, jump: false);

    public void UpdateExp(int newExp) {
        exp += newExp;
        
        if (exp >= nextLevelExp) {
            level++;
            nextLevelExp = expMultiplier * level * level;
            mana.Upgrade(level);
        };

        UIManager.UpdateExpText(exp, nextLevelExp, level);
    }

    protected override void Start() {
        base.Start();
        mana.OnChange += UIManager.UpdateManaText;

        UpdateExp(0);
        StartCoroutine(mana.Regenerate());
    }

    private void FixedUpdate() {
        HandleMovement();
    }

    private void Update() {
        UpdateInput();
        CastSpell();
    }

    private void UpdateInput() {
        input.movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space)) input.jump = true;
    }

    private void HandleMovement() {
        Move(input.movement);

        if (input.jump) {
            Jump();
            input.jump = false;
        }
    }

    private void CastSpell() {
        Spell spell = spellPrefab.GetComponent<Spell>();

        if (Input.GetKeyDown(KeyCode.Mouse0) && mana.IsMinimum(spell.manaCost) && !spellOnCooldown) {
            mana.Change(-spell.manaCost);
            Instantiate(spellPrefab, transform.position, transform.rotation).GetComponent<Spell>().caster = this;
            StartCoroutine(SpellCooldown(spell));
        }
    }

    private IEnumerator SpellCooldown(Spell spell) {
        spellOnCooldown = true;
        yield return new WaitForSeconds(spell.cooldown);
        spellOnCooldown = false;
    }
}
