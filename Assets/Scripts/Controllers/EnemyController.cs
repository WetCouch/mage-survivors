using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int hp = 100;
    private int expValue = 50;

    private PlayerController player;

    void Start() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other) {
        DetectSpellHit(other);
    }

    private void DetectSpellHit(Collider other) {
        if (other.gameObject.CompareTag("Spell")) {
            hp -= other.gameObject.GetComponent<SpellController>().damage;
            Destroy(other.gameObject);

            if (hp <= 0) Killed();
        }
    }

    private void Killed() {
        Destroy(gameObject);
        player.UpdateExp(expValue);
    }
}
