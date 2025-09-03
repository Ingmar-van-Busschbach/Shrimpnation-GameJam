using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Shooter2D))]
public class EnemyShootInput : MonoBehaviour
{
    Shooter2D shooter;
    GameObject player;
    float currentDelay;
    bool isShooting;

    [SerializeField] private float shootDelay = 0.2f;
    [SerializeField] private bool pauseBeforeShot;
    [SerializeField] private float pauseTimeBeforeShot;

    private void Start()
    {
        shooter = GetComponent<Shooter2D>();
        player = FindFirstObjectByType<PlayerInput>().gameObject;
    }

    void Update()
    {
        Vector2 direction = ((Vector2)player.gameObject.transform.position - (Vector2)this.gameObject.transform.position).normalized;
        
        if(!isShooting || !pauseBeforeShot)
        {
            shooter.HandleGunRotation(direction);
        }
        currentDelay -= Time.deltaTime;
        if (currentDelay <= 0)
        {
            currentDelay = shootDelay;
            if (pauseBeforeShot)
            {
                isShooting = true;
                StartCoroutine(WaitForShot(pauseTimeBeforeShot));
            }
            else
            {
                shooter.Shoot();
            }
        }
    }

    private IEnumerator WaitForShot(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        shooter.Shoot();
        yield return new WaitForFixedUpdate();
        isShooting = false;
    }
}
