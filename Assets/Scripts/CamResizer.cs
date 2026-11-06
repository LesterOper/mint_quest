using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CamResizer : MonoBehaviour
{
    public const float BaseScreenHalfHeight = 6.4f;
    public const float BaseScreenHalfWidth = 4.8f;
    public const float BaseScreenHeight = BaseScreenHalfHeight * 2;
    public const float BaseScreenWidth = BaseScreenHalfWidth * 2;

    public static float ScreenHalfHeight =>
        BaseScreenHalfHeight * Mathf.Max(((float)Screen.height / Screen.width) / (16f / 9f), 1f);    
    public static float ScreenHalfWidth =>
        BaseScreenHalfWidth * Mathf.Max(((float)Screen.height / Screen.width) / (16f / 9f), 1f);

    private void Awake()
    {
        GetComponent<Camera>().orthographicSize = ScreenHalfHeight;
    }
}