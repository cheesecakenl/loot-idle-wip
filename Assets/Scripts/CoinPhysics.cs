using UnityEngine;

namespace Dev4All.CodeSnippets.LootIdle
{
    public class CoinPhysics : MonoBehaviour
    {
        private float upwardForce = 10f;
        private float sidewaysForce = 5f;

        private Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 3f;
        }

        public void Launch()
        {
            float x = Random.Range(-sidewaysForce, sidewaysForce);
            float y = Random.Range(upwardForce, upwardForce + upwardForce);

            Vector2 force = new Vector2(x, y);

            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}