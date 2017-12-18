// rework  of combat simple
using BotSharp.ToScript.Extension;
using Sanderling;
using Sanderling.Parse;
using Parse = Sanderling.Parse;
using OverEntry = Sanderling.Parse.IOverviewEntry;
using OverTab = Sanderling.Interface.MemoryStruct.Tab;
using TargetTab = Sanderling.Parse.IShipUiTarget;


// max targets for ship of choise.
int maxtargets = 7;
ModuleMeasureAllTooltip();
int? ShieldHP => Measurement?.ShipUi?.HitpointsAndEnergy?.Shield / 10;
int? Capa = Measurement?.ShipUi?.HitpointsAndEnergy?.Capacitor;
var Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
var ButtonToLock = new[] { VirtualKeyCode.LCONTROL };
var ButtonToReturnDrones = new[] { VirtualKeyCode.SHIFT, VirtualKeyCode.VK_R };
var ButtonToLaunchDrones = new[] { VirtualKeyCode.VK_Z };
var DronesInSpaceGlobal = DronesInSpace();


for (;;)
{

 
    Host.Delay(1000);
    

    var RatArray = ShowMeRats();
int RatsQty = RatArray.Length;
	if (RatsQty > 0 )
	{
	Host.Log("Kill da rats");
	Combat(RatArray, maxtargets);
	}
	else
	{
	Host.Log("Nope" + " Goto Next Action!");
	DronesInSpaceGlobal = DronesInSpace();
	if (DronesInSpaceGlobal > 0)
		{
		Host.Log("Drones in Space = " +DronesInSpaceGlobal );
		Host.Log("Collecting my dronez");
		 Sanderling.KeyboardPressCombined(ButtonToReturnDrones);
		 Host.Delay(5000);
		 Sanderling.InvalidateMeasurement();
		 DronesInSpaceGlobal = DronesInSpace();
		}
	
	
	
	}
	


}


// return rats

public OverEntry[] ShowMeRats()
{
    var Measurment = Sanderling.MemoryMeasurementParsed?.Value;
    if (Measurment?.WindowOverview?.ElementAt(0)?.Caption?.RegexMatchSuccessIgnoreCase("pve") == true)
    {

    }
    else
    {


        Sanderling.Parse.IWindowOverview Overview = Measurment?.WindowOverview?.FirstOrDefault();
        var OverTabsArray = Overview?.PresetTab?.ToArrayIfNotEmpty();
        for (int i = 0; i < OverTabsArray.Length; i++)
        {
            string tabtext = OverTabsArray[i].Label.Text.ToString();
            if (tabtext == "pve")
            {
                Host.Log("Found at " + i + " place in array. Rereading Memory");
                var buttontoclick = Overview.PresetTab.ElementAt(i).RegionInteraction;
                Sanderling.MouseClickLeft(buttontoclick);
                // Invalidate Measuremnt and reread on next step;
                Sanderling.InvalidateMeasurement();
                break;
            }

        }



    }
    Measurment = Sanderling.MemoryMeasurementParsed?.Value;

    OverEntry[] ActiveOverview = Measurment?.WindowOverview?.ElementAt(0)?.ListView?.Entry?.Where(Entry => Entry?.MainIconIsRed ?? false)?.OrderBy(Entry => Entry?.DistanceMin)?.ToArray();

    return ActiveOverview;

}

public void Combat(OverEntry[] InpuArray, int InputMaxTargets)
{


    var Measurment = Sanderling.MemoryMeasurementParsed?.Value;
    Host.Log("Drone launch no preference");
    var DroneBay = Measurment.WindowDroneView[0].RegionInteraction;
    //Sanderling.MouseClickLeft(DroneBay);
    Host.Delay(500);

    Sanderling.KeyboardPressCombined(ButtonToLaunchDrones);
    //Sanderling.KeyboardPress(VirtualKeyCode.VK_Z);

    Host.Delay(500);

    // targeting cycle
    targetingcycle(InpuArray);
    damagecycle(InpuArray);

    //damagecycle();


}

public void targetingcycle(OverEntry[] InpuArray)
{
    var ButtonToLock = new[] { VirtualKeyCode.LCONTROL };
    //var RatTargeting = InpuArray[1].RegionInteraction;
    var Measurment = Sanderling.MemoryMeasurementParsed?.Value;
    for (int i = 0; i < InpuArray.Length; i++)
    {
        try
        {
            var RatTargeting = InpuArray[i].RegionInteraction;
            Sanderling.MouseClickLeft(RatTargeting);
            Host.Delay(500);
            Sanderling.KeyboardPressCombined(ButtonToLock);
            Host.Delay(500);
            if (i == 3)
            {
                break;
            }
        }
        catch { }
    }
}

void damagecycle(OverEntry[] InpuArray)
{

    var Measurment = Sanderling.MemoryMeasurementParsed?.Value;
    var memoryMeasurementAccu = Sanderling?.MemoryMeasurementAccu?.Value;
    var setModuleWeapon = memoryMeasurementAccu?.ShipUiModule?.Where(module => module?.TooltipLast?.Value?.IsWeapon ?? false);
    var SubsetModuleToToggle = setModuleWeapon?.Where(module => !(module?.RampActive ?? false));
    // bool InpuArray[

    //Host.Log(setModuleWeapon);
    TargetTab[] Targets = Measurment?.Target?.ToArray();
    // Host.Log("");
    //Host.Log(Targets.Length);
    try
    {
        while (Targets.Length > 0)
        {
            if (null != SubsetModuleToToggle)
            {
                SubsetModuleToToggle = setModuleWeapon?.Where(module => !(module?.RampActive ?? false));
                foreach (var Module in SubsetModuleToToggle.EmptyIfNull())
                {
                    ModuleToggle(Module);
                    Host.Delay(500);
                    Measurment = Sanderling.MemoryMeasurementParsed?.Value;
                }

                Sanderling.KeyboardPress(VirtualKeyCode.VK_F);
                Host.Delay(5000);
                Targets = Measurment?.Target?.ToArray();
            }
        }

    }
    catch { }


}

//public void ModuleToggle(Sanderling.Accumulation.IShipUiModule Module)
public void ModuleToggle(Sanderling.Accumulation.IShipUiModule Module)
{
    var ToggleKey = Module?.TooltipLast?.Value?.ToggleKey;

    Host.Log("toggle module using " + (null == ToggleKey ? "mouse" : Module?.TooltipLast?.Value?.ToggleKeyTextLabel?.Text));

    if (null == ToggleKey)
        Sanderling.MouseClickLeft(Module);
    else
        Sanderling.KeyboardPressCombined(ToggleKey);
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

public int? DronesInSpace()
{

    var Measurment = Sanderling.MemoryMeasurementParsed?.Value;
    var DroneTab = Measurment?.WindowDroneView?.FirstOrDefault();
    DroneViewEntryGroup DroneInSpaceList = DroneTab?.ListView?.Entry?.OfType<DroneViewEntryGroup>()?.FirstOrDefault(Entry => null != Entry?.Caption?.Text?.RegexMatchIfSuccess(@"Drones in Local Space", RegexOptions.IgnoreCase));
    int? DronesInLocalSpaceQty = DroneInSpaceList?.Caption?.Text?.AsDroneLabel()?.Status?.TryParseInt();
    //Host.Log("Qty of Drones = " + DronesInLocalSpaceQty);
    return DronesInLocalSpaceQty;
}
