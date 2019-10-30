using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using System;

public class Logger
{
    static string id;

    public static void SetID(int _id)
    {
        id = "P" + _id;
    }

    /// <summary>
    /// Writes text into a file with a time stamp. Files are storted in persistentDataPath, and named according to the current date.
    /// </summary>
    /// <param name="tag">The tag or "category" of the logged information. Can be used to quickly separate relevant logging information.</param>
    /// <param name="message">The string to be logged.</param>
    public static void Log(string tag, string message)
    {
        try
        {
            // We will use this.
            string path = Application.persistentDataPath + "/" + System.DateTime.Now.ToString("yyyy-MM-dd") + "-" + id + ".txt";

            //Debug.Log(path);

            StreamWriter writer = new StreamWriter(path, true);

            string time = System.DateTime.Now.ToString("hh:mm:ss:fff", CultureInfo.InvariantCulture);

            writer.WriteLine(time + "|" + tag + "|" + message);
            writer.Close();
        }
        catch (Exception e)
        {
            //Debug.Log("Error writing to log file.");
            //Debug.Log(e.ToString());
        }
    }

    /// <summary>
    /// Writes text into a file with a time stamp. Files are storted in persistentDataPath, and named according to the current date and the given suffix.
    /// </summary>
    /// <param name="suffix">The suffix to add into the filename. Used to separate data into several files.</param>
    /// <param name="tag">The tag or "category" of the logged information. Can be used to quickly separate relevant logging information.</param>
    /// <param name="message">The string to be logged.</param>
    public static void Log(string suffix, string tag, string message)
    {
        try
        {
            // We will use this.
            string path = Application.persistentDataPath + "/" + System.DateTime.Now.ToString("yyyy-MM-dd") + "-" + id + "-" + suffix + ".txt";

            //Debug.Log(path);

            StreamWriter writer = new StreamWriter(path, true);

            string time = System.DateTime.Now.ToString("hh:mm:ss:fff", CultureInfo.InvariantCulture);

            writer.WriteLine(time + "|" + tag + "|" + message);
            writer.Close();
        }
        catch (Exception e)
        {
            //Debug.Log("Error writing to log file.");
            //Debug.Log(e.ToString());
        }
    }
}
