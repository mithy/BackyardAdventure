using UnityEngine;

public class ActionHelperView : MonoBehaviour {

	[SerializeField]
	private GameObject _move;

	[SerializeField]
	private GameObject _pickup;

	public void ToggleMove() {
		_move.SetActive(true);
	}

	public void TogglePickup() {
		_pickup.SetActive(true);
	}

	public void HideAll() {
		_move.SetActive(false);
		_pickup.SetActive(false);
	}
}