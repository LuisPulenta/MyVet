using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MyVet.Common.Helpers
{
    public static class Settings
    {
        private const string _pet = "Pet";
        private const string _token = "Token";
        private const string _owner = "Owner";

        private static readonly string _settingsDefault = string.Empty;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Pet
        {
            get => AppSettings.GetValueOrDefault(_pet, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_pet, value);
        }
        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static string Owner
        {
            get => AppSettings.GetValueOrDefault(_owner, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_owner, value);
        }

    }
}