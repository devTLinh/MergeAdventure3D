using UnityEngine;

public static class GameMode
{
    const string ModeKey =
        "LastGameMode";

    public static bool IsGuest
    {
        get;
        private set;
    }

    public static bool UseCloud
    {
        get
        {
            return !IsGuest;
        }
    }

    public static void LoadMode()
    {
        int mode =
            PlayerPrefs.GetInt(
                ModeKey,
                0);

        IsGuest =
            mode == 1;
        Debug.Log(
            "[GameMode] LoadMode -> "
            + IsGuest);
    }

    public static void SetGuest()
    {
        IsGuest = true;
        Debug.Log(
            "[GameMode] SetGuest");

        PlayerPrefs.SetInt(
            ModeKey,
            1);

        PlayerPrefs.Save();
    }

    public static void SetCloud()
    {
        IsGuest = false;
        Debug.Log(
            "[GameMode] SetCloud");

        PlayerPrefs.SetInt(
            ModeKey,
            2);

        PlayerPrefs.Save();
    }

    public static void ClearMode()
    {
        IsGuest = false;
        Debug.Log(
            "[GameMode] ClearMode");


        PlayerPrefs.DeleteKey(
            ModeKey);

        PlayerPrefs.Save();
    }
}