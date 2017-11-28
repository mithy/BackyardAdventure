using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique, CreateAssetMenu]
public class TextHelper : ScriptableObject {

	private Dictionary<string, string> translations = new Dictionary<string, string>()
	{
		{ "Loading", "Loading" },

		{ "Star Collected", "Star Collected" },

		{ "DayOne", "DAY ONE" },
		{ "DayTwo", "DAY TWO" }
	};

	public string NotesUpdated {
		get {
			return GetTranslation("Notes Updated");
		}
	}

	public string Help {
		get {
			return "Welcome to the Backyard help!\n\nWSAD - to move\n\nLeft Click - Pick Up, interact\n\nRight Click - Drop, Throw, Stop\n\nSPACE - jump\n\nSHIFT - run\n\nTAB - show notepad";
		}
	}

	public void InitializeGlobals(Globals globals) {
		globals.textHelper = this;
	}

	public string GetTranslation(string text) {
		if (translations.ContainsKey(text)) {
			return translations[text];
		}

		return text;
	}
}