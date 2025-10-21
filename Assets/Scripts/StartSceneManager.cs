using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TMP_InputField nickNameInput;
    [SerializeField] private Button startBtn;

    void Start()
    {
        startBtn.onClick.AddListener(GameStart);
    }
    private void GameStart()
    {
        if (nickNameInput.text.Length < 1)
        {
            Debug.Log("닉네임을 입력하세요~");
            //나중에 알림으로 다 바꿔줘야함
        }
        else
        {
            PhotonNetwork.NickName = nickNameInput.text;
            NetworkManager.Instance.ConnectBtn();
        }
    }
}
