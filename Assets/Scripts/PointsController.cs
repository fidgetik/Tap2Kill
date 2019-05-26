using UnityEngine;

public class PointsController : MonoBehaviour
{
    [HideInInspector] public int CurrentPoints;
    private static PointsController _instance;
    public int MaximumPoints;

    public static PointsController Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
    
    private void Awake()
    {
        _instance = this;
        MaximumPoints = PlayerPrefs.GetInt("Points");
    }

    public bool IsItMaximum()
    {
        if (MaximumPoints <= CurrentPoints)
        {
            MaximumPoints = CurrentPoints;
            PlayerPrefs.SetInt("Points", CurrentPoints);
            return true;
        }
        else return false;
    }
}
