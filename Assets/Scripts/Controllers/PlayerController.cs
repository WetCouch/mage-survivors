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
    private readonly int expMultiplier = 50;
    private readonly int manaMultiplier = 100;
    private readonly int manaRegen = 10;

    [SerializeField] GameObject spellPrefab;
    [SerializeField] TextMeshProUGUI manaText;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI levelText;

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
        UpdateExp(0);
        StartCoroutine(RegenerateMana());
    }

    void FixedUpdate() {
        HandleMovement();
    }

    void Update() {
        CastSpell();
    }

    void LateUpdate() {
        UpdateManaText();
    }

    private void HandleMovement() {
        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * movementSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * movementSpeed);
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
}
