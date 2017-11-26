using UnityEngine;
using UnityEngine.UI;

public class NotebookView : MonoBehaviour {

	private const float ANIMATION_SPEED = 1500.0f;

	[SerializeField]
	private TextHelper _textHelper;

	[SerializeField]
	private Text _text;

	[SerializeField]
	private GameObject _background;

	private bool _isShown;

	private float _startTime;
	private float _travelLength;

	private Vector3 _initialPosition;
	private Vector3 _finalPositionHidden;
	private Vector3 _finalPositionShown;

	private void Awake() {
		RectTransform trans = GetComponent<RectTransform>();
		_finalPositionShown = new Vector3(trans.sizeDelta.x, 0, 0);
		_finalPositionHidden = new Vector3(0, 0, 0);
		_travelLength = 1;

		_background.SetActive(true);
	}
	
	private void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) {			_isShown = !_isShown;
			_startTime = Time.time;
			_travelLength = Vector3.Distance(transform.position, _isShown ? _finalPositionShown : _finalPositionHidden);
			_initialPosition = transform.position;
		}

		float distCovered = (Time.time - _startTime) * ANIMATION_SPEED;
		float fracJourney = distCovered / _travelLength;
		transform.position = Vector3.Lerp(_initialPosition, _isShown ? _finalPositionShown : _finalPositionHidden, fracJourney);
	}
}
