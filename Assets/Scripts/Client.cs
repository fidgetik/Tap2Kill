using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Client : MonoBehaviour
{
    [SerializeField] private Text _downloaderStatus;
    private string _serverAddress = "http://www.mocky.io/v2/5cea9000330000761b7c3857";   
    
    private void Start()
    {
        StartCoroutine(LoadFromServer(_serverAddress));
    }


    IEnumerator LoadFromServer(string url)
    {
        var request =  UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            _downloaderStatus.text = "Something wrong!";
            throw new Exception("ERROR!");
        }
        _downloaderStatus.text = "Download data is ready!\r\n"+ request.downloadHandler.text;
    }
}
