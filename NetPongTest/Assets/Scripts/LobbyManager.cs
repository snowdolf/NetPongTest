using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "1";

    public TMP_Text connectionInfoText;
    public Button joinButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        connectionInfoText.text = "Connecting to Master Server...";
        joinButton.interactable = false;
    }

    public override void OnConnectedToMaster()
    {
        connectionInfoText.text = "Connected to Master Server";
        joinButton.interactable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectionInfoText.text = $"Disconnected from server: {cause} - Try reconnecting...";
        joinButton.interactable = false;

        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        { 
            connectionInfoText.text = "Connecting to Random Room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = $"Disconnected from server - Try reconnecting...";

            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "No available rooms - Creating a new room...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "Joined Room - Starting Game...";
        PhotonNetwork.LoadLevel("Main");
    }
}
