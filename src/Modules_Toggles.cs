using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotActions
{
    class Modules_Toggles
    {
        
using BotSharp.ToScript.Extension;
using Parse = Sanderling.Parse;
var Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
// IEnumerable<Parse.IMenu> Menu => Measurement?.Menu;

ModuleMeasureAllTooltip();
    int? ShieldHP => Measurement?.ShipUi?.HitpointsAndEnergy?.Shield / 10;
    int? Capa = Measurement?.ShipUi?.HitpointsAndEnergy?.Capacitor;

//Func<object> TehBellter()

while(true)
{

var Measurement = Sanderling?.MemoryMeasurementParsed?.Value;


    var setModuleHardener = Sanderling?.MemoryMeasurementAccu?.Value?.ShipUiModule?.Where(module => module?.TooltipLast?.Value?.IsHardener ?? false);
    var SubsetModuleToToggle = setModuleHardener?.Where(module => !(module?.RampActive ?? false));
if (null != SubsetModuleToToggle)
	{
		foreach (var Module in SubsetModuleToToggle.EmptyIfNull() )

        ModuleToggle(Module);
}

Host.Delay(1000);
Sanderling?.WaitForMeasurement();


bool shbon => (40 > ShieldHP);

if (40 > ShieldHP)
{
ActivateSB();
Host.Log(ShieldHP+" "+"percents" + "SHB ON)");
}
else if (ShieldHP == 100)
{
DeactivateSB();
Host.Log(ShieldHP+" "+"percents" + "SHB OFF)");
}


Host.Log(ShieldHP+" "+"percents");
//Host.Log(Capa);

}



void ModuleToggle(Sanderling.Accumulation.IShipUiModule Module)
{
    var ToggleKey = Module?.TooltipLast?.Value?.ToggleKey;

    Host.Log("toggle module using " + (null == ToggleKey ? "mouse" : Module?.TooltipLast?.Value?.ToggleKeyTextLabel?.Text));

    if (null == ToggleKey)
        Sanderling.MouseClickLeft(Module);
    else
        Sanderling.KeyboardPressCombined(ToggleKey);
}



void ModuleMeasureAllTooltip()
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

void ActivateSB()
{
    var setModuleSB = Sanderling?.MemoryMeasurementAccu?.Value?.ShipUiModule?.Where(module => module?.TooltipLast?.Value?.IsShieldBooster ?? false);
    var SubsetModuleToToggle = setModuleSB?.Where(module => !(module?.RampActive ?? false));
    if (null != SubsetModuleToToggle)
    {
        foreach (var Module in SubsetModuleToToggle.EmptyIfNull())
            ModuleToggle(Module);
    }
}


void DeactivateSB()
{
    var setModuleSB = Sanderling?.MemoryMeasurementAccu?.Value?.ShipUiModule?.Where(module => module?.TooltipLast?.Value?.IsShieldBooster ?? false);
    var SubsetModuleToToggle = setModuleSB?.Where(module => !(module?.RampActive ?? false));
    if (null == SubsetModuleToToggle)
    {
        foreach (var Module in SubsetModuleToToggle.EmptyIfNull())
            ModuleToggle(Module);
    }
}


    }
}
