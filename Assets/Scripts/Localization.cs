using Assets.SimpleLocalization.Scripts;
using UnityEngine;

public static class Localization
{
    public static void SetLanguage(string language)
    {
        LocalizationManager.Read();
        var hasKey = PlayerPrefs.HasKey(SettingsType.Language.ToString());

        if (hasKey == true)
        {
            LocalizationManager.Language  = GetLanguageFromSaves();
        }
        else
        {
            LocalizationManager.Language  = language;
            PlayerPrefs.SetInt(SettingsType.Language.ToString(), GetNumberSystemLanguage());
        }
    }
    
    public static void SetDefaultLanguage()
    {
        LocalizationManager.Read();
        var hasKey = PlayerPrefs.HasKey(SettingsType.Language.ToString());

        if (hasKey == true)
        {
            LocalizationManager.Language  = GetLanguageFromSaves();
        }
        else
        {
            LocalizationManager.Language  = GetLanguageFromSystem();
            PlayerPrefs.SetInt(SettingsType.Language.ToString(), GetNumberSystemLanguage());
        }
    }

    private static string GetLanguageFromSaves()
    {
        var numberLanguage = PlayerPrefs.GetInt(SettingsType.Language.ToString(), DefaultSettingsValue.NumberLanguage);

        string language = numberLanguage switch
        {
            0 => GlobalStrings.Russian,
            1 => GlobalStrings.English,
            _ => GlobalStrings.English
        };

        return language;
    }

    private static string GetLanguageFromSystem()
    {
        string language = Application.systemLanguage switch
        {
            SystemLanguage.English => GlobalStrings.English,
            SystemLanguage.Russian => GlobalStrings.Russian,
            _ => GlobalStrings.English
        };

        return language;
    }
    
    private static int GetNumberSystemLanguage()
    {
        int numberLanguage = Application.systemLanguage switch
        {
            SystemLanguage.Russian => 0,
            SystemLanguage.English => 1,
            _ => 1
        };

        return numberLanguage;
    }
}