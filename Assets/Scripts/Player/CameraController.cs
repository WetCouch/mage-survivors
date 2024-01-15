using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private Character targetObject;

    private readonly Vector3 cameraOffset = new Vector3(0, 1, 0);

    void LateUpdate() {
        FollowTarget();
    }

    private void FollowTarget() {
        transform.position = targetObject.transform.position + cameraOffset;
        transform.rotation = targetObject.GetComponent<Character>().HeadTransform().rotation;
    }
}
