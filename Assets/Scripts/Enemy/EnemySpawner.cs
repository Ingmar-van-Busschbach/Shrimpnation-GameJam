using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject enemyInst;
    GameObject player;
    private float currentDelay;

    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private Vector2 bottomLeftSpawnTrigger;
    [SerializeField] private Vector2 topRightSpawnTrigger;
    [SerializeField] private float spawnDelay = 3f;
    [SerializeField] private Transform spawnLocation;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerInput>().gameObject;
    }

    private void Update()
    {
        currentDelay -= Time.deltaTime;
        if (
            player.transform.position.x > this.transform.position.x + bottomLeftSpawnTrigger.x &&
            player.transform.position.y > this.transform.position.y + bottomLeftSpawnTrigger.y &&
            player.transform.position.x < this.transform.position.x + topRightSpawnTrigger.x &&
            player.transform.position.y < this.transform.position.y + topRightSpawnTrigger.y
            )
        {
            if (currentDelay <= 0)
            {
                currentDelay = spawnDelay;
                SpawnEnemy();
            }
        }
    }
    void SpawnEnemy()
    {
        enemyInst = Instantiate(enemyToSpawn, spawnLocation.position, this.transform.rotation);
    }
}
