using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public bool grabbed;
    private RaycastHit2D hit;
    public float distance;
    public Transform holdPoint;
    public float throwForce;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!grabbed)
            {
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

                if (hit.collider != null && hit.collider.tag == "Rubish")
                {
                    grabbed = true;
                }
            }
            else
            {
                grabbed = false;

                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwForce;
                }
            }
        }
        if (grabbed)
        {
            hit.collider.gameObject.transform.position = holdPoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}
