namespace CleanArc.GenerateCode.Application.Generators
{
    internal class IEmailSenderGenerator : FileGenerator
    {
        public IEmailSenderGenerator(string path)
            : base(path, "IEmailSender.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "using Domain.Email;",
                "",
                "namespace Application.Interfaces.Email",
                "{",
                "    public interface IEmailSender",
                "    {",
                "        void SendEmail(Message message);",
                "        // Sending an Email in ASP.NET Core Asynchronously",
                "        Task SendEmailAsync(Message message);",
                "    }",
                "}",
            };
    }
}
