using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Shooter2D))]
public class PlayerShootInput : MonoBehaviour
{
    Shooter2D shooter;
    float currentDelay;
    Vector3 startingScale;

    [SerializeField] private float shootDelay = 0.2f;

    private void Start()
    {
        shooter = GetComponent<Shooter2D>();
        startingScale = transform.localScale;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 worldposition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, (Camera.main.transform.position.z * -1f)));
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
