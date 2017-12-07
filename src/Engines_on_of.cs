using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotActions
{
    class Engines_on_of
    {
        var motion = 1;﻿

        while(true)
        {
	        var Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
            var myspeed = Measurement?.ShipUi?.SpeedMilli;
            Host.Log(myspeed);
	        if (myspeed.Equals(0))
	            {
		            motion = 0;
		            return motion;
        	    }
	        else 
	            {
		            motion =1;
		            return motion;
        	    }
	    	Host.Log(motion);
	
	        if (motion.Equals(1))
            	{
                    StopEngine();
                    motion = 0;
	                Host.Delay(10000);
	            }
        	else if (motion.Equals(0))
	            {
                    StartEngine();
                    motion = 1;
	                Host.Delay(10000);
	            }
            }
    
void StartEngine()
{
    var bspeedmax = Sanderling?.MemoryMeasurementParsed?.Value?.ShipUi?.ButtonSpeedMax;
    Sanderling.MouseClickLeft(bspeedmax);

}

void StopEngine()
{
    var bspeed0 = Sanderling?.MemoryMeasurementParsed?.Value?.ShipUi?.ButtonSpeed0;
    Sanderling.MouseClickLeft(bspeed0);
}

void StopEngine2()
{
    Sanderling.KeyboardPressCombined(new[] { VirtualKeyCode.LCONTROL, VirtualKeyCode.SPACE });
}


    }
}
