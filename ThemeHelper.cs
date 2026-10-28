using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace prakt6_koscheev
{
   
    
        static class ThemeHelper
        {
            private static readonly string[] _themePath = { 
            "style/theme/default.xaml", 
            "style/theme/dark.xaml" 
        };
            public static string Current
            {
                get => Properties.Settings.Default.ThemePath1 == "" ? _themePath[0] : Properties.Settings.Default.ThemePath1;
                set
                {
                    Properties.Settings.Default.ThemePath1 = value;
                    Properties.Settings.Default.Save();
                }
            }
            public static void Apply(string themePath)
            {
                var newTheme = new ResourceDictionary
                {

                    Source = new Uri(themePath, UriKind.Relative)
                };
                var oldTheme = Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => _themePath.Any(path => d.Source != null && d.Source.OriginalString.EndsWith(path, StringComparison.OrdinalIgnoreCase)));
                if (oldTheme != null)
                {
                    int index =
                    Application.Current.Resources.MergedDictionaries.IndexOf(oldTheme);
                    Application.Current.Resources.MergedDictionaries[index] =
                    newTheme;
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(newTheme);
                }
                Current = themePath;
            }
            public static void Saved()
            {
                var theme = Current;
                Apply(theme);
            }
            public static void Toggle()
            {
                if (Current == _themePath[0])
                    Apply(_themePath[1]);
                else
                    Apply(_themePath[0]);
            }

        }
    
}
