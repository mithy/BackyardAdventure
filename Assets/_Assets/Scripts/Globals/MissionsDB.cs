using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;

[Game, Unique, CreateAssetMenu]
public class MissionsDB : ScriptableObject {

	public struct Mission {
		public string Text;
		public int Index;
		public bool IsObjective;
	}

	private Dictionary<EventsEnum, Mission> _dayOneEvents = new Dictionary<EventsEnum, Mission>()
	{
		{ EventsEnum.DayStarted, new Mission() { Index = 0, IsObjective = false, Text = "Harvest day" } },
		{ EventsEnum.FruitInCrate, new Mission() {Index = 5, IsObjective = true, Text = "Collect fruits" } }
	};

	private Dictionary<EventsEnum, Mission> _dayTwoEvents = new Dictionary<EventsEnum, Mission>()
	{
		{ EventsEnum.DayStarted, new Mission() { Index = 0, IsObjective = false, Text = "1,2,3 Basket!" } },
		{ EventsEnum.BasketballHoop, new Mission() { Index = 3, IsObjective = true, Text = "Score 3 shots at basket" } }
	};

    private Dictionary<EventsEnum, Mission> _dayThreeEvents = new Dictionary<EventsEnum, Mission>()
    {
        { EventsEnum.DayStarted, new Mission() { Index = 0, IsObjective = false, Text = "The sad, broken street lamp" } },
        { EventsEnum.GeneratorTriggered, new Mission() { Index = 1, IsObjective = true, Text = "Use generator to power the street lamp" } }
    };

    private Dictionary<EventsEnum, Mission> _dayFourEvents = new Dictionary<EventsEnum, Mission>()
    {
        { EventsEnum.DayStarted, new Mission() { Index = 0, IsObjective = false, Text = "Windy Windmill" } },
        { EventsEnum.Gear1Ready, new Mission() { Index = 1, IsObjective = true, Text = "Find First Gear" } },
        { EventsEnum.Gear2Ready, new Mission() { Index = 1, IsObjective = true, Text = "Find Second Gear" } },
        { EventsEnum.Gear3Ready, new Mission() { Index = 1, IsObjective = true, Text = "Find Third Gear" } }
    };

    private Dictionary<EventsEnum, Mission> _dayFiveEvents = new Dictionary<EventsEnum, Mission>()
    {
        { EventsEnum.DayStarted, new Mission() { Index = 0, IsObjective = false, Text = "Thanks for playing" } },
        { EventsEnum.Gear1Ready, new Mission() { Index = 9000, IsObjective = true, Text = "No clear objective" } },
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