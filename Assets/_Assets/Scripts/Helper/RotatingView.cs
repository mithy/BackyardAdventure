using UnityEngine;

public class RotatingView : MonoBehaviour {

    [SerializeField]
    private Vector3 _axis;

    [SerializeField]
    private float _speed = 100.0f;

	private void Update() {
		transform.Rotate(_axis, Time.deltaTime * _speed);
	}
}