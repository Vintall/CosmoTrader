using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class BattleLoger
{
    public static int battle_num;
    public static StreamWriter writer;
    public static void CreateFile()
    {
        writer = new StreamWriter(Application.dataPath + "/Battles/" + battle_num + ".txt");

    }
    public static void Log(string log)
    {
        writer.WriteLine(log);
    }
    public static void CloseFile()
    {
        writer.Close();
    }
}
