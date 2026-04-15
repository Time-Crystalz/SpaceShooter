using UnityEngine;

public class MobileUIManager : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR
        // In editor, check if simulator is active
        // Hide controls when in normal Game view (PC)
        bool isMobileSimulator = UnityEditor.EditorUserBuildSettings.activeBuildTarget 
            == UnityEditor.BuildTarget.Android 
            || UnityEditor.EditorUserBuildSettings.activeBuildTarget 
            == UnityEditor.BuildTarget.iOS;

        gameObject.SetActive(isMobileSimulator);
#else
        // In actual builds
        gameObject.SetActive(Application.isMobilePlatform);
#endif
    }
}