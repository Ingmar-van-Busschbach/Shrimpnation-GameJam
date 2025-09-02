using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Shooter2D))]
public class EnemyShootInput : MonoBehaviour
{
    Shooter2D shooter;
    GameObject player;

    private void Start()
    {
        shooter = GetComponent<Shooter2D>();
        player = FindFirstObjectByType<PlayerInput>().gameObject;
    }

    void Update()
    {
        Vector2 direction = ((Vector2)player.gameObject.transform.position - (Vector2)this.gameObject.transform.position).normalized;
        shooter.HandleGunRotation(direction);
    }
}
