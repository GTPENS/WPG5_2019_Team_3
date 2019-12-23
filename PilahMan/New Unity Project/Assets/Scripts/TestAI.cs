using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Photon.Pun;
public class TestAI : MonoBehaviourPun
{
    public Transform[] target;
    public float speed = 400f;
    public float nextWaypointDistance = 3f;

    public int currentPoint = 0;
    public Transform nextPoint;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    //stop trash cz Player
    //public Transform player;
    public float detectArea;
    public GameObject[] rubish;
    public Vector3 spawnpos;
    public float nextspawn = 5f;
    public float spawnrate = 5f;
    bool isStop;

    public int sampahJatuh = 0;
    public GameObject nextSampah;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        seeker.StartPath(rb.position, target[currentPoint].position, OnPathComplete);
        StartCoroutine(waitSpawner());
    }
    
    void UpdatePath()
    {
            seeker.StartPath(rb.position, target[currentPoint].position, OnPathComplete);
    }

    //void checkDistance()
    //{
    //    if (Vector3.Distance(player.position, transform.position) <= detectArea)
    //    {
    //        isStop = true;
    //    }
    //    else
    //    {
    //        isStop = false;
    //    }
    //}

    void changeGoal()
    {
        if (currentPoint == target.Length - 1)
        {
            currentPoint = 0;
            nextPoint = target[0];
        }
        else if (reachedEndOfPath == true)
        {
            currentPoint++;
            nextPoint = target[currentPoint];
            UpdatePath();
        }
    }

    void OnPathComplete(Path p)
    {
        path = p;
        currentWaypoint = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //checkDistance();
        if (path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            //Debug.Log("sampai");
            changeGoal();
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
           
            currentWaypoint++;
        }
        //if (force.x >= 0.01f)
        //{
        //    transform.localScale = new Vector3(-1f, 1f, 1f);
        //}
        //else if (force.x <= 0.01f)
        //{
        //    transform.localScale = new Vector3(1f, 1f, 1f);
        //}
    }

    void GantiSampah()
    {
        if (sampahJatuh == rubish.Length - 1)
        {
            sampahJatuh = 0;
            nextSampah = rubish[0];
        }
        else
        {
            sampahJatuh++;
            nextSampah = rubish[sampahJatuh];
        }
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(nextspawn);

        while (!isStop)
        {
            //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 1));
            Vector3 enemyPosition = transform.position;
            //Instantiate(rubish[sampahJatuh], enemyPosition, Quaternion.identity);
            
            PhotonNetwork.InstantiateSceneObject(rubish[sampahJatuh].name.ToString(), transform.position, Quaternion.identity, 0, null);
            GantiSampah();

            yield return new WaitForSeconds(spawnrate);
        }
    }
}
