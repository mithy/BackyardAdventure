using UnityEngine;

public class PlayerView : MonoBehaviour {

	private const float SCREEN_MIDDLE_POSITION = 0.5f;
	private const float MAXIMUM_INTERACT_DISTANCE = 10.0f;

	private EntityLink _entityLink;
	public EntityLink EntityLink {
		get {
			return _entityLink;
		}
	}

	[SerializeField]
	private Transform _targetPick;
	public Transform TargetPick {
		get {
			return _targetPick;
		}
	}

	public Vector3 ForwardDirection {
		get {
			return Camera.main.transform.forward;
		}
	}

	private void Awake() {
		_entityLink = GetComponent<EntityLink>();	}

	private void Update() {
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(SCREEN_MIDDLE_POSITION, SCREEN_MIDDLE_POSITION, 0));
		RaycastHit rayHit = new RaycastHit();

		if (Physics.Raycast(ray, out rayHit, MAXIMUM_INTERACT_DISTANCE)) {
			InteractibleView interact = rayHit.collider.gameObject.GetComponent<InteractibleView>();

			if (interact != null) {
				_entityLink.Entity.ReplacePlayerClickInput(interact.EntityLink.Entity);
			}
		}
	}
}