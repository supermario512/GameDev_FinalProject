using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;  // Reference to the slider component

    void Start()
    {
        // Set the max value of the slider to the max health
        slider.maxValue = 100f;  // Make sure this matches the maxHealth in EnemyScript
        slider.value = slider.maxValue;  // Start at full health
    }

    // This method will be called to update the health bar
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = Mathf.Max(currentHealth, 0f);
    }
}
