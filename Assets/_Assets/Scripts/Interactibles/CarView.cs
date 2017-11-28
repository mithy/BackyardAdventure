using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarView : MonoBehaviour {

    [SerializeField]
    private List<GameObject> _lights = new List<GameObject>();

    private void Update() {
        if (SceneManager.GetActiveScene().name == "scene_day_3") {
            ToggleLights(true);
        } else {
            ToggleLights(false);
        }
    }

    private void ToggleLights(bool areOn) {
        foreach (GameObject g in _lights) {
            g.SetActive(areOn);
        }
    }
}