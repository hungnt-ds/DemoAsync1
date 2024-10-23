using System.Diagnostics;
using System.Text;

namespace DemoAsync1;

public class KoiFish
{
    public int FishId { get; set; }
    public string FishName { get; set; } = null!;
    public string Breed { get; set; } = null!;
    public decimal Size { get; set; }
    public bool IsDeleted { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        var koiFishList = new List<KoiFish>
        {
            new KoiFish { FishId = 1, FishName = "Sakura", Breed = "Kohaku", Size = 30.5m, IsDeleted = false },
            new KoiFish { FishId = 2, FishName = "Midori", Breed = "Sanke", Size = 35.0m, IsDeleted = false },
            new KoiFish { FishId = 3, FishName = "Kumo", Breed = "Showa", Size = 25.0m, IsDeleted = true },
            new KoiFish { FishId = 4, FishName = "Hana", Breed = "Tancho", Size = 28.3m, IsDeleted = false }
        };
        // HÀM ĐÔNG BỘ
        Console.WriteLine("HÀM ĐÔNG BỘ:");
        var stopwatchSync = Stopwatch.StartNew();
        PrintKoiFishListSync(koiFishList);
        stopwatchSync.Stop();
        Console.WriteLine($"Thời gian chạy: {stopwatchSync.ElapsedMilliseconds} ms\n");

        // HÀM BẤT ĐỒNG BỘ
        Console.WriteLine("HÀM BẤT ĐỒNG BỘ:");
        var stopwatchAsync = Stopwatch.StartNew();
        PrintKoiFishListAsync(koiFishList).Wait();
        stopwatchAsync.Stop();
        Console.WriteLine($"Thời gian chạy: {stopwatchAsync.ElapsedMilliseconds} ms\n");

        Console.ReadLine();
    }

    static void PrintKoiFishListSync(List<KoiFish> koiFishList)
    {
        foreach (var koi in koiFishList)
        {
            Thread.Sleep(500);
            Console.WriteLine($"[Sync] FishId: {koi.FishId}, FishName: {koi.FishName}, Breed: {koi.Breed}, Size: {koi.Size}, IsDeleted: {koi.IsDeleted}");
        }
    }

    static async Task PrintKoiFishListAsync(List<KoiFish> koiFishList)
    {
        var tasks = koiFishList.Select(async koi =>
        {
            await Task.Delay(500);
            Console.WriteLine($"[Async] FishId: {koi.FishId}, FishName: {koi.FishName}, Breed: {koi.Breed}, Size: {koi.Size}, IsDeleted: {koi.IsDeleted}");
        });

        await Task.WhenAll(tasks);
    }
}



