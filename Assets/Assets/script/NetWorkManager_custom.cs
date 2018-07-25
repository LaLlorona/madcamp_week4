using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetWorkManager_custom : NetworkManager { 

	public void StartupHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();//need to use singleton.StartHost()

    }
    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputFieldIPAdress").transform.Find("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;


    }
    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;

    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            SetupMenuSceneButton();
        }
        else
        {
            SetupOtherSceneButton();
        }
    }
    void SetupMenuSceneButton()
    {
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);
    }
    void SetupOtherSceneButton()
    {
        GameObject.Find("ButtonDisConnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisConnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);

    }
}
