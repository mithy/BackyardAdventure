using UnityEngine;

public class InteractibleView : MonoBehaviour {

	private const string CONTAINER_TAG = "Container";

	public InteractionTypesEnum InteractionType;

	private EntityLink _entityLink;
	public EntityLink EntityLink {
		get {
			return _entityLink;
		}
	}

	private bool _isPickedUp;
	public bool IsPickedUp {
		get {
			return _isPickedUp;
		}
	}

	private Transform _initialParent;
	private Rigidbody _rigidbody;
	private Collider _collider;

	private void Awake() {
		_initialParent = transform.parent;
		_entityLink = GetComponent<EntityLink>();
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();
	}

	public void Throw(Vector3 forward, float force) {
		if (_rigidbody != null) {
			
			_rigidbody.AddForce(forward * force);
		}
	}

	public void TogglePickedUp(bool isPickedUp) {
		_isPickedUp = isPickedUp;

		if (!isPickedUp) {
			_rigidbody.velocity = Vector3.zero;
		}

		TogglePhysics(isPickedUp);
	}

	public void ToggleContainer(GameObject container) {
		transform.SetParent(container != null ? container.transform : _initialParent);
        TogglePhysics(container != null);
	}

	private void TogglePhysics(bool isPhysicsOff) {
		if (_rigidbody != null) {
			_rigidbody.useGravity = !isPhysicsOff;
			_rigidbody.isKinematic = isPhysicsOff;
		}

		if (_collider != null) {
			_collider.isTrigger = isPhysicsOff;
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == CONTAINER_TAG && _entityLink != null) {
			if (!_entityLink.Entity.hasContainedObject) {
				_entityLink.Entity.AddContainedObject(other.gameObject);
			}
		}
	}
}