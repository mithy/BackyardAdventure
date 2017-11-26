using UnityEngine;
using Entitas.CodeGeneration.Attributes;

[Game, Unique, CreateAssetMenu]
public class TextHelper : ScriptableObject {

	public string Loading {
		get {
			return "Loading";
		}
	}

	public string GetTitle(LevelsEnum level) {
		switch (level) {
			case LevelsEnum.Level1:
				return "DAY ONE";

			case LevelsEnum.Level2:
				return "DAY TWO";
		}

		return string.Empty;
	}
}