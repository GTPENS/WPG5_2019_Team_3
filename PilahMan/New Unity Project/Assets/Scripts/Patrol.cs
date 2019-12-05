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
    bool isRun = false;

    //djikstra
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;

    //Spawn Rubbish
    public GameObject[] rubish;
    public Vector3 spawnpos;
    public float nextspawn = 0f;
    public float spawnrate = 2f;
    public bool stop;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        homePosition = GameObject.FindGameObjectWithTag("HomePosition").transform;
        StartCoroutine(waitSpawner());
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
            while (Vector3.Distance(target.position, transform.position) <= runRad
            && Vector3.Distance(target.position, transform.position) > dashRad)
            {
                isRun = true;
                Debug.Log("Runaway");
                transform.position = Vector3.MoveTowards(transform.position, homePosition.position, moveSpeed * Time.deltaTime);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > runRad)
        {
            if(Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
            }else
            {
                changeGoal();
            }

        }
        else
        {
            isRun = false;
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

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(nextspawn);

        while (!stop && isRun == false )
        {
            //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 1));
            Vector3 enemyPosition = transform.position;
            Instantiate(rubish[0], enemyPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnrate);
        }
    }
}
