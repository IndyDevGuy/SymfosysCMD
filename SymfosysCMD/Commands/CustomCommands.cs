using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SymfosysCMD.Commands
{
    public static class CustomCommands
    {
		public static readonly RoutedUICommand Exit = new RoutedUICommand
		(
			"Exit",
			"Exit",
			typeof(CustomCommands),
			new InputGestureCollection()
			{
				new KeyGesture(Key.F4, ModifierKeys.Alt)
			}
		);
		public static readonly RoutedUICommand Preferences = new RoutedUICommand
		(
			"Preferences",
			"Preferences",
			typeof(CustomCommands),
			new InputGestureCollection()
			{
				new KeyGesture(Key.P, ModifierKeys.Control)
			}
		);
		public static readonly RoutedUICommand New = new RoutedUICommand
		(
			"New",
			"New",
			typeof(CustomCommands),
			new InputGestureCollection()
			{
				new KeyGesture(Key.N, ModifierKeys.Control)
			}
		);
		public static readonly RoutedCommand Open = new RoutedUICommand
		(
			"Open",
			"Open",
			typeof(CustomCommands),
			new InputGestureCollection()
			{
				new KeyGesture(Key.O, ModifierKeys.Control)
			}
		);
		public static readonly RoutedCommand Save = new RoutedUICommand
		(
			"Save",
			"Save",
			typeof(CustomCommands),
			new InputGestureCollection()
			{
				new KeyGesture(Key.S, ModifierKeys.Control)
			}
		);
	}
}
