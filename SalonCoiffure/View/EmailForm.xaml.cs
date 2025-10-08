using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Windows;

namespace SalonCoiffure
{
    /// <summary>
    /// Interaction logic for EmailForm.xaml
    /// </summary>
    public partial class EmailForm : Window
    {
        public EmailForm()
        {
            InitializeComponent();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string to = TxtTo.Text;
            string from = TxtFrom.Text;
            string pw = TxtPw.Text;
            string subject = TxtSubject.Text;
            string body = TxtBody.Text;

            if (string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(body))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // SMTP Configuration
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential(from, pw);
                    smtpClient.EnableSsl = true;

                    // Create the email
                    MailMessage mailMessage = new MailMessage(from, to, subject, body);

                    // Send the email
                    smtpClient.Send(mailMessage);

                    MessageBox.Show("Email envoyé avec succès!", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Nous avons pas peu envoyé l'email. Le mot de passe doit être un 'App Password' qui est générer par Gmail: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
