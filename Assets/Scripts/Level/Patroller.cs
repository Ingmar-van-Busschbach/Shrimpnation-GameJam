using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Patrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;
    public float speed;
    [SerializeField] public Rigidbody2D rb;
    Vector2 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = new Vector2(rb.transform.position.x, rb.transform.position.y);
        currentPoint = pointB.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - rb.transform.position;
        if(rb.linearVelocity.y > 0f)
        {
            if(currentPoint == pointB.transform)
            {
                rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y * 0.5f);
            }
            else
            {
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y * 0.5f);
            }
        }
        else
        {
            if(currentPoint == pointB.transform)
            {
                rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            }
        }

        if(Vector2.Distance(rb.transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }
        if(Vector2.Distance(rb.transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
        if(rb.transform.position.y < -20f)
        {
            rb.transform.position = originalPosition;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        Gizmos.DrawWireSphere(transform.position, .5f);
    }
}