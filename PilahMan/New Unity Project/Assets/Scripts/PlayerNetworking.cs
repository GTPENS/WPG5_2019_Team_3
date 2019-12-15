using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerNetworking : MonoBehaviour
{
    public MonoBehaviour[] scritpsToIgnore;
    public new Camera camera;

    private PhotonView photonview;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponentInChildren<Camera>();
        photonview = GetComponent<PhotonView>();
        if (!photonview.IsMine)
        {
            foreach (var script in scritpsToIgnore)
            {
                script.enabled = false;
                camera.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
