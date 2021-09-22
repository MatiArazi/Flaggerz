using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class FileHandler
{

    public static void SaveListToJSON<T>(List<T> toSave, string filename)
    {
        string content = JsonHelper.ToJson<T>(toSave.ToArray());
        WriteFile(GetPath(filename), content);
    }

    public static void SaveToJSON<T>(T toSave, string filename)
    {
        string content = JsonUtility.ToJson(toSave);
        WriteFile(GetPath(filename), content);
    }

    public static List<T> ReadListFromJSON<T>(string filename)
    {
        string content = ReadFile(GetPath(filename));
        Debug.Log(GetPath(filename));
        if (content == "{}") Debug.Log("No content :(");
        if (content == "") Debug.Log("No content :( ");
        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return new List<T>();
            Debug.Log("New List<t>");
        }

        List<T> res = JsonHelper.FromJson<T>(content).ToList();

        return res;
    }

    public static T ReadFromJSON<T>(string filename)
    {
        string content = ReadFile(GetPath(filename));

        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return default(T);
        }

        T res = JsonUtility.FromJson<T>(content);

        return res;

    }

    public static string GetPath(string filename)
    {
        //return Application.persistentDataPath + "/" + filename;
        // return Application.dataPath + "/StreamingAssets/" + filename;
        //return Application.dataPath + "/StreamingAssets/" + filename;
        //return Path.Combine(Application.streamingAssetsPath, filename);
        string localPath = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            localPath = Application.streamingAssetsPath + "/" + filename;
        }
        else
        {
            localPath = Application.streamingAssetsPath + "/" + filename;
        }
        return localPath;
    }

    private static void WriteFile(string path, string content)
    {
        /*FileStream fileStream = new FileStream (path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter (fileStream)) {
            writer.Write (content);
        }*/
        File.WriteAllText(path, content);
    }

    private static string ReadFile(string path)
    {
        /* if (File.Exists (path)) {
             using (StreamReader reader = new StreamReader (path)) {
                 string content = reader.ReadToEnd ();
                 return content;
             }
         }
         return "";
     }
     // otra cosa
        WWW t_WWW = new WWW(path); // El formato debe ser "ANSI", no "UTF-8"
        if (t_WWW.error != null)
        {
            Debug.LogError("error : " + path);
            return ""; // Error al leer el archivo
        }

        while (!t_WWW.isDone)
        {

        }
        Debug.Log("t_WWW.text=  " + t_WWW.text);

        return t_WWW.text;
        */
        return File.ReadAllText(path);
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}