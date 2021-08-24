using Janky.Utilities.EmailManager;

namespace FujiFilm214
{
    /// <summary>
    /// Class used to interact with Janky.EmailManager NuGet Pkg. This sets up
    /// credentials assigned to the EmailManager object, followed by setting up one
    /// more Emails with a To, Subject, and Message. After which a pre-configured email
    /// can be sent from anywhere within the project.
    /// </summary>
    public class EmailHandler
    {
        public static EmailManager EmailManager = new(
            Configuration.FromEmail,
            Configuration.EmailUsername,
            Configuration.EmailPassword
        );

        public static Email AlertMessage = new(
            Configuration.ToEmail,
            Configuration.Subject,
            Configuration.Message
        );
    }
}