using GooglePlayGames;
using GooglePlayGames.BasicApi.Nearby;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using NearbyDroids;
using System.IO;

public class RoomManager : MonoBehaviour

{   public bool isClient
    {
        get;set;

    }
    NearbyRoom room;
    bool connected;


    private Action<NearbyRoom, bool> roomDiscoveredCallback;
    
    //private static RoomListener roomListener = new RoomListener();
    // private readonly IDiscoveryListener listener;
    //  public InputField roomName, username;
    //private string localPlayerName;
    public const string RoomNameKey = "roomname";
    public const string PlayerNameKey = "playername";
    public GameObject  textObject;
    
    //private bool joining;
    void Awake()
    {
      
        //DontDestroyOnLoad(gameObject);

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public bool onDiscovery
    {
        get; set;
    }
    public bool onAdvertise
    {
        get; set;
    }
    public bool onFound

    { get; set; }
    public IMessageListener messageListener { get; private set; }

    public void createRoom(string rName, string uName)
    {

        NearbyController nearbyController = new NearbyController();
        nearbyController.Advertise(rName);
        return;
    }
    
    public void onCreateRoom()
    {

        NearbyRoom.StopAll();
        PlayGamesPlatform.InitializeNearby((client) =>
        {
            Debug.Log("Nearby connections initialized");
        });
        
        NearbyRoom nearbyRoom = new NearbyRoom("Most Popular Room!!");
        nearbyRoom.AutoJoin = true;
        nearbyRoom.AlwaysOpen = true;
        isClient = false; 
        nearbyRoom.OpenRoom();
        


        //List<string> appIdentifiers = new List<string>();
        //appIdentifiers.Add(PlayGamesPlatform.Nearby.GetAppBundleId());
        //PlayGamesPlatform.Nearby.StartAdvertising(
        //          "Awesome Game Host",  // User-friendly name
        //            appIdentifiers,  // App bundle Id for this game
        //           TimeSpan.FromSeconds(0),// 0 = advertise forever
        //           (AdvertisingResult result) =>
        //           {

        //               Debug.Log("OnAdvertisingResult: " + result);
        //           },
        //              (ConnectionRequest request) =>
        //              {

        //                  Debug.Log("Received connection request: " +
        //                   request.RemoteEndpoint.ServiceId + " " +
        //                   request.RemoteEndpoint.EndpointId + " " +
        //                   request.RemoteEndpoint.Name);

        //              }
        //           );

    }

    byte[] bytes = Encoding.ASCII.GetBytes("asdasd");
    

    public void OnEndpointFound(EndpointDetails discoveredEndpoint)
    {
        Debug.Log("Found Endpoint: " +
                  discoveredEndpoint.ServiceId + " " +
                  discoveredEndpoint.EndpointId + " " +
                  discoveredEndpoint.Name);
        PlayGamesPlatform.Nearby.SendConnectionRequest(
               "Local Game player",  // the user-friendly name
               discoveredEndpoint.EndpointId,   // the discovered endpoint	
               bytes,  // byte[] of data
               (response) => {
                   Debug.Log("response: " +
                                response.ResponseStatus);
               },
               (IMessageListener)messageListener);

    }
    public void StartMultiplayer()
    {

       


    }
    
    public void StartDiscovery()
    {

        isClient = true;
        Debug.Log("StartDiscovery called");
        onStartDiscovery();
    }


    internal void onStartDiscovery()
    {

        NearbyRoom.StopAll();


        NearbyRoom.FindRooms(OnRoomFound); 
        
        //PlayGamesPlatform.InitializeNearby((client) =>
        //{
        //    Debug.Log("Nearby connections initialized");
        //});
        //Debug.Log("onStartDiscovery called"); 
        //PlayGamesPlatform.Nearby.StartDiscovery(
        //                PlayGamesPlatform.Nearby.GetServiceId(),
        //                TimeSpan.FromSeconds(0),
        //                listener);
    }
      
    internal void OnRoomFound(NearbyRoom room, bool available)
    {

        if(available)

        {

            NearbyPlayer nearbyPlayer = new NearbyPlayer("Fatih");
            
            room.JoinRoom(nearbyPlayer, Encoding.ASCII.GetBytes("asdasd"), OnRoomJoined);
            // GameObject obj = Instantiate() as GameObject;
        }
    }
    
    internal void OnRoomJoined (ConnectionResponse response)
    {
        Debug.Log("OnRoomJoined Called status: " + response.ResponseStatus);
        if (response.ResponseStatus == ConnectionResponse.Status.Accepted)
        {
            // if we are connected, stop looking for rooms.
            NearbyRoom.StopRoomDiscovery();

            // the first payload is sent with the response so we can initialize
            // the game scene.
            OnMessageReceived(room.Address, response.Payload);
            connected = true;
        }
    }
    internal void OnMessageReceived(NearbyPlayer sender, byte[] data)
    {
        UpdateGameStateFromData(data);
    }
    internal void UpdateGameStateFromData(byte[] data)
    {
        MemoryStream ms = new MemoryStream(data);
      
        
    }

}
