using UnityEngine;
using UnityEngine.UI;

public class ShapeController : MonoBehaviour
{
    private float _timer;
    private bool _negative = false;

    private void Start()
    {
        var tempRand = Random.Range(0, 10);
        var image = GetComponent<Image>();

        if (tempRand > 7)
        {
            image.color = Color.red;
            _negative = true;
        }
        else
        {
            image.color = GetRandomColor();
        }
    }

    private Color32 GetRandomColor()
    {
        var r = (byte) Random.Range(0, 200);
        var g = (byte) Random.Range(0, 255);
        var b = (byte) Random.Range(0, 255);
        return new Color32(r, g, b, 255);
    }

    public void OnClick()
    {
        var _currentPoint = (_negative)
            ? --PointsController.Instance.CurrentPoints
            : ++PointsController.Instance.CurrentPoints;
        Hud.Instance.ResultText.text = _currentPoint.ToString();
        Destroy(gameObject);
    }
}