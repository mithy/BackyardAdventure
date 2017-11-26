using UnityEngine;

public class UIContainerView : MonoBehaviour {
	public void InitializeGlobals(Globals globals) {
		globals.notebookView = GetComponentInChildren<NotebookView>();
		globals.sceneIntro = GetComponentInChildren<SceneIntroView>();
		globals.throwPower = GetComponentInChildren<ThrowPowerView>();
	}
}