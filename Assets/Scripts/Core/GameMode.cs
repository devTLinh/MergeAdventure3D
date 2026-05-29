public static class GameMode
{
    public static bool IsGuest
    {
        get
        {
            return
                GameLaunchData
                .IsGuest;
        }
    }

    public static bool UseCloud
    {
        get
        {
            return !IsGuest;
        }
    }

    public static bool UseAuth
    {
        get
        {
            return !IsGuest;
        }
    }
}