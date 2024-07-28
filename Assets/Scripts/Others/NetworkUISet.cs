using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUISet : MonoBehaviour
{
    [SerializeField] private Button startHost;
    [SerializeField] private Button startClient;
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        EventManager.Instance.InputEvent.HostStart();
    }
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        EventManager.Instance.InputEvent.ClientStart();
    }

}
