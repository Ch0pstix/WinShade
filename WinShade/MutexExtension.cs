namespace WinShade;

using System.Reflection;
using System.Threading;
using System.Windows;
using System;

public static class MutexExtension
{
    private static Mutex? mutex;

    /// <summary>
    /// Ensures the <see cref="Application"/> may only have a single running instance.
    /// </summary>
    /// <param name="errorMessage">Optional: Error message shown when another instance of the <see cref="Application"/> is created.</param>
    public static void SetAsSingleInstanceApplication(this Application application, string errorMessage = "")
    {
        if (application is null) return;
        mutex = new(
            true,
            $"{Environment.MachineName}_{Environment.UserName}_{Assembly.GetExecutingAssembly().GetName().Name}",
            out bool IsFirstInstance
            );
        application.Exit += (_, _) => mutex.Dispose();
        if (!IsFirstInstance)
        {
            if (!string.IsNullOrWhiteSpace(errorMessage))
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            application.Shutdown();
        }
    }
}