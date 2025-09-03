using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    GameObject player;

    [SerializeField] private GameObject[] enemiesToActivate;
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private Vector2 bottomLeftSpawnTrigger;
    [SerializeField] private Vector2 topRightSpawnTrigger;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerInput>().gameObject;
    }

    private void Update()
    {
        if (
            player.transform.position.x > this.transform.position.x + bottomLeftSpawnTrigger.x &&
            player.transform.position.y > this.transform.position.y + bottomLeftSpawnTrigger.y &&
            player.transform.position.x < this.transform.position.x + topRightSpawnTrigger.x &&
            player.transform.position.y < this.transform.position.y + topRightSpawnTrigger.y
            )
        {
            foreach (GameObject enemyToActivate in enemiesToActivate)
            {
                enemyToActivate.GetComponent<EnemyInput>().enabled = true;
            }
        }
    }
    void SpawnEnemy()
    {
        enemyInst = Instantiate(enemyToSpawn, spawnLocation.position, this.transform.rotation);
    }
}
