using UnityEngine;
using UnityEngine.UI;

public class StarView : MonoBehaviour {

    private Text _amount;

	private void Awake() {
        _amount = GetComponentInChildren<Text>();
        _amount.text = "0 / 21";
	}

    public void UpdateAmount(int newAmount) {
        _amount.text = newAmount + " / " + 21;

        if (newAmount == 21) {
            _amount.text = "Double Jump Unlocked";
        }
    }
}
