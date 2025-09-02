using UnityEngine;

public class HurtBox : MonoBehaviour
{
    [SerializeField] LayerMask collisionLayers;
    [SerializeField] private float damagePerSecond = 1f;
    [SerializeField] Vector2 hurtBoxSize = new Vector2(1f, 1f);

    private void FixedUpdate()
    {
        RaycastHit2D boxCast = Physics2D.BoxCast((Vector2)this.transform.position, hurtBoxSize, 0f, new Vector2(1f, 0f), 0f, collisionLayers);
        if (boxCast.collider != null)
        {
            IDamageAble iDamageAble = boxCast.collider.gameObject.GetComponent<IDamageAble>();
            if (iDamageAble != null)
            {

                iDamageAble.ApplyDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
