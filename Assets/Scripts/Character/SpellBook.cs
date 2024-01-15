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

internal class SpellBook {
    private readonly SpellIncantation[] spellContainer;

    private SpellIncantation selectedSpell;
    private GameObject activeSpellInstance;

    private PlayerController caster;

    public SpellBook(GameObject[] prefabs, PlayerController caster) {
        spellContainer = prefabs
            .Select(prefab => new SpellIncantation(prefab))
            .ToArray();

            this.caster = caster;

        ChooseSpell(0);
    }

    public void ChooseSpell(int index) {
        if (activeSpellInstance != null) {
           Object.Destroy(activeSpellInstance);
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
        Transform targetTransform = caster.HeadTransform();
        // TransformPoint(Vector3.forward * distance);
        // activeSpellInstance = Object.Instantiate(selectedSpell.prefab, caster.transform.position + Spell.SpellOffset, caster.transform.rotation);
        activeSpellInstance = Object.Instantiate(selectedSpell.prefab, targetTransform.TransformPoint(Vector3.forward + Spell.SpellOffset), targetTransform.rotation);
        activeSpellInstance.GetComponent<Spell>().caster = caster;
    }
}