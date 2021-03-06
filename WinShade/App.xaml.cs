namespace WinShade;

using System.Windows;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

	protected override void OnStartup(StartupEventArgs e)
	{
		this.SetAsSingleInstanceApplication();

		WindowManager.Initialize();
		SettingsManager.Initialize();

		base.OnStartup(e);
	}
}
