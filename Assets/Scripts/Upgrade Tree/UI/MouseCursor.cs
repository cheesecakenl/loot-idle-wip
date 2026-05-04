using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public static MouseCursor instance = null;

    private Vector2 _clickHotspot;

    private Texture2D _mouseTextureHand;
    private Texture2D _mouseTextureSword;

    public enum MouseCursorType
    {
        HAND,
        SWORD
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
        _mouseTextureHand = Resources.Load<Texture2D>("Cursor/cursorHand_grey");
        _mouseTextureSword = Resources.Load<Texture2D>("Cursor/cursorSword_silver");

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
        Texture2D cursorTexture;

        if (type == MouseCursorType.HAND)
        {
            cursorTexture = _mouseTextureHand;
        }
        else
        {
            cursorTexture = _mouseTextureSword;
        }

        Cursor.SetCursor(cursorTexture, _clickHotspot, CursorMode.ForceSoftware);
        Cursor.visible = true;
    }
}