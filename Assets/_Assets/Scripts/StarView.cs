using UnityEngine;

public class StarView : MonoBehaviour {

	private const float ROTATION_SPEED = 100.0f;

	[SerializeField]
	private string UUID;
	
	private void Update() {
		transform.Rotate(0, ROTATION_SPEED * Time.deltaTime, 0);
	}
}