using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    bool grabbed;
    GameObject Char;
    Player player;
    Vector3 difference;
    Vector3 playerpos;
    Vector3 trashpos;
    // Start is called before the first frame update
    void Start()
    {
        Char = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerpos = Char.transform.position;
        grabbed = false;
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (grabbed == true)
        {
            difference = Char.transform.position - playerpos;
            this.transform.position = Char.transform.position + new Vector3(0.5f,0f,0f);
        }
        else return;
        
       
        
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Space) && grabbed == false && player.grab ==false )
        {

            grabbed = true;
            player.grab = true;
            Debug.Log(grabbed);
        } else if ( collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Space) && grabbed == true && player.grab == true   )
        {
            grabbed = false;
            player.grab = false;

        }
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TrashBin" && player.grab == true)
        {
            player.grab = false;
            Destroy(this.gameObject);
            player.score += 1;
        }
    }
}
