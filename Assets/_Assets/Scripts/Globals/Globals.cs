using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique, CreateAssetMenu]
public class Globals : ScriptableObject {

	public GameEntity player;

	public TextHelper textHelper;
	public MissionsDB missions;

    public FirstPersonController fpsController;

	public NotebookView notebookView;
	public NotebookAlertsView notebookAlertsView;
	public SceneIntroView sceneIntroView;
	public ThrowPowerView throwPowerView;
	public ActionHelperView actionHelperView;
    public StarView starView;
	public EndGameView endGameView;
	public ExitExitView exitExitView;

	public string GetSceneForLevel(LevelsEnum level) {
		switch (level) {
			case LevelsEnum.DayOne:
				return "scene_day_1";

			case LevelsEnum.DayTwo:

				return "scene_day_2";

			case LevelsEnum.DayThree:
				return "scene_day_3";

            case LevelsEnum.DayFour:
                return "scene_day_4";

            case LevelsEnum.DayFive:
                return "scene_day_5";
		}

		return string.Empty;
	}
}