namespace Logging
{
    public static class SettingHelper
    {
        public static bool HasEnabled(IList<SettingModel> settings, string key)
        {
            if (settings is null || key is null) return false;

            var setting = settings.FirstOrDefault(x => x.Key == key);

            if (setting is null || setting.Value is null) return false;

            var result = bool.TryParse(setting.Value, out bool value);

            return result && value;
        }
    }
}
