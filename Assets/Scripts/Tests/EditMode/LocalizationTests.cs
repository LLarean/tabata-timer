using Assets.SimpleLocalization.Scripts;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class LocalizationTests
    {
        [Test]
        public void SetDefaultLanguage_HasNotKey_LanguageCorrespondsSystemLanguage()
        {
            PlayerPrefs.DeleteKey(SettingsType.Language.ToString());
            Localization.SetDefaultLanguage();
            
            var language = LocalizationManager.Language;
            
            Assert.IsTrue(language == Application.systemLanguage.ToString());
        }
        
        [Test]
        public void SetDefaultLanguage_HasRussianLanguageKey_LanguageIsRussian()
        {
            PlayerPrefs.SetInt(SettingsType.Language.ToString(), 0);
            Localization.SetDefaultLanguage();
            
            var language = LocalizationManager.Language;
            
            Assert.IsTrue(language == GlobalStrings.Russian);
        }
        
        [Test]
        public void SetDefaultLanguage_HasEnglishLanguageKey_LanguageIsEnglish()
        {
            PlayerPrefs.SetInt(SettingsType.Language.ToString(), 1);
            Localization.SetDefaultLanguage();
            
            var language = LocalizationManager.Language;
            
            Assert.IsTrue(language == GlobalStrings.English);
        }
    }
}
