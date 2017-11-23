using UnityEngine;

public class DebugObject : MonoBehaviour {

	public Color _Color;
	
	public void OnDrawGizmos () {
		BoxCollider box = GetComponent <BoxCollider> ();
		Gizmos.color = _Color;
		Matrix4x4 rotationMatrix = Matrix4x4.TRS (transform.position, transform.rotation, transform.lossyScale);
		Gizmos.matrix = rotationMatrix; 
		Gizmos.DrawCube (Vector3.zero, box.size);
	}
}