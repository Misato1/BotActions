using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotActions
{
    class BeltList
    {
        static void Main(string[] args)
        {
           // using BotSharp.ToScript.Extension;
           // using Parse = Sanderling.Parse;

            while (true)
            {
             //   using BotSharp.ToScript.Extension;
             //   using Parse = Sanderling.Parse;

                while (true)
                {
                    var Measurement = Sanderling?.MemoryMeasurementParsed?.Value;

                    var listSurroundingsButton = Measurement?.InfoPanelCurrentSystem?.ListSurroundingsButton;

                    Sanderling.MouseClickRight(listSurroundingsButton);
                    Host.Delay(500);
                    Measurement = Sanderling?.MemoryMeasurementParsed?.Value;

                    var AsteroidMenuEntry = Measurement?.Menu?.FirstOrDefault()?.EntryFirstMatchingRegexPattern("asteroid", RegexOptions.IgnoreCase);

                    Host.Delay(500);
                    Sanderling.MouseClickLeft(AsteroidMenuEntry);
                    Host.Log(AsteroidMenuEntry);
                    Host.Delay(500);
                    Sanderling.InvalidateMeasurement();
                    Measurement = Sanderling?.MemoryMeasurementParsed?.Value;

                    Host.Delay(500);

                    var MenuArray = Measurement?.Menu?.ElementAt(1).Entry.ToArray();


                    Host.Log(MenuArray.Length);


                    int lenght = MenuArray.Length;

                    for (int b = 0; b <= lenght - 1; b++)
                    {
                        // 
                        var menu = Measurement?.Menu?.ElementAtOrDefault(1);
                        if (null == menu)
                        {
                            Host.Log("say no menu active. Activating ClickForMenu!");
                            //listSurroundingsButton = Measurement?.InfoPanelCurrentSystem?.ListSurroundingsButton;
                            Sanderling.MouseClickRight(listSurroundingsButton);
                            Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
                            //Measurement = Sanderling?.MemoryMeasurementParsed?.Value;   
                            AsteroidMenuEntry = Measurement?.Menu?.FirstOrDefault()?.EntryFirstMatchingRegexPattern("asteroid", RegexOptions.IgnoreCase);
                            Sanderling.MouseClickLeft(AsteroidMenuEntry);
                            Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
                            menu = Measurement?.Menu?.ElementAtOrDefault(1);

                        }
                        /* end of menu check
                         */
                        Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
                        Host.Log(b);

                        var Belts = Measurement?.Menu?.ElementAt(1)?.Entry?.ElementAt(b).Text;


                        Host.Log(b + " " + Belts);
                        var AsteroidMenuEntryWarp = menu?.EntryFirstMatchingRegexPattern(Belts, RegexOptions.IgnoreCase);
                        Host.Log(AsteroidMenuEntryWarp.Text);
                        Sanderling.MouseClickLeft(AsteroidMenuEntryWarp);
                        Host.Delay(2000);
                        Measurement = Sanderling?.MemoryMeasurementParsed?.Value;
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
                    }

                    Host.Delay(2000);


                
            }


        }





    }
    }
}
