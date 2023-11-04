
using Chronometer;

var chronometer = new Chronometer.Chronometer();

string line;

while ((line = Console.ReadLine()) != "exit")
{
    if (line == "start")
    {
        Task.Run(() => chronometer.Start());
    }

    else if (line == "stop")
    {
        chronometer.Stop();
    }

    else if (line == "lap")
    {
        Console.WriteLine(chronometer.Lap());
    }

    else if (line == "laps")
    {
        if (chronometer.Laps.Count == 0)
        {
            Console.WriteLine("No laps so far!");
        }

        else
        {
            Console.WriteLine("Laps: ");

            Console.WriteLine(string.Join(", ", chronometer.Laps));
        }
    }

    else if (line == "reset")
    {
        chronometer.Reset();
    }

    else if (line == "time")
    {
        Console.WriteLine(chronometer.GetTime);
    }


}

chronometer.Stop();
