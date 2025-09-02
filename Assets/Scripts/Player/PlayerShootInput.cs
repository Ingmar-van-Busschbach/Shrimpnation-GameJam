using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Shooter2D))]
public class PlayerShootInput : MonoBehaviour
{
    Shooter2D shooter;
    float currentDelay;

    [SerializeField] private float shootDelay = 0.2f;

    private void Start()
    {
        shooter = GetComponent<Shooter2D>();
    }

    void Update()
    {
        Vector2 worldposition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (worldposition - (Vector2)this.gameObject.transform.position).normalized;
        shooter.HandleGunRotation(direction);

        currentDelay -= Time.deltaTime;
        if (Mouse.current.leftButton.isPressed && currentDelay <= 0)
        {
            currentDelay = shootDelay;
            shooter.Shoot();
        }
    }
}
