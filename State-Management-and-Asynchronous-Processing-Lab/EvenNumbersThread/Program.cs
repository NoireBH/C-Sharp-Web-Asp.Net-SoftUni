
int start = int.Parse(Console.ReadLine());
int end = int.Parse(Console.ReadLine());

Thread evenNumThread = new Thread(() => PrintEvenNumbers(start,end));
evenNumThread.Start();
evenNumThread.Join();
Console.WriteLine("Thread finished work");


static void PrintEvenNumbers(int start, int end)
{
    for (int i = start; i < end; i+= 2)
    {
        if (i % 2 != 0)
        {
            Console.WriteLine(i + 1);
            continue;
        }

        Console.WriteLine(i);
    }
}