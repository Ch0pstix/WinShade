namespace WinShade.Views;

using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

using Controls;


/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FullDisplayWindow
{
	private readonly DispatcherTimer t_timer;
	private readonly DoubleAnimation a_fadeIn;
	private readonly DoubleAnimation a_fadeOut;

	private DateTimeOffset dto_timeLastActive = DateTimeOffset.Now;
	private bool b_areControlsVisible = true;

	public MainWindow()
	{
		InitializeComponent();

		a_fadeIn = new DoubleAnimation(1, TimeSpan.FromMilliseconds(250));
		a_fadeOut = new DoubleAnimation(0, TimeSpan.FromMilliseconds(250));

		t_timer = new() { Interval = TimeSpan.FromMilliseconds(500) };

		AttachEvents();

		t_timer.Start();
	}

	private void AttachEvents()
	{
		MouseMove += (_, _) =>
		{
			dto_timeLastActive = DateTimeOffset.Now;
			if (!b_areControlsVisible)
			{
				RootControls.BeginAnimation(OpacityProperty, a_fadeIn);
				Cursor = Cursors.Arrow;
				b_areControlsVisible = true;
			}
		};

		MouseLeave += (_, _) =>
		{
			if (!SettingsToggle.IsChecked.GetValueOrDefault())
			{
				RootControls.BeginAnimation(OpacityProperty, a_fadeOut);
				b_areControlsVisible = false;
			}
		};

		t_timer.Tick += (_, _) =>
		{
			if (SettingsToggle.IsChecked.GetValueOrDefault()) dto_timeLastActive = DateTimeOffset.Now;
			if (DateTimeOffset.Now - dto_timeLastActive >= TimeSpan.FromSeconds(5))
			{
				RootControls.BeginAnimation(OpacityProperty, a_fadeOut);
				Cursor = Cursors.None;
				b_areControlsVisible = false;
			}
		};
	}

	private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
	{
		Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
	}
}