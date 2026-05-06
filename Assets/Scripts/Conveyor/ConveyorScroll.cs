using UnityEngine;

public class ConveyorScroll : MonoBehaviour
{
    public float speed = -0.3f;

    private Renderer rend;
    private Material mat;
    private Vector2 offset;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
    }

    void Update()
    {
        offset.x += speed * Time.deltaTime;
        mat.mainTextureOffset = offset;
    }
}