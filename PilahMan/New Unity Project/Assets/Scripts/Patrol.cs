using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public int health;
    public float moveSpeed;
    public Transform target;
    public float runRad;
    public float dashRad;
    public Transform homePosition;

    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        homePosition = GameObject.FindGameObjectWithTag("HomePosition").transform;
    }

    // Update is called once per frame
    void Update()
    {
        checkDistance();
    }

    void checkDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= runRad
            && Vector3.Distance(target.position, transform.position) > dashRad)
        {
            transform.position = Vector3.MoveTowards(transform.position, homePosition.position, moveSpeed * Time.deltaTime);
        }else if (Vector3.Distance(target.position, transform.position) > runRad)
        {
            if(Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
            }else
            {
                changeGoal();
            }
            
        }
    }

    private void changeGoal()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}
