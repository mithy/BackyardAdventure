using UnityEngine;

public class BasketballView : MonoBehaviour {

	private const float MAXIMUM_DISTANCE = 30.0f;

	[SerializeField]
	private Transform _hoop;

	[SerializeField]
	private Transform _ball;

	private Vector3 _initialBallPosition;
	private Rigidbody _ballRigidbody;

	private void Awake() {
		_initialBallPosition = _ball.transform.position;
		_ballRigidbody = _ball.GetComponent<Rigidbody>();
	}

	private void Update() {
		if (Vector3.Distance(_hoop.transform.position, _ball.transform.position) > MAXIMUM_DISTANCE) {
			_ball.transform.position = _initialBallPosition;
			_ballRigidbody.velocity = Vector3.zero;
		}
	}
}