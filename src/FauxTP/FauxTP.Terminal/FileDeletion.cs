using System;
using System.IO;
 
namespace FauxTP.Termial {
    class FileDeleteProgram {
        static void Main(string[] args) {
			
			//add file to directory for test case
			Console.WriteLine("Enter directory name:");
			string directory = Console.ReadLine();
			//add directory for test case
			Directory.CreateDirectory(directory);
			
			//User enters file to be deleted
			Console.WriteLine("Enter file name:");
			string filePath = Console.ReadLine();
			//add file for test case
			File.Create(filePath);
			
			//add file to directory
			//string pathString = Path.Combine(pathString, filePath);
			string pathString = Path.Combine(directory, filePath);
			//verify
			Console.WriteLine("File path: {0}", pathString);
			
			//Search for file in a given directory
			
			//If file exists, delete
			if(File.Exists(filePath))
			{
				File.Delete(filePath);
				Console.WriteLine(filePath + " File successfully deleted.");
			}
			else
			{
				Console.WriteLine("File not found.");
			}
        }
    }
}