using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using YG;

public class LanguageInitializer : MonoBehaviour
{
    private void Start()
    {
        var lang = YandexGame.EnvironmentData.language;
        var language = GetSystemLanguageString(lang);
        
        LocalizationManager.Read();
        LocalizationManager.Language  = language;
    }
    
    private string GetSystemLanguageString(string lang)
    {
        string language = lang switch
        {
            "en" => GlobalStrings.English,
            "ru" => GlobalStrings.Russian,
            _ => GlobalStrings.Russian
        };

        return language;
    }
}