using UnityEngine;

public class PanZoom : MonoBehaviour
{
    public RectTransform content;

    public float zoomSpeed = 0.1f;
    public float panSpeed = 10f;

    private float minZoom = 0.25f;
    private float maxZoom = 1f;

    void Update()
    {
        HandlePan();
        HandleZoom();
    }

    void HandlePan()
    {
        if (Input.GetMouseButton(1)) // RMB
        {
            Vector2 delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            content.anchoredPosition += delta * panSpeed;

            GameEvents.UI.OnTreePanningOrZooming?.Invoke();
        }
    }

    void HandleZoom()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll == 0) return;

        float scale = Mathf.Clamp(content.localScale.x + scroll * zoomSpeed, minZoom, maxZoom);
        content.localScale = Vector3.one * scale;
    }
}