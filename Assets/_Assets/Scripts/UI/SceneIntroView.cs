using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneIntroView : MonoBehaviour {

	private const float FADE_TIME = 0.05f;

	[SerializeField]
	private TextHelper _textHelper;

	[SerializeField]
	private Text _titleText;

	[SerializeField]
	private GameObject _crosshair;

	private Image _backgroundImage;

	private void Awake() {
		_backgroundImage = GetComponent<Image>();
	}

	public void Activate(LevelsEnum level) {
		_titleText.text = _textHelper.GetTitle(level);
		_backgroundImage.color = Color.black;
		_crosshair.SetActive(false);
		StartCoroutine(ActivateCoroutine());
	}

	public void FadeIn() {
		StartCoroutine(FadeInCoroutine());	}

	private IEnumerator ActivateCoroutine() {
		Color c = Color.black;

		while (c.a > 0) {
			c.a -= 0.01f;
			_backgroundImage.color = c;

			yield return new WaitForSeconds(FADE_TIME);
		}

		yield return new WaitForSeconds(0.5f);
		_titleText.text = string.Empty;
		_crosshair.SetActive(true);	}

	private IEnumerator FadeInCoroutine() {
		Color c = new Color(0, 0, 0, 1);

		_titleText.text = _textHelper.Loading;

		while (c.a <= 1) {
			c.a += 0.01f;
			_backgroundImage.color = c;

			yield return new WaitForSeconds(FADE_TIME);
		}
	}
}