using UnityEngine;

public class ItemMover : MonoBehaviour
{
    public float speed = 2f;

    private void OnTriggerStay2D(Collider2D other)
    {
        other.transform.position += Vector3.right * speed * Time.deltaTime;
    }
}