// Cancellation
using System.Collections.Concurrent;

CancellationTokenSource cts = new();
cts.CancelAfter(1000);
ConcurrentQueue<int> queue = new();

await DownloadWebsiteAsync("https://www.google.com", cts.Token);

async Task<string> DownloadWebsiteAsync(string url, CancellationToken cancellationToken)
{
    using HttpClient client = new();
    string html = "";
    for(int i = 0; i < 500; i++)
    {
        queue.Enqueue(i);
        if (cancellationToken.IsCancellationRequested)
        {
            html = "";
            await Console.Out.WriteLineAsync("Canceled");
            break;
        }
        html = await client.GetStringAsync(url, cancellationToken); // Throws OperationCanceledException if canceled
        await Console.Out.WriteLineAsync("Downloaded");
    }
    return html;
}


// Parallel
int[] data = [];
data.AsParallel().Where(IsPrime);

bool IsPrime(int number)
{
    if (number < 2)
        return false;

    if (number == 2 || number == 3)
        return true;

    if (number % 2 == 0 || number % 3 == 0)
        return false;

    int sqrt = (int)Math.Sqrt(number);
    for (int i = 5; i <= sqrt; i += 6)
    {
        if (number % i == 0 || number % (i + 2) == 0)
            return false;
    }

    return true;
}


// Tasks
Task t = Task.Run(() =>
{

});

t.Wait();

void DoSomeStuff()
{
    Thread.Sleep(1000);
}

int Foo() => 42;

async Task<int> Foo2()
{
    return 42;
}

int result = await Foo2();
int result2 = await Foo2();

Task<int> t2 = Foo2();
Task<int> t3 = Foo2();
await Task.WhenAll(t2, t3);
int res1 = t2.Result;
int res2 = t3.Result;

async Task<string> DownloadWebsite(string url)
{
    using HttpClient client = new();
    return await client.GetStringAsync(url); // Asynchronous
}

string DownloadWebsite2(string url)
{
    using HttpClient client = new();
    return client.GetStringAsync(url).Result; // Synchronous
}