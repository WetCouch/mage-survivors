using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    private static TextMeshProUGUI _manaText;
    private static TextMeshProUGUI _expText;
    private static TextMeshProUGUI _levelText;

    [SerializeField]
    private TextMeshProUGUI manaText;
    [SerializeField]
    private TextMeshProUGUI expText;
    [SerializeField]
    private TextMeshProUGUI levelText;

    public static void UpdateLevelText(int level) {
        _levelText.text = $"Level: { level }";
    }

    public static void UpdateExpText(int curr, int nextLevel) {
        _expText.text = $"Exp: { curr } / { nextLevel }";
    }

    public static void UpdateManaText(int curr, int max) {
        _manaText.text = $"Mana: { curr } / { max }";
    }

    protected void Awake() {
        _expText = expText;
        _levelText = levelText;
        _manaText = manaText;
    }
}