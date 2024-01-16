using System;

public abstract class BaseCharacterResource {
    public event Action<int, int> OnChange;

    protected int current;
    protected int max;

    protected readonly int multiplier;

    public BaseCharacterResource(int multiplier, int level) {
        this.multiplier = multiplier;
        Upgrade(level);
    }

    public abstract void Upgrade(int level);

    public bool IsMinimum(int value) {
        return current >= value;
    }

    public bool IsMax() {
        return current >= max;
    }

    public void Change(int value) {
        current += value;
        EmitChange();
    }

    public void EmitChange() {
        OnChange?.Invoke(current, max);
    }
}
