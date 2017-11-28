using UnityEngine;

public class InteractibleView : MonoBehaviour {

	private const string CONTAINER_TAG = "Container";

	public InteractionTypesEnum InteractionType;
	public InteractibleTypesEnum Type;
	public bool IsCollectable;

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

	public void MoveObject(Vector3 direction, float force) {
		if (_rigidbody != null) {
			_rigidbody.AddForce(direction * force);
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
		if (InteractionType == InteractionTypesEnum.Pickable && IsCollectable) {
			if (other.gameObject.tag == CONTAINER_TAG && _entityLink != null) {
				if (!_entityLink.Entity.hasContainedObject) {
					_entityLink.Entity.AddContainedObject(other.gameObject);
				}
			}
		}

		if (Type == InteractibleTypesEnum.BasketballHoopTop) {
			_entityLink.Entity.AddBasketHoopTrigger(true);
		}

		if (Type == InteractibleTypesEnum.BasketballHoopBottom) {
			_entityLink.Entity.AddBasketHoopTrigger(false);
		}

        if (Type == InteractibleTypesEnum.GeneratorZone) {
            InteractibleView interactible2 = other.gameObject.GetComponent<InteractibleView>();
            if (interactible2 != null && interactible2.Type == InteractibleTypesEnum.Generator) {
                _entityLink.Entity.AddEventTrigger(EventsEnum.GeneratorTriggered, -1);
            }
        }

        if (Type == InteractibleTypesEnum.WindmillZone) {
            InteractibleView interactible2 = other.gameObject.GetComponent<InteractibleView>();
            if (interactible2 != null) {
                bool isOkToDelete = false;

                if (interactible2.Type == InteractibleTypesEnum.Gear1) {
                    _entityLink.Entity.AddEventTrigger(EventsEnum.Gear1Ready, -1);
                    isOkToDelete = true;
                }

                if (interactible2.Type == InteractibleTypesEnum.Gear2) {
                    _entityLink.Entity.AddEventTrigger(EventsEnum.Gear2Ready, -1);
                    isOkToDelete = true;
                }

                if (interactible2.Type == InteractibleTypesEnum.Gear3) {
                    _entityLink.Entity.AddEventTrigger(EventsEnum.Gear3Ready, -1);
                    isOkToDelete = true;
                }

                if (isOkToDelete) {
                    interactible2.EntityLink.Entity.Destroy();
                    Destroy(interactible2.gameObject);
                }
            }
        }
	}
}