using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NearbyDroids;
namespace Chat
{

    using GooglePlayGames.BasicApi.Nearby;
    using UnityEngine;


    public class ChatMenuEvents : MonoBehaviour
    {
        public GameObject mainMenuPanel;
        public GameObject multiplayerRoomPanel;
        public GameObject createRoomGroup;



        internal void Awake()
        {
            // show the main menu at start
            if (mainMenuPanel != null)
            {
                mainMenuPanel.SetActive(true);
              
            }
        }
        public void MainMenu()
        {
            GameManager.Instance.StopPlaying();
        }

        public void CreateRoom()
        {
            mainMenuPanel.SetActive(false);
            multiplayerRoomPanel.SetActive(true);
            createRoomGroup.SetActive(true);
            multiplayerRoomPanel.GetComponent<Multiplayer>().Joining = false;
        }
        public void JoinRoom()
        {
            mainMenuPanel.SetActive(false);
            multiplayerRoomPanel.SetActive(true);
            createRoomGroup.SetActive(false);
            multiplayerRoomPanel.GetComponent<Multiplayer>().Joining = true;
        }


    }
}