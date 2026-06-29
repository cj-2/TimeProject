namespace TimeProject.Application.Factories;

public static class FeedbackFactory
{
    public static string Create(string message, bool isPublic, string name, string email, bool isVerified = false)
    {
        var sendType = isPublic ? "Público" : "Credenciado";
        var verifiedLabel = isVerified ? "Sim" : "Não";

        return $"<b>Nome:</b> {name}" +
               $"\n<b>Tipo de Envio:</b> {sendType}" +
               $"\n<b>Email:</b> {email}" +
               (isPublic ? "" : $"\n<b>E-mail Verificado:</b> {verifiedLabel}") +
               $"\n\n<b>Mensagem:</b>\n<blockquote>{message}</blockquote>";
    }
}