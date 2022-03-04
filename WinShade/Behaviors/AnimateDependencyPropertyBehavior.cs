namespace WinShade.Behaviors;

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using Microsoft.Xaml.Behaviors;

public class AnimateDependencyPropertyBehavior : Behavior<SolidColorBrush>
{
	public static readonly DependencyProperty DPProperty =
	  DependencyProperty.Register(
		  "DP",
		  typeof(DependencyProperty),
		  typeof(AnimateDependencyPropertyBehavior),
		  new FrameworkPropertyMetadata(null)
		  );

	public static readonly DependencyProperty ValueProperty =
	  DependencyProperty.Register(
		  "Value",
		  typeof(double),
		  typeof(AnimateDependencyPropertyBehavior),
		  new FrameworkPropertyMetadata(
			  0.0d,
			  FrameworkPropertyMetadataOptions.None,
			  ValueChangedCallback
			  )
		  );

	public static readonly DependencyProperty InitValueProperty =
		DependencyProperty.Register(
			"InitValue",
			typeof(double?),
			typeof(AnimateDependencyPropertyBehavior),
			new PropertyMetadata(null)
			);

	public static readonly DependencyProperty DurationProperty =
		DependencyProperty.Register(
			"Duration",
			typeof(TimeSpan),
			typeof(AnimateDependencyPropertyBehavior),
			new PropertyMetadata(new TimeSpan(0, 0, 1))
			);

	public static readonly DependencyProperty IsEnabledProperty =
		DependencyProperty.Register(
			"IsEnabled",
			typeof(bool),
			typeof(AnimateDependencyPropertyBehavior),
			new PropertyMetadata(true)
			);

	public DependencyProperty DP
	{
		get { return (DependencyProperty)GetValue(DPProperty); }
		set { SetValue(DPProperty, value); }
	}

	public double Value
	{
		get { return (double)GetValue(ValueProperty); }
		set { SetValue(ValueProperty, value); }
	}

	public double? InitValue
	{
		get { return (double?)GetValue(InitValueProperty); }
		set { SetValue(InitValueProperty, value); }
	}

	public TimeSpan Duration
	{
		get { return (TimeSpan)GetValue(DurationProperty); }
		set { SetValue(DurationProperty, value); }
	}

	public bool IsEnabled
	{
		get { return (bool)GetValue(IsEnabledProperty); }
		set { SetValue(IsEnabledProperty, value); }
	}

	protected override void OnAttached()
	{
		base.OnAttached();
		if (InitValue is not null)
		{
			var init_animation = new DoubleAnimation((double)InitValue, Value, new Duration(IsEnabled ? Duration : TimeSpan.Zero));
			AssociatedObject.BeginAnimation(DP, init_animation);
		}
		else
		{
			AssociatedObject.SetValue(DP, Value);
		}
	}

	private static void ValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is not AnimateDependencyPropertyBehavior item || item.AssociatedObject == null) return;
		var valuechange_animation = new DoubleAnimation((double)e.NewValue, new Duration(item.IsEnabled ? item.Duration : TimeSpan.Zero));
		item.AssociatedObject.BeginAnimation(item.DP, valuechange_animation);
	}
}