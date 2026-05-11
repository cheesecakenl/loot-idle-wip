using UnityEngine;

public class CauldronStirring : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform stirStick;

    [Header("Stirring")]
    [SerializeField] private float maxDistanceFromCauldron = 4f;
    [SerializeField] private float requiredStirAmount = 3000f;
    [SerializeField] private float minimumMouseMovement = 5f;

    [Header("Stick")]
    [SerializeField] private float stickMaxDistance = 1.5f;
    [SerializeField] private float stickYOffset = -0.5f;

    private float stirProgress;
    private Vector2 previousMousePosition;
    private bool stirring;

    private void Start()
    {
        stirStick.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorld = GetMouseWorldPosition();

            if (Vector2.Distance(mouseWorld, transform.position) <= maxDistanceFromCauldron)
            {
                stirring = true;
                previousMousePosition = Input.mousePosition;
                stirStick.gameObject.SetActive(true);
            }
        }

        if (Input.GetMouseButton(0) && stirring)
        {
            Vector2 currentMousePosition = Input.mousePosition;

            float horizontalMovement =
                Mathf.Abs(currentMousePosition.x - previousMousePosition.x);

            if (horizontalMovement >= minimumMouseMovement)
            {
                stirProgress += horizontalMovement;

                Debug.Log($"Stir progress {stirProgress}");

                UpdateStirStick();

                if (stirProgress >= requiredStirAmount)
                {
                    Brew();
                    stirring = false;
                    stirStick.gameObject.SetActive(false);
                }
            }

            previousMousePosition = currentMousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            stirring = false;
            stirStick.gameObject.SetActive(false);
        }
    }

    private void UpdateStirStick()
    {
        Vector2 mouseWorld = GetMouseWorldPosition();
        Vector2 cauldronCenter = transform.position;

        float xOffset = mouseWorld.x - cauldronCenter.x;
        xOffset = Mathf.Clamp(xOffset, -stickMaxDistance, stickMaxDistance);

        stirStick.position = new Vector2(
            cauldronCenter.x + xOffset,
            stirStick.position.y
        );
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = -cam.transform.position.z;
        return cam.ScreenToWorldPoint(mouse);
    }

    private void Brew()
    {
        Debug.Log("BREW!");
        stirProgress = 0;
    }
}