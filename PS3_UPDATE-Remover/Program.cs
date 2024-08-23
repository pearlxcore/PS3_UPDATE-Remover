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
                ScanAndDeleteFolder(directory);
            }
            else
            {
                Console.WriteLine("\nThe directory does not exist. Please enter a valid directory.\n");
            }
        }
    }

    private static void ScanAndDeleteFolder(string directory)
    {
        try
        {
            string[] subDirectories = Directory.GetDirectories(directory);

            foreach (var dir in subDirectories)
            {
                string ps3UpdateFolder = Path.Combine(dir, "PS3_UPDATE");
                string ps3UpdateFile = Path.Combine(ps3UpdateFolder, "PS3UPDAT.PUP");

                if (Directory.Exists(ps3UpdateFolder) && File.Exists(ps3UpdateFile))
                {
                    Directory.Delete(ps3UpdateFolder, true);
                    Console.WriteLine($"Deleted PS3_UPDATE folder: {ps3UpdateFolder}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}
