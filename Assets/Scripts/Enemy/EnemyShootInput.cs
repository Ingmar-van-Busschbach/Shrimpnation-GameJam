using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Shooter2D))]
public class EnemyShootInput : MonoBehaviour
{
    Shooter2D shooter;
    GameObject player;
    float currentDelay;

    [SerializeField] private float shootDelay = 0.2f;

    private void Start()
    {
        shooter = GetComponent<Shooter2D>();
        player = FindFirstObjectByType<PlayerInput>().gameObject;
    }

    void Update()
    {
        Vector2 direction = ((Vector2)player.gameObject.transform.position - (Vector2)this.gameObject.transform.position).normalized;
        shooter.HandleGunRotation(direction);

        currentDelay -= Time.deltaTime;
        if (currentDelay <= 0)
        {
            currentDelay = shootDelay;
            shooter.Shoot();
        }
    }
}
