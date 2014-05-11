namespace Elysium.Test
{
    public sealed partial class App
    {
        private void StartupHandler(object sender, System.Windows.StartupEventArgs e)
        {
            this.Apply(Theme.Dark);
        }
    }
}