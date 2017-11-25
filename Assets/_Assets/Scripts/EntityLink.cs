using UnityEngine;

public class EntityLink : MonoBehaviour {

	private GameEntity _entity;
	public GameEntity Entity {
		get {
			return _entity;
		}
	}

	public string UUID {
		get {
			if (_entity == null) {
				return string.Empty;
			}

			return _entity.indexableEntity.uuid;
		}
	}

	public void Link(GameEntity entity) {
		_entity = entity;
	}

	public void Unlink() {
		_entity = null;
	}
}