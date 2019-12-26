using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public Text TimeText;
    public float time = 60f;
    int playerCount;
    public GameObject WaitScreen;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    int score;
    public int goal;
    // Start is called before the first frame update

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        
    }
    // Update is called once per frame
    void Update()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        score = PhotonNetwork.PlayerList[0].GetScore();
        if (playerCount >= 2)
        {
            WaitScreen.SetActive(false);
            //Time.timeScale = 1; 
            time -= Time.deltaTime;
            
        }
        
        TimeText.text = time.ToString();
        if (time <= 0)
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);

        }

        if (score >=goal)
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
        }
        
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        
    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
        
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Player", new Vector2 (Random.Range(-38.7f, 43.2f),transform.position.y), Quaternion.identity);

        //Time.timeScale = 0;

    }
}
