using Entitas;
using UnityEngine;

[Game]
public class PickedUpComponent : IComponent {
	public float InitialTime;
	public Vector3 InitialPosition;
	public float TotalTravelDistance;
}