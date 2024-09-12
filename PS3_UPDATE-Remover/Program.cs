using System.IO;
using System.Runtime.CompilerServices;

namespace PS3_UPDATE_Remover;

internal class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Enter main directory (or type 'e' to quit):");
            string directory = Console.ReadLine();

            if (directory.Equals("e", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (Directory.Exists(directory))
            {
                List<string> result = ScanDirectory(directory);
                DeleteFolder(result);
            }
            else
            {
                Console.WriteLine("\nDirectory does not exist. Please enter a valid directory.\n");
            }
        }
    }

    private static void DeleteFolder(List<string> resultList)
    {
        if (resultList.Count > 0)
        {
            Console.WriteLine($"\nFound {resultList.Count} directories with PS3_UPDATE folder. Delete all PS3_UPDATE folder? (y/n)");
            string result = Console.ReadLine();
            if (result.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine();
                foreach (var dir in resultList)
                {
                    string ps3UpdateFolder_ = "";

                    try
                    {
                        string ps3UpdateFolder = ps3UpdateFolder_ = Path.Combine(dir, "PS3_UPDATE");
                        string ps3UpdateFile = Path.Combine(ps3UpdateFolder, "PS3UPDAT.PUP");

                        if (Directory.Exists(ps3UpdateFolder) && File.Exists(ps3UpdateFile))
                        {
                            Directory.Delete(ps3UpdateFolder, true);
                            Console.WriteLine($"Deleted PS3_UPDATE folder: {ps3UpdateFolder}");
                        }
                        Console.WriteLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occured :" + ex.Message + $" [{ps3UpdateFolder_}]");
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine($"\nFound {resultList.Count} directory.\n");
        }
    }

    private static List<string> ScanDirectory(string directory)
    {
        List<string> ps3UpdateDirectoryList = new List<string>();
        try
        {
            List<string> allDirectories = new List<string>(Directory.EnumerateDirectories(directory));
            allDirectories.Insert(0, directory);
            string[] subDirectories = allDirectories.ToArray();

            foreach (var dir in subDirectories)
            {
                string ps3UpdateFolder = Path.Combine(dir, "PS3_UPDATE");
                string ps3UpdateFile = Path.Combine(ps3UpdateFolder, "PS3UPDAT.PUP");

                if (Directory.Exists(ps3UpdateFolder) && File.Exists(ps3UpdateFile))
                {
                    ps3UpdateDirectoryList.Add(dir);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        return ps3UpdateDirectoryList;
    }
}
