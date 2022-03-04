namespace WinShade.Controls;

using System.Windows;

using Models;

public class FullDisplayWindow : Window
{
	public FullDisplayWindow()
	{
		WindowStartupLocation = WindowStartupLocation.Manual;
	}

	static FullDisplayWindow()
	{
		AllowsTransparencyProperty.OverrideMetadata(typeof(FullDisplayWindow), new FrameworkPropertyMetadata(true));
		HeightProperty.OverrideMetadata(typeof(FullDisplayWindow), new FrameworkPropertyMetadata(0d));
		ShowInTaskbarProperty.OverrideMetadata(typeof(FullDisplayWindow), new FrameworkPropertyMetadata(false));
		TitleProperty.OverrideMetadata(typeof(FullDisplayWindow), new FrameworkPropertyMetadata(""));
		TopmostProperty.OverrideMetadata(typeof(FullDisplayWindow), new FrameworkPropertyMetadata(true));
		WidthProperty.OverrideMetadata(typeof(FullDisplayWindow), new FrameworkPropertyMetadata(0d));
		WindowStyleProperty.OverrideMetadata(typeof(FullDisplayWindow), new FrameworkPropertyMetadata(WindowStyle.None));
	}

	public void Minimize()
	{
		WindowState = WindowState.Minimized;
	}

	public void Maximize()
	{
		WindowState = WindowState.Maximized;
	}

	public Display Display
	{
		get { return (Display)GetValue(DisplayProperty); }
		set { SetValue(DisplayProperty, value); }
	}

	public static readonly DependencyProperty DisplayProperty =
		DependencyProperty.Register(
			 "Display",
			 typeof(Display),
			 typeof(FullDisplayWindow),
			 new FrameworkPropertyMetadata(Display.PrimaryDisplay)
			);


}
