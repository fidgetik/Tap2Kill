using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum GameState
{
    Pause,
    Play
}

public class GameController : MonoBehaviour
{
    
    #region Variables
    [SerializeField] private GraphicRaycaster _gameControllerRayCaster;
    [SerializeField] private GameObject[] _prefabsOfShapes;
    [SerializeField] private GameObject _shapesFolder;
    
    [Header("Shape Controller Time")] [SerializeField]
    private float _maximumLifeTime;
    [SerializeField] private float _minimumLifeTime;
    [SerializeField] private float _maximumBornTime;
    [SerializeField] private float _minimumBornTime;

    private PointsController _pointsController;
    private Hud _hud;

    private GameState _currentState;
    private GameObject _shape;
    
    private bool _isPause = false;
    private float _countDownTimer;
    private float _creatorTimer;
    
    private static GameController _instance;
    private const float TimeForGame = 60f;

    #endregion

    #region Properties

    public static GameController Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public float CountDownTimer
    {
        get { return _countDownTimer; }
        set
        {
            _hud.SetTimerTime(_countDownTimer);
            _countDownTimer = value;
        }
    }

    #endregion

    private void Awake()
    {
        _instance = this;
        _hud = Hud.Instance;
        _currentState = GameState.Play;
        _countDownTimer = TimeForGame;
        _creatorTimer = 0f;
    }

    private void Start()
    {
        _pointsController = PointsController.Instance;
    }

    private void Update()
    {
        TimeController();
        PauseController();
        StartCoroutine(ShapeGenerator());
    }

    private void PauseController()
    {
        if (_currentState == GameState.Pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void TimeController()
    {
        CountDownTimer -= Time.deltaTime;
        if (CountDownTimer < 0)
        {
            GameOver();
            CountDownTimer = TimeForGame;
        }
    }

    private void GameOver()
    {
        var resultWin = _pointsController.IsItMaximum() ? true : false;
        _currentState = GameState.Pause;
        _gameControllerRayCaster.enabled = false;
        _hud.ResultWindow.SetActive(true);
        _hud.PauseButton.interactable = false;
        if (resultWin)
        {
            _hud.ResultDescription.text = "Congratulation!\r\nYou got high score: " + _pointsController.CurrentPoints;
        }
        else
        {
            _hud.ResultDescription.text = "You lose!\r\nYou got: " + _pointsController.CurrentPoints + "\r\nHigh score: " + _pointsController.MaximumPoints;
        }
    }
    
    
    private int GetRandomImage()
    {
        return Random.Range(0, _prefabsOfShapes.Length);
        ;
    }

    private Vector2 GetRandomPosition()
    {
        var randomX = Random.Range(30, transform.position.x * 2 - 30f);
        var randomY = Random.Range(30, transform.position.y * 2 - 150f);
        return new Vector2(randomX, randomY);
    }

    public float GetRandomTime(bool forDestroy)
    {
        if (!forDestroy) return Random.Range(_minimumBornTime, _maximumBornTime);
        else return Random.Range(_minimumLifeTime, _maximumLifeTime);
    }

    private IEnumerator ShapeGenerator()
    {
        _creatorTimer += Time.deltaTime;
        if (_creatorTimer > GetRandomTime(false))
        {
            var shape = Instantiate(_prefabsOfShapes[GetRandomImage()], GetRandomPosition(), Quaternion.identity,
                _shapesFolder.transform);
            Destroy(shape, GetRandomTime(true));
            _creatorTimer = 0;
        }

        yield return null;
    }

    public void Pause(GameObject window)
    {
        if (_isPause)
        {
            window.SetActive(false);
            _currentState = GameState.Play;
        }
        else
        {
            window.SetActive(true);
            _currentState = GameState.Pause;
        }
        _gameControllerRayCaster.enabled = _isPause;
        _isPause = !_isPause;
    }

    public void Restart(GameObject window)
    {
        window.SetActive(false);
        _isPause = false;
        _gameControllerRayCaster.enabled = true;
        _hud.PauseButton.interactable = true;
        var shapes = FindObjectsOfType<ShapeController>();
        foreach (var shape in shapes)
        {
            Destroy(shape.gameObject);
        }

        _hud.ResultText.text = 0.ToString();
        _pointsController.CurrentPoints = 0;
        _countDownTimer = TimeForGame;
        _currentState = GameState.Play;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}