using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;

[Game, Unique, CreateAssetMenu]
public class MissionsDB : ScriptableObject {

	public struct Mission {
		public string Text;
		public string FinishText;
		public int Index;
		public bool IsDiscoverable;
		public bool IsObjective;
	}

	private Dictionary<EventsEnum, Mission> _dayOneEvents = new Dictionary<EventsEnum, Mission>()
	{
		{ EventsEnum.DayStarted, new Mission() { Index = 0, IsDiscoverable = false, IsObjective = false, Text = "Harvest day" } },
		{ EventsEnum.FruitHovered, new Mission() { Index = 0, IsDiscoverable = false, IsObjective = false, Text = string.Empty } },
		{ EventsEnum.FruitInCrate, new Mission() {Index = 5, IsDiscoverable = false, IsObjective = true, Text = "Collect fruits", FinishText = "Crate ready for delivery" } },
		{ EventsEnum.FruitPicked, new Mission() { Index = 0, IsDiscoverable = true, IsObjective = false, Text = "Hmm.. a fruit? What to do with it." } },
		{ EventsEnum.CratePicked, new Mission() { Index = 0, IsDiscoverable = true, IsObjective = false, Text = "Empty crate? Too many things lying around." } }
	};

	private Dictionary<EventsEnum, Mission> _dayTwoEvents = new Dictionary<EventsEnum, Mission>()
	{
		{ EventsEnum.DayStarted, new Mission() { Index = 0, IsDiscoverable = false, IsObjective = false, Text = "1,2,3 Basket!" } },
		{ EventsEnum.BasketballHoop, new Mission() { Index = 3, IsDiscoverable = true, IsObjective = true, Text = "Throw 3 correct shots at basket", FinishText = "Well done" } }
	};

    private Dictionary<EventsEnum, Mission> _dayThreeEvents = new Dictionary<EventsEnum, Mission>()
    {
        { EventsEnum.DayStarted, new Mission() { Index = 0, IsDiscoverable = false, IsObjective = false, Text = "The sad, broken street lamp" } },
        { EventsEnum.GeneratorTriggered, new Mission() { Index = 1, IsDiscoverable = true, IsObjective = true, Text = "Use generator to power the street lamp", FinishText = "Well done" } }
    };

    private Dictionary<EventsEnum, Mission> _dayFourEvents = new Dictionary<EventsEnum, Mission>()
    {
        { EventsEnum.DayStarted, new Mission() { Index = 0, IsDiscoverable = false, IsObjective = false, Text = "Windy Windmill" } },
        { EventsEnum.Gear1Ready, new Mission() { Index = 1, IsDiscoverable = true, IsObjective = true, Text = "Find First Gear", FinishText = "Well done" } },
        { EventsEnum.Gear2Ready, new Mission() { Index = 1, IsDiscoverable = true, IsObjective = true, Text = "Find Second Gear", FinishText = "Well done" } },
        { EventsEnum.Gear3Ready, new Mission() { Index = 1, IsDiscoverable = true, IsObjective = true, Text = "Find Third Gear", FinishText = "Well done" } }
    };

    private Dictionary<EventsEnum, Mission> _dayFiveEvents = new Dictionary<EventsEnum, Mission>()
    {
        { EventsEnum.DayStarted, new Mission() { Index = 0, IsDiscoverable = false, IsObjective = false, Text = "Thanks for playing" } },
        { EventsEnum.Gear1Ready, new Mission() { Index = 9000, IsDiscoverable = true, IsObjective = true, Text = "No clear objective", FinishText = "Well done" } },
    };

    public Dictionary<EventsEnum, Mission> GetEventsForDay(LevelsEnum day) {
		switch (day) {
			case LevelsEnum.DayOne:
				return _dayOneEvents;

			case LevelsEnum.DayTwo:
				return _dayTwoEvents;

            case LevelsEnum.DayThree:
                return _dayThreeEvents;

            case LevelsEnum.DayFour:
                return _dayFourEvents;

            case LevelsEnum.DayFive:
                return _dayFiveEvents;
		}

		return null;
	}

	public void InitializeGlobals(Globals globals) {
		globals.missions = this;
	}
}