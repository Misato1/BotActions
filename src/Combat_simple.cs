using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotActions
{
    class Combat_simple
    {
        
using BotSharp.ToScript.Extension;
using Parse = Sanderling.Parse;

//Sanderling.Parse.IMemoryMeasurement	 Measurement	=>
//	Sanderling?.MemoryMeasurementParsed?.Value;
var Measurement = Sanderling?.MemoryMeasurementParsed?.Value;


    //Sanderling.Parse.IWindowOverview	WindowOverview	=>
    //	Measurement?.WindowOverview?.FirstOrDefault();
    var WindowOverview = Measurement?.WindowOverview?.FirstOrDefault();

    Parse.IOverviewEntry[] ListRatOverviewEntry => WindowOverview?.ListView?.Entry?.Where(entry =>
            (entry?.MainIconIsRed ?? false) || (entry?.IsAttackingMe ?? false))
            ?.OrderBy(entry => entry?.DistanceMax ?? int.MaxValue)
            ?.ToArray();

    Host.Log(ListRatOverviewEntry.Length);

var SetRatName =
        ListRatOverviewEntry?.Select(entry => Regex.Split(entry?.Name ?? "", @"\s+")?.FirstOrDefault())
        ?.Distinct()
        ?.ToArray();

    var SetRatTarget = Measurement?.Target?.Where(target =>
        SetRatName?.Any(ratName => target?.TextRow?.Any(row => row.RegexMatchSuccessIgnoreCase(ratName)) ?? false) ?? false);

    var RatTargetNext = SetRatTarget?.OrderBy(target => target?.DistanceMax ?? int.MaxValue)?.FirstOrDefault();

    IEnumerable<Parse.IMenu> Menu => Measurement?.Menu;
    Sanderling.Interface.MemoryStruct.IMenuEntry MenuEntryLockTarget =>
        Menu?.FirstOrDefault()?.Entry?.FirstOrDefault(entry => entry.Text.RegexMatchSuccessIgnoreCase("^lock"));
    var memoryMeasurementAccu = Sanderling?.MemoryMeasurementAccu?.Value;
    var setModuleWeapon = memoryMeasurementAccu?.ShipUiModule?.Where(module => module?.TooltipLast?.Value?.IsWeapon ?? false);
// temp implementation of turning guns on 
// Sanderling.KeyboardPress(VirtualKeyCode.F2);
	
	//while(true)
	{
if(null == RatTargetNext)
	{
		Host.Log("no rat targeted.");
		//Sanderling.MouseClickRight(ListRatOverviewEntry?.FirstOrDefault());
		Sanderling.MouseClickLeft(ListRatOverviewEntry?.FirstOrDefault());
		Host.Delay(500);
		//Sanderling.MouseClickLeft(MenuEntryLockTarget);
		Sanderling.KeyboardPressCombined(new[] {VirtualKeyCode.LCONTROL , VirtualKeyCode.LBUTTON
});
	}
	else
	{
		Host.Log("rat targeted. sending drones.");
		Sanderling.MouseClickLeft(RatTargetNext);
		 Sanderling.KeyboardPress(VirtualKeyCode.F1);
	//	Sanderling.MouseClickRight(DronesInSpaceListEntry);
	//	Sanderling.MouseClickLeft(Menu?.FirstOrDefault()?.EntryFirstMatchingRegexPattern("engage", RegexOptions.IgnoreCase));
	}
	}
	

    }
}
