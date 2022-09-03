using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;
public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    // Start is called before the first frame update
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text ErrorMsg;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform RoomListContent;
    [SerializeField] GameObject RoomListItemPrefab;
    [SerializeField] Transform PlayerListContent;
    [SerializeField] GameObject PlayerListItemPrefab;
    [SerializeField] GameObject StartGameButton;
    [SerializeField] Menu LoadingMenu, RoomMenu, ErrorMenu, LobbyMenu;

    public int LevelScene = 1;
    void Awake(){
        Instance = this;
    }
    void Start()
    {
        // untuk terhubung ke server photon
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        // join lobby untuk membuat room lobby
        PhotonNetwork.JoinLobby();
        // digunakan ketika semua player join room, scene dari semua player akan di synkronkan(semua scene sama)
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        PhotonNetwork.NickName = "Player" + Random.Range(0,1000).ToString("0000");
    }
    public void CreateRoom(){
        if(string.IsNullOrEmpty(roomNameInputField.text)){
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu(LoadingMenu);
    }
    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu(RoomMenu);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        Player[] players = PhotonNetwork.PlayerList;
        
		foreach(Transform child in PlayerListContent)
		{
			Destroy(child.gameObject);
		}

		for(int i = 0; i < players.Count(); i++)
		{
			Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
		}
        StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ErrorMsg.text = "Gagal membuat room: " + message;
        Debug.Log("Gagal membuat room: " + message);
        MenuManager.Instance.OpenMenu(ErrorMenu);
    }
    public void JoinRoom(RoomInfo info){
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu(LoadingMenu);
    }
    public void JoinRoomSolo(string scene){
        SceneManager.LoadScene(scene);
        // const string glyphs= "abcdefghijklmnopqrstuvwxyz0123456789"; //add the characters you want
        // int charAmount = Random.Range(0, 30); //set those to the minimum and maximum length of your string
        // string myString = "";
        // for(int i=0; i<charAmount; i++)
        // {
        //     myString += glyphs[Random.Range(0, glyphs.Length)];
        // }
        // Debug.Log("room name="+myString);
        // PhotonNetwork.JoinRoom(myString);
        // MenuManager.Instance.OpenMenu(LoadingMenu);
    }
    public void LeaveRoom(){
        PhotonNetwork.LeaveRoom();
        // VoiceChatManager.Instance.LeaveChannel();
        MenuManager.Instance.OpenMenu(LoadingMenu);
    }
    public void LeaveRoomToLobby(){
        PhotonNetwork.LeaveRoom();
        
        SceneManager.LoadScene("menu");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        StartCoroutine(MainReconnect());  
    }
    private IEnumerator MainReconnect()
    {
        while (PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState != ExitGames.Client.Photon.PeerStateValue.Disconnected)
        {
            Debug.Log("Waiting for client to be fully disconnected..", this);

            yield return new WaitForSeconds(0.2f);
        }

        Debug.Log("Client is disconnected!", this);

        if (!PhotonNetwork.ReconnectAndRejoin())
        {
            if (PhotonNetwork.Reconnect())
            {
                Debug.Log("Successful reconnected!", this);
            }
        }
        else
        {
            Debug.Log("Successful reconnected and joined!", this);
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("left room");
        MenuManager.Instance.OpenMenu(LobbyMenu);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{   
        foreach (Transform trans in RoomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if(roomList[i].RemovedFromList)
				continue;
            Instantiate(RoomListItemPrefab,RoomListContent).GetComponent<RoomListItem>().Setup(roomList[i]);
        }
    }
    // ketika master room keluar dan ganti hak master room, start game button akan diberikan ke player lain
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab,PlayerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
    public void setSceneLevel(int levelScene){
        this.LevelScene = levelScene;
    }

    public void StartGame(){
        MenuManager.Instance.OpenMenu(LoadingMenu);
        PhotonNetwork.LoadLevel(this.LevelScene);

    }
}
