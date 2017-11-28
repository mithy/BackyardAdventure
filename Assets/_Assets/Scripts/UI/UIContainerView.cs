using UnityEngine;

public class UIContainerView : MonoBehaviour {
	public void InitializeGlobals(Globals globals) {
		globals.notebookView = GetComponentInChildren<NotebookView>();
		globals.notebookMessagesView = GetComponentInChildren<NotebookMessagesView>();
		globals.sceneIntroView = GetComponentInChildren<SceneIntroView>();
		globals.throwPowerView = GetComponentInChildren<ThrowPowerView>();
		globals.actionHelperView = GetComponentInChildren<ActionHelperView>();
		globals.endGameView = GetComponentInChildren<EndGameView>();
	}
}