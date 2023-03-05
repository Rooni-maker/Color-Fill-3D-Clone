using System.IO;
using System.Text;
using UnityEngine;
using System.Security.Cryptography;

public class SaveSystem
{
    public static void SaveProgress()
    {
        DataBase.Instance.HashOfSaveData = HashGenerator(SaveObjectAsJSON());
        File.WriteAllText(GetSavePath(), JsonUtility.ToJson(DataBase.Instance, true));
    }

    private static string SaveObjectAsJSON() => JsonUtility.ToJson(DataBase.Instance.CreateSaveObject(), true);

    public static void LoadProgress()
    {
        if(File.Exists(GetSavePath())) 
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(GetSavePath()), DataBase.Instance);
            if(!Application.isEditor)
            {
                if((HashGenerator(SaveObjectAsJSON()) != DataBase.Instance.HashOfSaveData))
                {
                    DataBase.Instance.Reset();
                    DeleteProgress();
                    SaveProgress();
                }
            }
        }
        else     
            SaveProgress();
    }

    private static string HashGenerator(string SaveContent)
    {
        SHA256Managed Crypt = new SHA256Managed();
        string Hash = string.Empty;
        byte[] Crypto = Crypt.ComputeHash(Encoding.UTF8.GetBytes(SaveContent), 0, Encoding.UTF8.GetByteCount(SaveContent));
        foreach (byte Bit in Crypto)
        {
            Hash += Bit.ToString("x2");
        }
        return Hash;
    }

    private static void DeleteProgress()
    {
        if(File.Exists(GetSavePath()))
            File.Delete(GetSavePath());
    }

    private static string GetSavePath() => Path.Combine(Application.persistentDataPath, "SavedGame.json");

//Editor only functions
#if UNITY_EDITOR

    //For Opening SaveData file => Shortcut key (Ctrl or Cmd) + Shift + j
    [UnityEditor.MenuItem("Shady/Save System/Open Save File %#j")]
    private static void OpenSaveFile()
    {
        if(File.Exists(GetSavePath()))
            UnityEditor.EditorUtility.RevealInFinder(GetSavePath());
        else
            UnityEditor.EditorUtility.RevealInFinder(Application.persistentDataPath);
    }

    //For Restting Save Data File => Shortcut key (Ctrl or Cmd) + Shift + r
    [UnityEditor.MenuItem("Shady/Save System/Reset Save Data %#r")]
    private static void ResetSaveData()
    {
        if(UnityEditor.EditorUtility.DisplayDialog("Save Data", "Do you want to Reset all Save Data ?", "Yes", "No"))
        {
            UnityEditor.EditorUtility.DisplayDialog("Save Data", "Save Data Reset Succesful!", "Okay");
            DeleteProgress();
        }
    }

#endif

}