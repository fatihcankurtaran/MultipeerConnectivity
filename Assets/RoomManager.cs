using GooglePlayGames;
using GooglePlayGames.BasicApi.Nearby;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class RoomManager : MonoBehaviour
{
   
    //IDiscoveryListener listener;
    //  public InputField roomName, username;
    //private string localPlayerName;
    public const string RoomNameKey = "roomname";
    public const string PlayerNameKey = "playername";
    public GameObject  textObject;
    IDiscoveryListener listener;
    //private bool joining;
    void Awake()
    {
      
        DontDestroyOnLoad(gameObject);

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


        PlayGamesPlatform.InitializeNearby((client) =>
        {
            Debug.Log("Nearby connections initialized");
        });

        List<string> appIdentifiers = new List<string>();
        appIdentifiers.Add(PlayGamesPlatform.Nearby.GetAppBundleId());
        PlayGamesPlatform.Nearby.StartAdvertising(
                  "Awesome Game Host",  // User-friendly name
                    appIdentifiers,  // App bundle Id for this game
                   TimeSpan.FromSeconds(0),// 0 = advertise forever
                   (AdvertisingResult result) =>
                   {

                       Debug.Log("OnAdvertisingResult: " + result);
                   },
                      (ConnectionRequest request) =>
                      {

                          Debug.Log("Received connection request: " +
                           request.RemoteEndpoint.ServiceId + " " +
                           request.RemoteEndpoint.EndpointId + " " +
                           request.RemoteEndpoint.Name);

                      }
                   );

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
    


    internal void StartDiscovery()
    {
        

        PlayGamesPlatform.Nearby.StartDiscovery(
                        PlayGamesPlatform.Nearby.GetServiceId(),
                        TimeSpan.FromSeconds(0),
                        listener);
    }


    public void OnEndpointLost(string lostEndpointId)
    {
        Debug.Log("Endpoint lost: " + lostEndpointId);
    }
}
