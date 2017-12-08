using UnityEngine;

public class UIContainerView : MonoBehaviour {
	public void InitializeGlobals(Globals globals) {
		globals.notebookView = GetComponentInChildren<NotebookView>();
		globals.notebookAlertsView = GetComponentInChildren<NotebookAlertsView>();
		globals.sceneIntroView = GetComponentInChildren<SceneIntroView>();
		globals.throwPowerView = GetComponentInChildren<ThrowPowerView>();
		globals.actionHelperView = GetComponentInChildren<ActionHelperView>();
        globals.starView = GetComponentInChildren<StarView>();
		globals.exitExitView = GetComponentInChildren<ExitExitView>();
		globals.endGameView = GetComponentInChildren<EndGameView>();
	}
}