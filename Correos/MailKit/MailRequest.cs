namespace Correos.MailKit
{
    public class MailRequest
    {
        public string toEmail { get; set; } = null!;
        public string subject { get; set; } = null!;
        public string body { get; set; } = null!;
        public string usuario { get; set; } = null!;
        public string Attachment { get; set; }
    }
}
