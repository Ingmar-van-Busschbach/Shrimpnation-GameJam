using UnityEngine;
using System.Collections;

public class Shooter2D : MonoBehaviour
{
    GameObject bulletInst;
    Vector3 startingScale;
    Vector3 localScale = new Vector3(1f, 1f, 1f);

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float randomDeviation;

    private void Start()
    {
        startingScale = this.transform.localScale;
    }
    public void HandleGunRotation(Vector2 aimDirection)
    {
        this.transform.right = aimDirection;
        
        if (this.transform.rotation.z > 90 || this.transform.rotation.z < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }
        this.transform.localScale = Vector3.Scale(localScale, startingScale);
    }
    public void Shoot()
    {
        Quaternion randomDeviationRotator = Quaternion.Euler(0,0,Random.Range(-randomDeviation, randomDeviation));
        if(bullet != null)
        {
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, this.transform.rotation * randomDeviationRotator);
        }
    }
}
