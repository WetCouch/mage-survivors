using UnityEngine;

public class CharacterHead : MonoBehaviour {
    private float totalRotation = 0;
    private readonly float xRotationLimit = 80;

    public void Rotate(float xRotation) {
        totalRotation = Mathf.Clamp(totalRotation + xRotation, -xRotationLimit, xRotationLimit);

        transform.rotation = Quaternion.Euler(totalRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}