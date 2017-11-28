using UnityEngine;

public class GearView : MonoBehaviour {

    private const float MAXIMUM_LOW_LIMIT = -20;

    [SerializeField]
    private Transform _gear1;

    [SerializeField]
    private Transform _gear2;

    [SerializeField]
    private Transform _gear3;

    private Vector3 _gear1InitialPosition;
    private Vector3 _gear2InitialPosition;
    private Vector3 _gear3InitialPosition;

    private void Awake() {
        _gear1InitialPosition = _gear1.position;
        _gear2InitialPosition = _gear2.position;
        _gear3InitialPosition = _gear3.position;
    }

    private void Update() {
        // Preventing gears going thru terrain.
        if (_gear1 != null) {
            if (_gear1.position.y < MAXIMUM_LOW_LIMIT) {
                _gear1.position = _gear1InitialPosition;
                _gear1.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        if (_gear2 != null) {
            if (_gear2.position.y < MAXIMUM_LOW_LIMIT) {
                _gear2.position = _gear2InitialPosition;
                _gear2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        if (_gear3 != null) {
            if (_gear3.position.y < MAXIMUM_LOW_LIMIT) {
                _gear3.position = _gear3InitialPosition;
                _gear3.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}