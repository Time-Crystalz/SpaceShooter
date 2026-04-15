using UnityEngine;

public class CameraAspectFit : MonoBehaviour
{
    private Camera cam;
    private float defaultOrthographicSize = 9f;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        AdjustCamera();
    }

    private void AdjustCamera()
    {
        float targetAspect = 9f / 16f;   // your mobile portrait ratio
        float currentAspect = (float)Screen.width / Screen.height;

        if (currentAspect > targetAspect)
        {
            // PC / landscape - keep default size, no adjustment needed
            cam.orthographicSize = defaultOrthographicSize;
        }
        else
        {
            // Mobile / portrait - adjust to fit width
            cam.orthographicSize = defaultOrthographicSize * (targetAspect / currentAspect);
        }
    }
}