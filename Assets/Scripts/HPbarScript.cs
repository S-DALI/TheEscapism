using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets {
    public class HPbarScript : MonoBehaviour
    {

        [SerializeField] private Image HealthBarFilling;
        [SerializeField] private MonstersHealth health;
        [SerializeField] Transform enemy;
        [SerializeField] Gradient gradient;
        void Awake()
        {
            health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            health.HealthChanged -= OnHealthChanged;
        }
        void Update()
        {
            if (transform.localScale.x < 0 && enemy.localScale.x > 0 || transform.localScale.x > 0 && enemy.localScale.x < 0)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        void OnHealthChanged(float valueAsPercantage)
        
        {
            
            HealthBarFilling.fillAmount = valueAsPercantage;
            HealthBarFilling.color = gradient.Evaluate(valueAsPercantage);
            
        }
    }
}
