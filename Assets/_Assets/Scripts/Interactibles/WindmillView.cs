using UnityEngine;

public class WindmillView : MonoBehaviour {

    public float PALET_SPEED = 20.0f;
    public float GEAR_SPEED = 5.0f;

    [SerializeField]
    private Transform _gear1;

    [SerializeField]
    private Transform _gear2;

    [SerializeField]
    private Transform _gear3;

    [SerializeField]
    private Transform _palets;

    public bool IsOperational {
        get {
            return _gear1.gameObject.activeInHierarchy &&
                _gear2.gameObject.activeInHierarchy &&
                _gear3.gameObject.activeInHierarchy;
        }
    }

    private void Update() {
        if (IsOperational) {
            _gear1.transform.Rotate(0, 0, GEAR_SPEED * Time.deltaTime);
            _gear2.transform.Rotate(0, 0, -GEAR_SPEED * Time.deltaTime);
            _gear3.transform.Rotate(0, 0, GEAR_SPEED * Time.deltaTime);
            _palets.transform.Rotate(0, 0, PALET_SPEED * Time.deltaTime);
        }
    }

    public void TurnOnGear(int index) {
        switch (index) {
            case 1:
                _gear1.gameObject.SetActive(true);
                break;

            case 2:
                _gear2.gameObject.SetActive(true);
                break;

            case 3:
                _gear3.gameObject.SetActive(true);
                break;
        }
    }

}