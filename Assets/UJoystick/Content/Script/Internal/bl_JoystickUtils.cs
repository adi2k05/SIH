using UnityEngine;
using UnityEngine.InputSystem;

public static class bl_JoystickUtils
{
    public static Vector3 TouchPosition(this Canvas canvas, int touchID)
    {
        Vector3 result = Vector3.zero;

        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
#if UNITY_EDITOR
            result = Mouse.current.position.ReadValue();
#else
            if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
            {
                // Clamp to avoid invalid index
                touchID = Mathf.Clamp(touchID, 0, Touchscreen.current.touches.Count - 1);
                result = Touchscreen.current.touches[touchID].position.ReadValue();
            }
#endif
        }
        else if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            Vector2 localPos = Vector2.zero;
#if UNITY_EDITOR
            Vector3 pos = Mouse.current.position.ReadValue();
#else
            Vector3 pos = Vector3.zero;
            if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
            {
                touchID = Mathf.Clamp(touchID, 0, Touchscreen.current.touches.Count - 1);
                pos = Touchscreen.current.touches[touchID].position.ReadValue();
            }
#endif
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                pos,
                canvas.worldCamera,
                out localPos
            );
            result = canvas.transform.TransformPoint(localPos);
        }

        return result;
    }
}