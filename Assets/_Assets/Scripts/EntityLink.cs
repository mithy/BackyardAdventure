using UnityEngine;

public class EntityLink : MonoBehaviour {

	private GameEntity _entity;
	public GameEntity Entity {
		get {
			return _entity;
		}
	}

	public void Link(GameEntity entity) {
		_entity = entity;
	}

	public void Unlink() {
		_entity = null;
	}
}