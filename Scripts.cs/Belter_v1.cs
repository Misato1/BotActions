using Parse = Sanderling.Parse;
using BotSharp.ToScript.Extension;
using System;
using System.Collections.Generic;

bool coldstart = true;
var Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
var WindowOverview = Measurement?.WindowOverview?.FirstOrDefault();
Parse.IOverviewEntry[] ListRatOverviewEntry => WindowOverview?.ListView?.Entry?.Where(entry =>
            (entry?.MainIconIsRed ?? false) || (entry?.IsAttackingMe ?? false))
            ?.OrderBy(entry => entry?.DistanceMax ?? int.MaxValue)
            ?.ToArray(); 

for (;;)
{


// Host.Log("Infinite loop begins");


if (coldstart == true)
	{
		// Host.Log("Measure Modules ones on ColdStart");
		ModuleMeasureAllTooltip();
Host.Delay(1000);
		// Host.Log("Enabling Hardeners");
		 var setModuleHardener = Sanderling?.MemoryMeasurementAccu?.Value?.ShipUiModule?.Where(module => module?.TooltipLast?.Value?.IsHardener ?? false);
var SubsetModuleToToggle = setModuleHardener?.Where(module => !(module?.RampActive ?? false));
		if (null != SubsetModuleToToggle)
		{
			foreach (var Module in SubsetModuleToToggle.EmptyIfNull() )
	
	        ModuleToggle(Module);
coldstart = false;
	        BeltList();
	        // Cold Start End
	}

}

	var Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
var MenuArray = Measurement?.Menu?.ElementAt(1).Entry.ToArray();
int lenght = MenuArray.Length;

BeltRunner(MenuArray, lenght);



Host.Delay(1000);

}


public void ModuleMeasureAllTooltip()
{
    Host.Log("measure tooltips of all modules.");

    for (; ; )
    {
        var NextModule = Sanderling.MemoryMeasurementAccu?.Value?.ShipUiModule?.FirstOrDefault(m => null == m?.TooltipLast);

        if (null == NextModule)
            break;

        Host.Log("measure module.");
        //	take multiple measurements of module tooltip to reduce risk to keep bad read tooltip.
        Sanderling.MouseMove(NextModule);
        Sanderling.WaitForMeasurement();
        Sanderling.MouseMove(NextModule);
    }


}

public void ModuleToggle(Sanderling.Accumulation.IShipUiModule Module)
{
    var ToggleKey = Module?.TooltipLast?.Value?.ToggleKey;

    Host.Log("toggle module using " + (null == ToggleKey ? "mouse" : Module?.TooltipLast?.Value?.ToggleKeyTextLabel?.Text));

    if (null == ToggleKey)
        Sanderling.MouseClickLeft(Module);
    else
        Sanderling.KeyboardPressCombined(ToggleKey);
}

public void SystemEvaluation()
{

}

public void BeltList()
{
    var listSurroundingsButton = Measurement?.InfoPanelCurrentSystem?.ListSurroundingsButton;
    Sanderling.MouseClickRight(listSurroundingsButton);
    Host.Delay(500);
    InvalidateMeasurment();
    Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
    var AsteroidMenuEntry = Measurement?.Menu?.FirstOrDefault()?.EntryFirstMatchingRegexPattern("asteroid", RegexOptions.IgnoreCase);
    Host.Delay(500);
    Sanderling.MouseClickLeft(AsteroidMenuEntry);
    //Host.Log(AsteroidMenuEntry);
    Host.Delay(500);
    //yield return;
    // var MenuArray = Measurement?.Menu?.ElementAt(1).Entry.ToArray();

}

public void InvalidateMeasurment()
{
    Sanderling.InvalidateMeasurement();
    Host.Delay(500);

}

public void BeltRunner(Sanderling.Parse.IMenuEntry[] MenuArray, int lenght)
{
    for (int b = 0; b <= lenght - 1; b++)
    {
        var menu = Measurement?.Menu?.ElementAtOrDefault(1);
        if (null == menu)
        {
            BeltList();
            Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
            menu = Measurement?.Menu?.ElementAtOrDefault(1);
        }

        var Belts = Measurement?.Menu?.ElementAt(1)?.Entry?.ElementAt(b).Text;
        var AsteroidMenuEntryWarp = menu?.EntryFirstMatchingRegexPattern(Belts, RegexOptions.IgnoreCase);
        Sanderling.MouseClickLeft(AsteroidMenuEntryWarp);
        Host.Delay(500);
        Measurement = Sanderling?.MemoryMeasurementParsed?.Value;

        /*
		warp block
		
		*/

        var menu2 = Measurement?.Menu?.ElementAtOrDefault(2);
        var warpMenuEntry = menu2?.EntryFirstMatchingRegexPattern("warp", RegexOptions.IgnoreCase);
        Sanderling.MouseClickLeft(warpMenuEntry);
        Host.Delay(12000);
        Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
        var ManeuverType = Measurement?.ShipUi?.Indication?.ManeuverType;

        while (ShipManeuverTypeEnum.Warp == ManeuverType ||
        ShipManeuverTypeEnum.Jump == ManeuverType)
        {

            Host.Delay(5000);
            Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
            ManeuverType = Measurement?.ShipUi?.Indication?.ManeuverType;
            Host.Log(ManeuverType);

            //Sanderling.InvalidateMeasurement();
        }
        //		Host.Delay(1000);
        //	CombatSimple();
    }
}

public void CombatSimple()

{

    Host.Log("Hey!");
    Host.Delay(1000);

    var Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
    var WindowOverview = Measurement?.WindowOverview?.FirstOrDefault();
    var SetRatName =
            ListRatOverviewEntry?.Select(entry => Regex.Split(entry?.Name ?? "", @"\s+")?.FirstOrDefault())
            ?.Distinct()
            ?.ToArray();
    Host.Log(ListRatOverviewEntry.Length);
    Host.Log(SetRatName);


}




