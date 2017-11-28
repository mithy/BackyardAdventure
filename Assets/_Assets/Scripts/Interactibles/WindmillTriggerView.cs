using UnityEngine;

public class WindmillTriggerView : MonoBehaviour {
    [SerializeField]
    private WindmillView _windmill;

    private void OnTriggerEnter(Collider other) {
        InteractibleView interactible2 = other.gameObject.GetComponent<InteractibleView>();
        if (interactible2 != null) {
            if (interactible2.Type == InteractibleTypesEnum.Gear1) {
                _windmill.TurnOnGear(1);
            }

            if (interactible2.Type == InteractibleTypesEnum.Gear2) {
                _windmill.TurnOnGear(2);
            }

            if (interactible2.Type == InteractibleTypesEnum.Gear3) {
                _windmill.TurnOnGear(3);
            }
        }
    }
}