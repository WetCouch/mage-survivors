using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private GameObject targetObject;

    private Vector2 rotation = Vector2.zero;

    private readonly float sensitivity = 15;
    private readonly float yRotationLimit = 80;

    void LateUpdate()
    {
        FollowPosition();
        HandleRotation();
    }

    private void FollowPosition() {
        transform.position = targetObject.transform.position;
    }

	void HandleRotation() {
        rotation += new Vector2(Input.GetAxis("Mouse X") * sensitivity, Input.GetAxis("Mouse Y") * sensitivity);
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);

        (Quaternion x, Quaternion y) angle = (x: Quaternion.AngleAxis(rotation.x, Vector3.up), y: Quaternion.AngleAxis(rotation.y, Vector3.left));

		transform.rotation = angle.x * angle.y;
        targetObject.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
	}
}
