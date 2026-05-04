using System.Collections.Generic;
using UnityEngine;

namespace Dev4All.CodeSnippets.LootIdle
{
    public class AutoClicker : MonoBehaviour
    {
        [SerializeField] private float clickInterval = 3f;

        private float timer;

        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= clickInterval)
            {
                timer = 0f;
                ClickOneObject();
            }
        }

        private void ClickOneObject()
        {
            IClickable[] clickables = FindAllClickables();

            if (clickables.Length == 0)
                return;

            IClickable target = clickables[0];

            // Get position of target so popups appear at correct location
            //Chest chest = (target as MonoBehaviour)?.GetComponent<Chest>();
            //transform.position = chest.transform.position;

            target.Click();
        }

        private IClickable[] FindAllClickables()
        {
            MonoBehaviour[] behaviours = FindObjectsByType<MonoBehaviour>(
                FindObjectsSortMode.None
            );

            List<IClickable> result = new List<IClickable>();

            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour is IClickable clickable)
                {
                    result.Add(clickable);
                }
            }

            return result.ToArray();
        }
    }
}