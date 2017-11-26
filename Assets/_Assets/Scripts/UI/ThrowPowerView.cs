using UnityEngine;
using UnityEngine.UI;

public class ThrowPowerView : MonoBehaviour {

	private const float FILL_DIFFERENCE = 0.5f;

	private Image _powerImage;

	private void Awake() {
		_powerImage = GetComponent<Image>();
		_powerImage.fillAmount = 0.0f;
	}

	public void SetPower(float power) {
		_powerImage.fillAmount = power;
	}
}