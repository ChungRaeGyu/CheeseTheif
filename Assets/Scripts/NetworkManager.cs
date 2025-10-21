using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using WebSocketSharp;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public static NetworkManager Instance;
    bool reStart = false;
    string roomName = "";
    

    public void Quit()
    {
        Application.Quit();
    }
    public void DisConnect()
    {
        PhotonNetwork.Disconnect();
    }
    public void OnApplicationQuit()
    {
        GotoLobby();
        DisConnect();
    }
    public void ReJoindRoom(string name)
    {
        PhotonNetwork.LeaveRoom();
        reStart = true;
        roomName = name;

        //���� ������ OnJoinedLobby���� �ٽ� JoinRoom�� �õ�
        //OnLeftRoom���� �ٷ� JoinRoom�� �õ��ϸ� �ȵ�
    }
    public void GotoLobby()
    {
        reStart = false;
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        JoinLobby();
    }
    public void ConnectBtn()
    {
        //���۾� ��ư
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        //if (PhotonNetwork.LocalPlayer.NickName.IsNullOrEmpty())
        //{
        //    PhotonNetwork.LocalPlayer.NickName = PlayerData.Instance.GetNickName();
        //}
        JoinLobby();
    }
    public void JoinLobby() => PhotonNetwork.JoinLobby();
    public override void OnJoinedLobby()
    {
        if (reStart)
        {
            Debug.Log("���⿩�⿩���⶧���ΰ�??");
            PhotonNetwork.JoinRoom(roomName);
            return;
        }
        PhotonNetwork.LoadLevel("Lobby");

    }

    public void QuickMatchBtn()
    {
        //����ٰ� ������ �޸� �̰� mmr��Ī�̳� �̷��� �ȴ�.
        PhotonNetwork.JoinRandomRoom();
    }

    public void Joinroom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("Room");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }
    public void CreateRoom()
    {
        string roomName = "Room" + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = 8; //�ִ��ο�
        roomOptions.EmptyRoomTtl = 5;
        roomOptions.CleanupCacheOnLeave = true;
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //        Screen.SetResolution(540, 960, false);
        Application.runInBackground = true; // ��Ŀ�� �Ҿ ��Ʈ��ũ ������ ����
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;
    }
}
