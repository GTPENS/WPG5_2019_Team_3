using UnityEngine;
using System.Collections;

//This script manages the player object
public class Player : MonoBehaviour
{
    private int speed = 5;
    private GameObject player;
    public bool grabbed;
    private RaycastHit2D hit;
    public float distance;
    public Transform holdPoint;
    public float throwForce;
  
    void Update()
    {
        //Get our raw inputs
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //Normalize the inputs
        Vector2 direction = new Vector2(x, y).normalized;
        //Move the player
        Move(direction);
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (!grabbed)
            {
                Physics2D.queriesStartInColliders = false;
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

    void Move(Vector2 direction)
    {
        //Find the screen limits to the player's movement
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //Get the player's current position
        Vector2 pos = transform.position;
        //Calculate the proposed position
        pos += direction * speed * Time.deltaTime;
        //Ensure that the proposed position isn't outside of the limits
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        //Update the player's position
        transform.position = pos;
    }
   
    

    //public void OnCollisionStay2D(Collision2D c)
    //{
    //    string layerName = LayerMask.LayerToName(c.gameObject.layer);
    //    //If the player hit an enemy bullet or ship...
    //    if (layerName == "Rubish" && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Debug.Log("NC");
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {

    //            if (!grabbed)
    //            {
    //                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

    //                if (hit.collider != null && hit.collider.tag == "Rubish")
    //                {
    //                    grabbed = true;
    //                    Debug.Log("NC");

    //                }
    //            }
    //            else
    //            {
    //                grabbed = false;

    //                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
    //                {
    //                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwForce;
    //                }
    //            }
    //        }
    //        if (grabbed)
    //        {
    //            hit.collider.gameObject.transform.position = holdPoint.position;
    //        }
    //    }


    //}
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}

        
    
