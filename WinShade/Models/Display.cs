namespace WinShade.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

using WpfScreenHelper;

public struct Display
{
	public int Id { get; set; }
	public string Name { get; set; }
	public bool IsPrimary { get; set; }
	public double Scale { get; set; }
	public Rect Bounds { get; set; }
	public Rect WorkingArea { get; set; }

	private Display(string name, bool isPrimary, double scale, Rect bounds, Rect workingArea)
	{
		Name = name;
		IsPrimary = isPrimary;
		Scale = scale;
		Bounds = bounds;
		WorkingArea = workingArea;

		Id = Convert.ToInt32(Regex.Match(name, @"[0-9]").Value);
	}

	public static IEnumerable<Display> AllDisplays
	{
		get
		{
			List<Display> displays = new();
			foreach (Screen screen in Screen.AllScreens)
			{
				displays.Add(new(
						screen.DeviceName,
						screen.Primary,
						screen.ScaleFactor,
						screen.WpfBounds,
						screen.WpfWorkingArea
					));
			}
			return displays.ToArray<Display>();
		}
	}

	public static Display PrimaryDisplay
	{
		get
		{
			return new(
					Screen.PrimaryScreen.DeviceName,
					Screen.PrimaryScreen.Primary,
					Screen.PrimaryScreen.ScaleFactor,
					Screen.PrimaryScreen.WpfBounds,
					Screen.PrimaryScreen.WpfWorkingArea
				);
		}
	}

	public override bool Equals(object? obj)
	{
		return obj is Display other
			&& GetHashCode() == other.GetHashCode();
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Name, IsPrimary, Scale, Bounds, WorkingArea);
	}

	public override string ToString()
	{
		return $"Display: {{ Name: {Name}, Primary: {IsPrimary}, Scale: {Scale}: Bounds: {Bounds}, Working Area: {WorkingArea} }}";
	}

	public static bool operator ==(Display left, Display right)
	{
		return left.Equals(right);
	}

	public static bool operator !=(Display left, Display right)
	{
		return !left.Equals(right);
	}
}
