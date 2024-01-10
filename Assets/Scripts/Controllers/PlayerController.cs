using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class PlayerController : MonoBehaviour {
    private int level = 0;
    private int exp = 0;

    private int nextLevelExp;

    private readonly float movementSpeed = 15;
    private readonly float rotationSpeed = 90;
    private readonly int expMultiplier = 50;

    [SerializeField] GameObject spellPrefab;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI levelText;

    public void UpdateExp(int newExp) {
        exp += newExp;
        
        if (exp >= nextLevelExp) {
            Debug.Log(level);
            level++;
            Debug.Log(level);
            nextLevelExp = expMultiplier * level * level;
        };

        expText.text = $"Exp: { exp } / { nextLevelExp }";
        levelText.text = $"Level: { level }";
    }

    void Start() {
        UpdateExp(0);
    }

    void FixedUpdate() {
        HandleMovement();
    }

    void Update() {
        CastSpell();
    }

    private void HandleMovement() {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * movementSpeed);
        transform.Rotate(Vector3.up * Time.deltaTime * horizontalInput * rotationSpeed);
    }

    private void CastSpell() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(spellPrefab, transform.position, transform.rotation);
        }
    }
}
