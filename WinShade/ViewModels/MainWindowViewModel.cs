namespace WinShade.ViewModels;

using System;
using System.Windows.Threading;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

public class MainWindowViewModel : ReactiveObject
{
	private readonly DispatcherTimer _timer;

	[Reactive] public string ClockHours { get; set; }
	[Reactive] public string ClockMinutes { get; set; }
	[Reactive] public string ClockDate { get; set; }

	public MainWindowViewModel()
	{
		var time = (Properties.Settings.Default.Use24HourTime) 
			? DateTime.Now.ToString("H:mm").Split(':') 
			: DateTime.Now.ToString("h:mm").Split(':');
		ClockHours = time[0];
		ClockMinutes = time[1];
		ClockDate = DateTime.Now.ToString("dddd, MMMM d");
		_timer = new();
		_timer.Interval = TimeSpan.FromSeconds(1);
		_timer.Tick += (s, e) =>
		{
			var time = (Properties.Settings.Default.Use24HourTime)
				? DateTime.Now.ToString("H:mm").Split(':')
				: DateTime.Now.ToString("h:mm").Split(':');
			ClockHours = time[0];
			ClockMinutes = time[1];
			ClockDate = DateTime.Now.ToString("dddd, MMMM d");
		};
		_timer.Start();
	}
}
