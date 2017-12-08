using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCameraView : MonoBehaviour {

    [SerializeField]
    private Color _day3Color;

    [SerializeField]
    private Color _day4Color;

    [SerializeField]
    private Color _day5Color;

    private void Update() {
        if (SceneManager.GetActiveScene().name == "scene_day_3") {
            Camera.main.backgroundColor = _day3Color;
        }

        if (SceneManager.GetActiveScene().name == "scene_day_4") {
            Camera.main.backgroundColor = _day4Color;
        }

        if (SceneManager.GetActiveScene().name == "scene_day_5") {
            Camera.main.backgroundColor = _day5Color;
        }
    }
}
