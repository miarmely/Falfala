namespace Entities.ConfigModels
{
	public class UserSettingsConfig
	{
        public int TelNoLength{ get; set; }
        public int MinPasswordLength { get; set; }
        public int MaxPasswordLength { get; set; }
        public List<string> SpecialChars { get; set; }
    }
}
