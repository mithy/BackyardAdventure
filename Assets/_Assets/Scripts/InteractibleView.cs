using UnityEngine;

public class InteractibleView : MonoBehaviour {

	private EntityLink _entityLink;
	public EntityLink EntityLink {
		get {
			return _entityLink;
		}
	}

	private Rigidbody _rigidbody;

	private void Awake() {
		_entityLink = GetComponent<EntityLink>();
		_rigidbody = GetComponent<Rigidbody>();
	}

	public void Throw(Vector3 forward, float force) {
		if (_rigidbody != null) {
			_rigidbody.AddForce(forward * force);
		}
	}

	public void TogglePickedUp(bool isPickedUp) {
		if (_rigidbody != null) {
			_rigidbody.useGravity = !isPickedUp;

			if (!isPickedUp) {
				_rigidbody.velocity = Vector3.zero;
			}
		}
	}
}