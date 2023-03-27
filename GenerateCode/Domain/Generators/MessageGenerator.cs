namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class MessageGenerator : FileGenerator
    {
        public MessageGenerator(string path)
            : base(path, "Message.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "using Microsoft.AspNetCore.Http;",
                "",
                "using MimeKit;",
                "",
                "namespace Domain.Email",
                "{",
                "    public class Message",
                "    {",
                "        public List<MailboxAddress> To { get; set; }",
                "        public string Subject { get; set; }",
                "        public string Content { get; set; }",
                "",
                "        public IFormFileCollection Attachments { get; set; }",
                "",
                "        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)",
                "        {",
                "            To = new List<MailboxAddress>();",
                "",
                "            To.AddRange(to.Select(x => new MailboxAddress(\"email\", x)));",
                "            Subject = subject;",
                "            Content = content;",
                "            Attachments = attachments;",
                "        }",
                "    }",
                "}",
            };
    }
}
