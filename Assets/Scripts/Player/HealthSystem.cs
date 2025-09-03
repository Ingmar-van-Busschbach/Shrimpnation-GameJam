using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthSystem : MonoBehaviour, IDamageAble
{
    [SerializeField] private float maxHealth = 10f;
    private float currentHealth;
    [SerializeField] private bool loadSceneOnDeath;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private SceneLoader sceneLoader;

    void Start()
    {
        currentHealth = maxHealth;
        if(loadSceneOnDeath){
            sceneLoader = GetComponent<SceneLoader>();
        }
    }

    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }
    public float GetHealth(){
        return currentHealth;
    }
    public float GetMaxHealth(){
        return maxHealth;
    }

    public void OnDeath()
    {
        if(loadSceneOnDeath){
            sceneLoader.LoadScene(sceneToLoad);
        }
        else{
            Destroy(this.gameObject);
        }
    }
}
