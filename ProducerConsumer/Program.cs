using System.Security.Cryptography;

const byte SIZE = 10;
const int AMOUNT = 100;
const int SLEEP_MIN = 150;
const int SLEEP_MAX = 450;

bool producerDone = false;

Random rnd = new(42);
Queue<byte> buffer = new(SIZE);

AutoResetEvent areConsumer = new(false);
AutoResetEvent areProducer = new(true);

Thread producer = new Thread(Produce);
Thread consumer = new Thread(Consume);
Thread producer2 = new Thread(Produce);
Thread consumer2 = new Thread(Consume);

producer.Name = "Producer";
consumer.Name = "Consumer";
producer2.Name = "Producer2";
consumer2.Name = "Consumer2";

producer.Start();
producer2.Start();
consumer.Start();
consumer2.Start();

producer.Join();
producer2.Join();

producerDone = true;

consumer.Join();
consumer2.Join();

Console.WriteLine("Done");

void Produce()
{
    int i = 0;
    bool doWork = false;
    while (i < AMOUNT / 2)
    {
        if (buffer.Count < SIZE)
        {
            Thread.Sleep(rnd.Next(SLEEP_MIN, SLEEP_MAX));
            lock (buffer)
            {
                if (buffer.Count < SIZE)
                {
                    buffer.Enqueue((byte)i);
                    Console.WriteLine($"{Thread.CurrentThread.Name} produced: {i} - Items: {buffer.Count}");
                    doWork = true;
                }
            }
            if (doWork)
            {
                i++;
                areConsumer.Set();
            }
        }
        else
        {
            Console.WriteLine("Queue is full. Go to sleep");
            areProducer.WaitOne(500);
        }
    }
    Console.WriteLine($"{Thread.CurrentThread.Name} finished");
}

void Consume()
{
    while (true)
    {
        if (buffer.Count > 0)
            ConsumeWork();
        else
        {
            Console.WriteLine("Queue is empty go to sleep");
            if (producerDone)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} finished");
                break;
            }
            areConsumer.WaitOne(500);
        }
    }
}

void ConsumeWork()
{
    byte value;
    bool doWork = false;
    lock (buffer)
    {
        if (buffer.Count > 0)   // Multiple consumers -> check if data is here for the consumer
        {
            doWork = true;
            value = buffer.Dequeue();   // Require lock, because enqueue and dequeue are not atomic
            Console.WriteLine($"{Thread.CurrentThread.Name} consumed: {value} - Items: {buffer.Count}");
        }
    }
    if (doWork)
    {
        areProducer.Set();
        Thread.Sleep(rnd.Next(SLEEP_MIN, SLEEP_MAX));
    }
}

/*
 * areConsumer.WaitOne();
        lock (buffer)
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} consumed: {buffer.Dequeue()}");
        areProducer.Set();
        Thread.Sleep(rnd.Next(SLEEP_MIN, SLEEP_MAX)); 
 */

public static class RandomGen2
{
    private static Random _global = new Random();
    [ThreadStatic]
    private static Random _local;

    public static int Next()
    {
        Random inst = _local;
        if (inst == null)
        {
            int seed;
            lock (_global) seed = _global.Next();
            _local = inst = new Random(seed);
        }
        return inst.Next();
    }
}