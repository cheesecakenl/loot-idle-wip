using UnityEngine;

public class PopupAmount : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float heightOffset = 2f;

    private Vector3 target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Target is slightly above the starting position
        target = transform.position + Vector3.up * heightOffset;

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );
    }
}
