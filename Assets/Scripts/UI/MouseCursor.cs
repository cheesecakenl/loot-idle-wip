using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public static MouseCursor instance = null;

    private Vector2 _clickHotspot;

    [SerializeField] private Texture2D _mouseTextureHand;

    public enum MouseCursorType
    {
        HAND
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);

        Init();
    }

    void Init()
    {
        // Offset from top left of the texture
        _clickHotspot = new Vector2(0f, 0f);
    }

    public void HideCursor()
    {
        Cursor.SetCursor(null, _clickHotspot, CursorMode.Auto);
        Cursor.visible = false;
    }

    public void ShowCursor(MouseCursorType type)
    {
        Texture2D cursorTexture = null;

        if (type == MouseCursorType.HAND)
        {
            cursorTexture = _mouseTextureHand;
        }

        Cursor.SetCursor(cursorTexture, _clickHotspot, CursorMode.ForceSoftware);
        Cursor.visible = true;
    }
}