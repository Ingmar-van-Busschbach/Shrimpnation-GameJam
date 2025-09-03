using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthSystem))]
public class PlayerHealth : MonoBehaviour
{
    HealthSystem healthSystem;
    public Image healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthBar != null){
            healthBar.fillAmount = Mathf.Clamp(healthSystem.GetHealth() / healthSystem.GetMaxHealth(), 0, 1);
        }
    }
}
