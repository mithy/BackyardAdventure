using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndGameView : MonoBehaviour {

	[SerializeField]
	private GameObject _crosshair;

    [SerializeField]
    private Text _text;

    [SerializeField]
    private GameObject _sun;

    private Color _startColor = Color.white;
    private Color _endColor = new Color(1, 1, 1, 0);
    private float _timeColor;

    public void Toggle(bool isOn) {
		_crosshair.gameObject.SetActive(!isOn);
		_text.gameObject.SetActive(isOn);
        _sun.SetActive(isOn);

        if (isOn) {
            _timeColor = 0;
            _text.color = Color.white;
        }
	}

    private void Update() {
        if (_timeColor <= 1) {
            _timeColor += Time.deltaTime;
            _text.color = Color.Lerp(_startColor, _endColor, _timeColor);
        }
    }
}