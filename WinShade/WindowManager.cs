namespace WinShade;

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using Models;
using Views;

public class WindowManager
{
	public static IEnumerable<MainWindow> Windows 
		=> Application.Current.Windows.OfType<MainWindow>();

	public static void Initialize()
	{
		_ = new WindowManager();
		return;
	}

	private WindowManager()
	{
		foreach (var (display, i) in Display.AllDisplays.Select((display, i) => (display, i)))
		{
			var window = new MainWindow()
			{
				Display = display, 
				Left = display.Bounds.X, 
				Top = display.Bounds.Y,
				Owner = (i > 0) ? Windows.FirstOrDefault() : null
			};
			AttachEvents(window);
			window.Show();
			window.Maximize();
		}
	}

	private static void AttachEvents(MainWindow window)
	{
		window.MouseDown += (_, e) => 
		{
			if (e.ClickCount == 2)
				window.Close();
		};

		window.KeyDown += (_, e) =>
		{
			if (e.Key == Key.Escape)
				window.Close();
		};

		window.Closing += (_, e) =>
		{
			if (window.Owner != null)
				window.Owner.Close();
		};
	}
}
