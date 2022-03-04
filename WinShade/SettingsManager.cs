namespace WinShade;

using static Properties.Settings;

public class SettingsManager
{
	public static void Initialize()
	{
		new SettingsManager();
	}

	public SettingsManager()
	{
		Default.PropertyChanged += OnSettingChanged;
	}

	private void OnSettingChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		Default.Save();
	}
}
