using UnityEngine;
using UnityEngine.UI;

public class EndGameView : MonoBehaviour {

	[SerializeField]
	private GameObject _crosshair;

    [SerializeField]
    private Text _text;

	public bool IsOn {
		get {
			return _text.gameObject.activeInHierarchy;
		}
	}

	public void Toggle(bool isOn) {
		_crosshair.gameObject.SetActive(!isOn);
		_text.gameObject.SetActive(isOn);
	}
}