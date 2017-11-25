using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique, CreateAssetMenu]
public class Globals : ScriptableObject {

	public GameEntity player;

	public string GetSceneForLevel(LevelsEnum level) {
		switch (level) {
			case LevelsEnum.Level1:
				return "scene_level_1";
		}

		return string.Empty;
	}
}