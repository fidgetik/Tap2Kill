using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private GameObject _resultWindow;
    [SerializeField] private Text _resultDescription;
    [SerializeField] private Text _resultText;
    [SerializeField] private Text _countDown;
    [SerializeField] private Button _pauseButton;
    
    private static Hud _instance;

    #region Properties
    public static Hud Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public Text ResultText
    {
        get { return _resultText; }
        set
        {
            _resultText = value;
        }
    }

    public Button PauseButton
    {
        get { return _pauseButton; }
        set { _pauseButton = value; }
    }

    public Text ResultDescription
    {
        get { return _resultDescription; }
        set { _resultDescription = value; }
    }

    public GameObject ResultWindow
    {
        get { return _resultWindow; }
        set { _resultWindow = value; }
    }
    

    #endregion
  
    private void Awake()
    {
        _instance = this;
    }

    public void SetTimerTime(float time)
    {
        _countDown.text = time.ToString("##");
    }
    
}
