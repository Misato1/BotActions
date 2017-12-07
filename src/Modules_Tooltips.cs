using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotActions
{
    class Modules_Tooltips
    {

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
}
