using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
public class LevelEnder : MonoBehaviour
{
    GameObject player;
    SceneLoader sceneLoader;
    bool activated;

    [SerializeField] private string levelToLoad;
    [SerializeField] private Vector2 bottomLeftSpawnTrigger;
    [SerializeField] private Vector2 topRightSpawnTrigger;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerInput>().gameObject;
        sceneLoader = GetComponent<SceneLoader>();
    }

    private void Update()
    {
        if (activated)
        {
            return;
        }
        if (
            player.transform.position.x > this.transform.position.x + bottomLeftSpawnTrigger.x &&
            player.transform.position.y > this.transform.position.y + bottomLeftSpawnTrigger.y &&
            player.transform.position.x < this.transform.position.x + topRightSpawnTrigger.x &&
            player.transform.position.y < this.transform.position.y + topRightSpawnTrigger.y
            )
        {
            activated = true;
            sceneLoader.LoadScene(levelToLoad);
        }
    }
}
