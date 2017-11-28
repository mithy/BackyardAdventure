using UnityEngine;

public class GeneratorTriggerView : MonoBehaviour {

    [SerializeField]
    private BrokenLampView _brokenLamp;

    private void OnTriggerEnter(Collider other) {
        InteractibleView interactible2 = other.gameObject.GetComponent<InteractibleView>();
        if (interactible2 != null && interactible2.Type == InteractibleTypesEnum.Generator) {
            _brokenLamp.FixIt();
        }
    }
}