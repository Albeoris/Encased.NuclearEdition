using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Encased.Debugger
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Game directory
                String directoyPath = args[0];

                String executablePath = directoyPath + "\\Encased.exe";
                String unityPath = directoyPath + "\\Unity.exe";

                if (!File.Exists(unityPath))
                {
                    File.Copy(executablePath, unityPath);
                    File.SetLastWriteTimeUtc(unityPath, File.GetLastWriteTimeUtc(executablePath));
                }
                else
                {
                    FileInfo fi1 = new FileInfo(executablePath);
                    FileInfo fi2 = new FileInfo(unityPath);
                    if (fi1.Length != fi2.Length || fi1.LastWriteTimeUtc != fi2.LastWriteTimeUtc)
                    {
                        File.Copy(executablePath, unityPath, true);
                        File.SetLastWriteTimeUtc(unityPath, fi1.LastWriteTimeUtc);
                    }
                }

                executablePath = unityPath;

                String EncasedDataPath = Path.GetFullPath(directoyPath + "\\Encased_Data");
                String unityDataPath = Path.GetFullPath(directoyPath + "\\Unity_Data");

                if (!Directory.Exists(unityDataPath))
                {
                    JunctionPoint.Create(unityDataPath, EncasedDataPath, true);
                }
                else
                {
                    try
                    {
                        foreach (String item in Directory.EnumerateFileSystemEntries(unityDataPath))
                            break;
                    }
                    catch
                    {
                        JunctionPoint.Delete(unityDataPath);
                        JunctionPoint.Create(unityDataPath, EncasedDataPath, true);
                    }
                }

                String arguments = String.Join(" ", args.Skip(1).Select(a => '"' + a + '"'));

                ProcessStartInfo gameStartInfo = new ProcessStartInfo(executablePath, arguments) {UseShellExecute = false, WorkingDirectory = directoyPath};
                //gameStartInfo.EnvironmentVariables["UNITY_GIVE_CHANCE_TO_ATTACH_DEBUGGER"] = "1";

                Process gameProcess = new Process {StartInfo = gameStartInfo};
                gameProcess.Start();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Unexpected error has occurred.");
                Console.WriteLine(ex);
                Console.WriteLine();
                Console.WriteLine("Press enter to exit...");
                Console.ReadLine();
            }
        }
    }
}