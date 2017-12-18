// Sample of overview parse for simple combat or warp out

using Sanderling.Parse;
using Parse = Sanderling.Parse;
// calling OverEntry globaly for accessibility in methods;
using OverEntry = Sanderling.Parse.IOverviewEntry;
//using BotSharp.ToScript.Extension;

for (;;)
{
ShowMeRats();
Host.Delay(1000);
ShowMeStations();
Host.Delay(1000);
}

public void ShowMeRats()
{
    var Measurment = Sanderling.MemoryMeasurementParsed?.Value;
    //var ActiveOverview = Measurment?.WindowOverview?.FirstOrDefault();

    OverEntry[] ActiveOverview2 = Measurment?.WindowOverview?.ElementAt(0)?.ListView?.Entry?.Where(Entry => Entry?.MainIconIsRed ?? false)?.OrderBy(Entry => Entry?.DistanceMin)?.ToArray();

    for (int i = 0; i <= ActiveOverview2.Length - 1; i++)
    {

        Host.Log(ActiveOverview2[i].Name);
    }
}


public void ShowMeStations()
{
    var Measurment = Sanderling.MemoryMeasurementParsed?.Value;
    //var ActiveOverview = Measurment?.WindowOverview?.FirstOrDefault();

    OverEntry[] ActiveOverview2 = Measurment?.WindowOverview?.ElementAt(0)?.ListView?.Entry?.Where(Entry => Entry?.Type?.RegexMatchSuccessIgnoreCase("station") ?? false)?.OrderBy(Entry => Entry?.DistanceMax)?.ToArray();

    for (int i = 0; i <= ActiveOverview2.Length - 1; i++)
    {

        Host.Log(ActiveOverview2[i].Name);

    }
}