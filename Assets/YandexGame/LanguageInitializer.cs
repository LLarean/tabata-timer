using UnityEngine;
using YG;

public class LanguageInitializer : MonoBehaviour
{
    private void Start()
    {
        var lang = YandexGame.EnvironmentData.language;
        var language = GetSystemLanguageString(lang);
        
        Localization.SetLanguage(language);
    }
    
    private string GetSystemLanguageString(string lang)
    {
        string language = lang switch
        {
            "en" => GlobalStrings.English,
            "ru" => GlobalStrings.Russian,
            _ => GlobalStrings.English
        };

        return language;
    }
}