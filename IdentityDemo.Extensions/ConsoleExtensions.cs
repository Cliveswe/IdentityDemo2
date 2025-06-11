namespace IdentityDemo.Extensions;

/// <summary>
/// Provides extension methods for displaying formatted messages in the console.
/// </summary>
/// <remarks>The <see cref="ConsoleExtensions"/> class includes a variety of methods for writing messages to the
/// console with specific formatting, such as bold text, colored text, or standard output. These methods are designed to
/// enhance the readability of console output and improve user experience.   Note that some methods rely on console
/// features such as ANSI escape codes or color settings, which may not be supported in all environments. Ensure that
/// the target console supports these features before using the corresponding methods.</remarks>
public static class ConsoleExtensions
{
    /// <summary>
    /// Displays the specified message to the standard output.
    /// </summary>
    /// <remarks>This method writes the provided string to the console using <see
    /// cref="Console.WriteLine(string)"/>. If <paramref name="message"/> is null, a <see cref="ArgumentNullException"/>
    /// will be thrown.</remarks>
    /// <param name="message">The message to be written to the console. Cannot be null.</param>
    public static void DisplayStandardMessage(this string message) {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Displays the specified integer value as a message to the console.
    /// </summary>
    /// <param name="message">The integer value to be displayed as a message.</param>
    public static void DisplayStandardMessage(this int message) {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Displays the specified title in a bold format.
    /// </summary>
    /// <param name="title">The title text to be displayed. Cannot be null or empty.</param>
    public static void DisplayTitle(this string title) {
        DisplayBoldText(title);
    }

    /// <summary>
    /// Displays the specified text in bold in the console output.
    /// </summary>
    /// <remarks>This method uses ANSI escape codes to apply bold formatting to the text. Ensure that the
    /// console supports ANSI escape codes for the formatting to work correctly.</remarks>
    /// <param name="text">The text to be displayed in bold. Cannot be null.</param>
    public static void DisplayBoldText(this string text) {
        Console.Write("\x1b[1m");
        Console.Write(text);
        Console.Write("\x1b[22m");
    }

    /// <summary>
    /// Displays the specified message in the console with an informational cyan color.
    /// </summary>
    /// <remarks>This method changes the console's foreground color to cyan while displaying the message and
    /// resets it to the default color afterward. It is intended for informational messages to improve visibility in the
    /// console output.</remarks>
    /// <param name="message">The message to display. Cannot be null or empty.</param>
    public static void DisplayInfoMessage(this string message) {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    /// <summary>
    /// Displays the specified error message in red text on the console.
    /// </summary>
    /// <remarks>This method changes the console text color to red while displaying the message and resets it
    /// to the default color afterward. Ensure the console is available when calling this method.</remarks>
    /// <param name="message">The error message to display. Cannot be null or empty.</param>
    public static void DisplayErrorMessage(this string message) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    /// <summary>
    /// Displays the specified message in green text to indicate success.
    /// </summary>
    /// <remarks>This method changes the console text color to green before displaying the message and resets
    /// the color to its default state afterward.</remarks>
    /// <param name="message">The success message to display. Cannot be null or empty.</param>
    public static void DisplaySuccessMessage(this string message) {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
