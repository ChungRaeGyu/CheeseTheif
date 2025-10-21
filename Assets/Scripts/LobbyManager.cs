using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private Button createRoomBtn;
    [SerializeField] private Button joinRoomBtn;
    [SerializeField] private Button creditBtn;
    [SerializeField] private Button quitBtn;

   
    [Header("JoinPanel")]
    [SerializeField] private TMP_InputField roomText;
    [SerializeField] private GameObject joinPanel;
    [SerializeField] private Button joinPanelBtn;
    [SerializeField] private Button joinPanelQuitBtn;

    private void Start()
    {
        createRoomBtn.onClick.AddListener(NetworkManager.Instance.CreateRoom);
        joinRoomBtn.onClick.AddListener(() => {
            NetworkManager.Instance.Joinroom(roomText.text);
        });
        //createRoomBtn.onClick.AddListener(NetworkManager.Instance.CreateRoom);
        quitBtn.onClick.AddListener(NetworkManager.Instance.Quit);
        joinPanelBtn.onClick.AddListener(joinPanelControl);
        joinPanelQuitBtn.onClick.AddListener(joinPanelControl);
    }

    private void joinPanelControl()
    {
        joinPanel.SetActive(!joinPanel.activeSelf);
    }
}
