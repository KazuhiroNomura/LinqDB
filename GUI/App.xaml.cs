using System.Windows;
namespace GUI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App:Application {
    protected override void OnStartup(StartupEventArgs e) {
        base.OnStartup(e);
        System.Windows.Controls.ToolTipService.ShowDurationProperty.OverrideMetadata(
            typeof(DependencyObject),
            new FrameworkPropertyMetadata(int.MaxValue));
    }
}