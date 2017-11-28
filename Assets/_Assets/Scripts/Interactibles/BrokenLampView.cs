using UnityEngine;

public class BrokenLampView : MonoBehaviour {

    [SerializeField]
    private GameObject _dayLamp;

    [SerializeField]
    private GameObject _nightLamp;

	public void FixIt() {
        _dayLamp.SetActive(false);
        _nightLamp.SetActive(true);
    }
}