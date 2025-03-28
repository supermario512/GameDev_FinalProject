using UnityEngine;
using TMPro;

public class HpNumber : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerScript player;
    private TextMeshProUGUI hpNum;
    void Start()
    {
        player = FindObjectOfType<PlayerScript>();

        hpNum = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        hpNum.text = "Health: " + player.currentHealth.ToString();
    }
}
