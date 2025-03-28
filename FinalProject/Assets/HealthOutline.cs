using UnityEngine;

public class HealthOutline : MonoBehaviour
{
    private RectTransform healthPanel;
    public PlayerScript player;
    private float maxWidth;
    private float scaleMultiplier = 0.002f;

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();

        healthPanel = GetComponent<RectTransform>();
        maxWidth = healthPanel.sizeDelta.x;

        // Set pivot to the left side (0, 0.5)
        healthPanel.pivot = new Vector2(0f, 0.5f);       
    }

    void Update()
    {
        if (player == null)
        {
            return; // Avoid errors if the player is not assigned
        }

        // Calculate health percentage
        float healthPercentage = Mathf.Clamp(player.currentHealth / player.maxHealth, 0f, 1f);
        
        float healthOutlineScale = player.maxHealth / player.initialHealth;

        // Update health bar width based on health percentage (resizing from the left)
        healthPanel.sizeDelta = new Vector2(maxWidth * healthOutlineScale, healthPanel.sizeDelta.y);
    }
}
