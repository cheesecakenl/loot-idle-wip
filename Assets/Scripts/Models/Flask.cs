using UnityEngine;

public class Flask : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ingredient ingredient = collision.gameObject.GetComponent<Ingredient>();

        ingredient.DoDrop();
    }
}
