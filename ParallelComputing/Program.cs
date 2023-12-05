#region Unsafe
Random rnd = new Random(42);
var map = new Dictionary<byte, byte>();
bool isRunning = true;

Thread th1 = new Thread(DicAction);
Thread th2 = new Thread(DicAction);

th1.Start();
th2.Start();

int count = 0;
int amount = 20000;
for (int i = 0; i < amount; i++)
{
    while (th1.IsAlive || th2.IsAlive)
        Thread.Sleep(10);

    count += map.Count;

    map = new();
    isRunning = true;
    new Thread(DicAction).Start();
    new Thread(DicAction).Start();
}

Console.WriteLine(count / amount);

void DicAction()
{
    while (true)
    {
        try
        {
            if(!isRunning)
                break;
            byte r = (byte)rnd.Next(0, 100);
            if (!map.ContainsKey(r))
                map.Add(r, r);
            else
                map.Remove(r);
        }
        catch
        {
            isRunning = false;
            break;
        }
    }
}
#endregion

#region Safe
Random rndSafe = new Random(42);
var mapSafe = new Dictionary<byte, byte>();
bool isRunningSafe = true;

Thread th1Safe = new Thread(DicActionSafe);
Thread th2Safe = new Thread(DicActionSafe);

th1Safe.Start();
th2Safe.Start();

int countSafe = 0;
int amountSafe = 20000;
for (int i = 0; i < amountSafe; i++)
{
    while (th1Safe.IsAlive || th2Safe.IsAlive)
        Thread.Sleep(10);

    countSafe += mapSafe.Count;

    mapSafe = new();
    isRunningSafe = true;
    new Thread(DicActionSafe).Start();
    new Thread(DicActionSafe).Start();
}

Console.WriteLine(countSafe / amountSafe);

void DicActionSafe()
{
    while (true)
    {
        try
        {
            if (!isRunningSafe)
                break;
            byte r = (byte)rndSafe.Next(0, 100);
            lock (mapSafe)
            {
                if (!mapSafe.ContainsKey(r))
                    mapSafe.Add(r, r);
                else
                    mapSafe.Remove(r);
            }
        }
        catch
        {
            isRunningSafe = false;
            break;
        }
    }
}
#endregion