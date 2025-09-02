using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BulletBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float velocity = 15f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private LayerMask collisionLayers;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetStraightVelocity();
        DestroyBullet();
    }

    private void SetStraightVelocity()
    {
        rb.linearVelocity = transform.right * velocity;
    }

    private void DestroyBullet()
    {
        Destroy(this.gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collisionLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            IDamageAble iDamageAble = collision.gameObject.GetComponent<IDamageAble>();
            if(iDamageAble != null)
            {
                iDamageAble.ApplyDamage(damage);
            }

            Destroy(this.gameObject);
        }
    }
}
