using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpreadSheetURL
{
    public static readonly Dictionary<string, List<string>> SheetURL = new Dictionary<string, List<string>>()
    {
        { "1RzXc04yWfLX0Z3v8hqJJpl0mDl2VGUTSHdX9N7zlA1A", BulletSheet.Sheets }, // Bullet
    };
}

public enum SpreadSheetType
{
    Bullet = 0,
}

public static class BulletSheet
{
    public static readonly List<string> Sheets = new List<string>()
    {
        "Bullet", // Bullet
    };
}
