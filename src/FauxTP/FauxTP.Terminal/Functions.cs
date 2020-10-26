using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.IO;

namespace FauxTP.Library
{
    class Functions
    {
       //this is a test function to simulate loading a CSV file line by line into array.
        static public string[] loadCSV()
        {
            string[] csvfile = { "line1", "line2" };
            return csvfile;
        }

        //write a file where the contents are in an array to a specific file name
        static private void writeFile(string[] filecontents, string filename)
        {
          using (System.IO.StreamWriter file =
          new System.IO.StreamWriter(filename))
            {
                foreach (string line in filecontents)
                {
                    // If the line doesn't contain the word 'Second', write the line to the file.
                    //if (!line.Contains("Second"))
                    {
                        file.WriteLine(line);
                    }

                }
            }
        }

        //returns true if the file exists returns false if it does not
        static private bool fileExists(string filename)
        {
            return File.Exists(filename);
        }

        /* This function expects an array of strings representing the filecontents, a string for the filename
         * and a bool to indicate whether or not the file should be overwritten. It outputs a bool to indicate
         * whether or not the file has been written or not.
         */
        static public bool overwriteTest(string[] filecontents, string filename, bool shouldOverwrite)
        {

            if (fileExists(filename))
            {
                if(shouldOverwrite)
                {
                    writeFile(filecontents, filename);
                    return true;
                }
            }
            else
            {
                writeFile(filecontents, filename);
                return true;
            }


            return false;
               
        }       
    }
}

