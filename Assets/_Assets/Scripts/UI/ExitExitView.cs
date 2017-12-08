using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitExitView : MonoBehaviour {

	private Text _exitText;	

	public bool IsActive {
		get {
			return _exitText.gameObject.activeInHierarchy;
		}
	}

	private void Awake() {
		_exitText = GetComponentInChildren<Text>();
		_exitText.gameObject.SetActive(false);
	}

	public void ToggleVisibility(bool isOn) {
		_exitText.gameObject.SetActive(isOn);
	}	
}