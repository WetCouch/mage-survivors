using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character {
    internal readonly int expValue = 50;

    public Enemy() : base((100, 0), (0, 0)) {}
}
