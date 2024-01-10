using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private int level = 0;
    private int exp = 0;

    private int nextLevelExp;

    private readonly float movementSpeed = 15;
    private readonly int expMultiplier = 50;

    [SerializeField] GameObject spellPrefab;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI levelText;

    public void UpdateExp(int newExp) {
        exp += newExp;
        
        if (exp >= nextLevelExp) {
            level++;
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
        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * movementSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * movementSpeed);
    }

    private void CastSpell() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Instantiate(spellPrefab, transform.position, transform.rotation);
        }
    }
}
