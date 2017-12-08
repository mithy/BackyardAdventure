using UnityEngine;

public class RotatingSunView : MonoBehaviour {

    [SerializeField]
    private float _rotateSpeed;

	private void Update () {
        transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);
	}
}
