using UnityEngine;
using UnityEngine.UI;

public class NotebookAlertsView : MonoBehaviour {

	private const float ANIMATION_SPEED = 10.0f;

    [SerializeField]
    private Color _normalColor;

    [SerializeField]
    private Color _alertColor;

    private Image _image;

    [SerializeField]
    private bool _isAlerting;

    private Color _startColor;
    private Color _endColor;
    private float _timeInterval;

	private void Awake() {
		_image = GetComponent<Image>();
	}

	private void Update() {
        if (_isAlerting) {
            _image.color = Color.Lerp(_startColor, _endColor, _timeInterval);
            _timeInterval += Time.deltaTime;

            if (_timeInterval >= 1) {
                Color auxiliaryColor = _startColor;
                _startColor = _endColor;
                _endColor = auxiliaryColor;
                _timeInterval = 0;
            }
        } else {
            _image.color = _normalColor;
        }
	}

    public void ToggleAlert(bool isOn) {
        // Set the colors only if the state is not triggered already.
        if (isOn && !_isAlerting) {
            _startColor = _normalColor;
            _endColor = _alertColor;
            _timeInterval = 0;
        }

        _isAlerting = isOn;       
    }
}