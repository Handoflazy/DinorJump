using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        private List<LifeElementUI> healthImages;

        [SerializeField]
        private Sprite FullHealth, emptyHealth;

        [SerializeField]
        private LifeElementUI healthPrefab;
        public void Initialize(int MaxHealth)
        {
            healthImages = new List<LifeElementUI>();
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < MaxHealth; i++)
            {
                var life = Instantiate(healthPrefab);
                life.transform.SetParent(gameObject.transform, false);
                healthImages.Add(life);
            }
        }
        public void SetHealth(int currentHealth)
        {
            for (int i = 0; i < healthImages.Count; i++)
            {
                if (i < currentHealth)
                {
                    healthImages[i].SetSprite(FullHealth);
                }
                else
                {
                    healthImages[i].SetSprite(emptyHealth);
                }
            }
        }
    }
}