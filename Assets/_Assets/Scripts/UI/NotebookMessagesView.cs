using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotebookMessagesView : MonoBehaviour {

	private const float ANIMATION_SPEED = 150.0f;
	private const float MAX_WAIT_TIME = 3.0f;

	[SerializeField]
	private TextHelper _textHelper;

	private Text _text;
	private Image _image;
	private Queue<string> _messageQueue = new Queue<string>();

	private bool _isCurrentlyDisplayingAMessage;

	private float _startTime;
	private float _travelLength;

	private void Awake() {
		_image = GetComponent<Image>();
		_text = GetComponentInChildren<Text>();
	}

	private void Update() {
		if (!_isCurrentlyDisplayingAMessage && _messageQueue.Count > 0) {
			string message = _messageQueue.Dequeue();
			_isCurrentlyDisplayingAMessage = true;
			StartCoroutine(DisplayMessageCoroutine(message));
		}
	}

	public void DisplayMessage(string message) {
		_messageQueue.Enqueue(_textHelper.GetTranslation(message));
	}

	private IEnumerator DisplayMessageCoroutine(string message) {
		_text.text = message;
		Coroutine colorChange = StartCoroutine(AnimateToggle(true));
		yield return colorChange;

		yield return new WaitForSeconds(MAX_WAIT_TIME);

		colorChange = StartCoroutine(AnimateToggle(false));
		yield return colorChange;

		_text.text = string.Empty;

		_isCurrentlyDisplayingAMessage = false;
	}

	private IEnumerator AnimateToggle(bool isVisible) {
		Color start = isVisible ? new Color(1, 1, 1, 0) : Color.white;
		Color end = isVisible ? Color.white : new Color(1, 1, 1, 0);
		Color textColorStart = isVisible ? new Color(0, 0, 0, 0) : Color.black;
		Color textColorEnd = isVisible ? Color.black : new Color(0, 0, 0, 0);

		for (float i = 0; i <= 1; i += Time.deltaTime) {
			Color c = Color.Lerp(start, end, i);
			Color textColor = Color.Lerp(textColorStart, textColorEnd, i);
			_text.color = textColor;
			_image.color = c;

			yield return null;
		}
	}
}