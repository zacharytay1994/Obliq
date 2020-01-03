using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextIO
{
    public static void WriteFile(string s, string path)
    {
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(s);
        writer.Close();
    }
    public static string ReadFile(string path)
    {
        StreamReader reader = new StreamReader(path, true);
        string r = reader.ReadToEnd();
        reader.Close();
        return r;
    }
}
