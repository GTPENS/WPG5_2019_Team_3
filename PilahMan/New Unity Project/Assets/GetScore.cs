using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class GetScore : MonoBehaviour
{
    public Text scoretext;
    // Start is called before the first frame update
    void Start()
    {
        scoretext = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        scoretext.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
        //if (PhotonNetwork.IsMasterClient)
        //{

        //    scoretext.text = PhotonNetwork.PlayerList[0].GetScore().ToString();
        //    Debug.Log(PhotonNetwork.PlayerList[0].GetScore().ToString());
        //}
    }
}
