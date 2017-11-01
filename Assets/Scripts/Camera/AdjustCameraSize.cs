using UnityEngine;

public class AdjustCameraSize : MonoBehaviour {

	float cameraSize;
	float aspectRatio;
	float currentAspectRatio;
	float adjustedCameraSize;

	// Use this for initialization
	void Start () {
        Camera camera = GetComponent<Camera>();
#if (UNITY_IOS || UNITY_ANDROID)
		cameraSize = 4.68f;
		aspectRatio = 2.0f / 3.0f;
		currentAspectRatio = (float)Screen.width / (float)Screen.height;
		adjustedCameraSize = (aspectRatio / currentAspectRatio) * cameraSize;
		camera.orthographicSize = adjustedCameraSize;
#endif
    }
}