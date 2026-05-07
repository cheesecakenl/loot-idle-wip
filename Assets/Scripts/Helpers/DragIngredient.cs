using UnityEngine;

public class DragIngredient : MonoBehaviour
{
    private Camera _cam;
    private Transform _transform;

    private Vector3 _dragOffset;

    [SerializeField] private float _dragSpeed = 50f;

    private bool _isDragging;

    private Rigidbody2D rb;
    [SerializeField] private float gravityScale = 3f;

    private void Awake()
    {
        _cam = Camera.main;
        _transform = transform;

        rb = GetComponent<Rigidbody2D>();        
    }

    private void OnMouseDown()
    {
        rb.gravityScale = 0;

        _isDragging = true;        
        _dragOffset = _transform.position - GetMousePosition();
    }

    private void Update()
    {
        // Stop dragging only when mouse button is released
        if (_isDragging && Input.GetMouseButtonUp(0))
        {
            rb.gravityScale = gravityScale;

            _isDragging = false;            
        }

        // Continue dragging while button is held
        if (_isDragging && Input.GetMouseButton(0))
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position,
                GetMousePosition() + _dragOffset,
                _dragSpeed * Time.deltaTime
            );
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 position = _cam.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;

        return position;
    }
}