
while (true)
{
    string command = Console.ReadLine();

	if (command == "show")
	{
		long result = SumAsync();
        Console.WriteLine(result);
    }
}

static long SumAsync()
{
    long sum = 0;

    return Task.Run(() =>
    {
        for (int i = 1; i < 10000; i++)
        {
            if (i % 2 == 0)
            {
                sum += i;
            }
        }

        return sum;
    }).Result;

	
}