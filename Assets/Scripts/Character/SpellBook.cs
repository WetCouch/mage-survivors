using System.Linq;
using UnityEngine;

internal class SpellIncantation {
    public readonly GameObject prefab;

    private float cooldownEnd = 0;

    internal SpellIncantation(GameObject prefab) {
        this.prefab = prefab;
    }

    public bool CanCast() {
        return Time.time >= cooldownEnd;
    }

    public void SetCooldown(float cooldown) {
        cooldownEnd = Time.time + cooldown;
    }
}

[RequireComponent(typeof(Character))]
internal class SpellBook : MonoBehaviour {
    [SerializeField]
    private GameObject[] spellPrefabs;
    
    private SpellIncantation[] spellContainer;
    private SpellIncantation selectedSpell;
    private GameObject activeSpellInstance;

    private Character caster;

    public int SpellCount() {
        return spellContainer.Length;
    }

    public void ChooseSpell(int index) {
        if (activeSpellInstance != null) {
           Destroy(activeSpellInstance);
        }

        selectedSpell = spellContainer[index];
        NewSpellObject();
    }

    public void Cast(CharacterResource mana) {
        Spell spell = activeSpellInstance.GetComponent<Spell>();

        if (selectedSpell.CanCast() && mana.IsMinimum(spell.manaCost)) {
            mana.Change(-spell.manaCost);
            selectedSpell.SetCooldown(spell.cooldown);
            spell.Cast();
            NewSpellObject();
        }
    }

    private void NewSpellObject() {
        var (position, rotation) = Spell.GetTransform(caster, Spell.HandOffset);
        activeSpellInstance = Instantiate(selectedSpell.prefab, position, rotation);
        activeSpellInstance.GetComponent<Spell>().caster = caster;
    }

    private void Awake() {
        caster = GetComponent<Character>();
        spellContainer = spellPrefabs
            .Select(prefab => new SpellIncantation(prefab))
            .ToArray();
    }

    private void Start() {
        ChooseSpell(0);
    }
}