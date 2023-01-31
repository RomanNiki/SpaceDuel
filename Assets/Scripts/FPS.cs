using UnityEngine;

public sealed class FPS : MonoBehaviour
{
    [SerializeField] private float _updateInterval = 0.5f;

    private float _accum;
    private int _frames;
    private float _timeLeft;
    private float _framePerSec;

    private readonly GUIStyle _textStyle = new();

    private void Start()
    {
        _timeLeft = _updateInterval;

        _textStyle.fontStyle = FontStyle.Bold;
        _textStyle.normal.textColor = Color.white;
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;
        _accum += Time.timeScale / Time.deltaTime;
        ++_frames;

        if (_timeLeft > 0.0) return;
        _framePerSec = (_accum / _frames);
        _timeLeft = _updateInterval;
        _accum = 0.0f;
        _frames = 0;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(5, 5, 100, 25), _framePerSec.ToString("F2") + "FPS", _textStyle);
    }
}