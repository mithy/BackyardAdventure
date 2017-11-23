using UnityEngine;

public class PlayerView : MonoBehaviour {

	private const float SCREEN_MIDDLE_POSITION = 0.5f;
	private const float MAXIMUM_INTERACT_DISTANCE = 10.0f;

	private void Update() {
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(SCREEN_MIDDLE_POSITION, SCREEN_MIDDLE_POSITION, 0));
		RaycastHit rayHit = new RaycastHit();

		if (Physics.Raycast(ray, out rayHit, MAXIMUM_INTERACT_DISTANCE)) {
			//Debug.Log(rayHit.collider.gameObject.name);
		}
	}
}