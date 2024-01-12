using System;
using System.Collections;
using UnityEngine;

public class CharacterResource {

    public int current;
    private int max ;
    private readonly int multiplier;
    private readonly int regen;

    public event Action<int, int> OnChange;

    public CharacterResource(int multiplier, int regen) {
        current = multiplier;
        max = multiplier;
        this.multiplier = multiplier;
        this.regen = regen;
    }

    public bool IsMinimum(int value) {
        return current >= value;
    }

    public void Change(int value) {
        current += value;
    }

    public void Upgrade(int level) {
        max = multiplier * level;
        current = max;
        EmitChange();
    }

    public IEnumerator Regenerate() {
        while (regen > 0) {
            current += regen;
            if (current > max) current = max;
            EmitChange();
            yield return new WaitForSeconds(1);
        }
    }

    private void EmitChange() {
        OnChange(current, max);
    }
}