using TimeProject.Application.Dtos.Emails;

namespace TimeProject.Application.Factories;

public static class RecoveryEmailFactory
{
    public static EmailPayloadDto Create(string email, string url, DateTime dateLimit)
    {
        return new EmailPayloadDto
        {
            To = email,
            Subject = "Recuperação de Senha - Registra meu tempo aí!",
            Body = EmailFactory
                .InjectBody("""
                            <span class="preheader">Você acabou de requisitar a recuperação de senha...</span>
                            <table role="presentation" border="0" cellpadding="0" cellspacing="0" class="main">

                             <!-- START MAIN CONTENT AREA -->
                             <tr>
                               <td class="wrapper">
                                 <p>Você acabou de requisitar a recuperação de senha.</p>
                                 <table role="presentation" border="0" cellpadding="0" cellspacing="0" class="btn btn-primary">
                                   <tbody>
                                     <tr>
                                       <td align="left">
                                         <table role="presentation" border="0" cellpadding="0" cellspacing="0">
                                           <tbody>
                                             <tr>
                                               <td>
                            """
                            + $"<a href='{url}' target=\"_blank\">Recriar senha</a>" +
                            """
                                               </td>
                                             </tr>
                                           </tbody>
                                         </table>
                                       </td>
                                     </tr>
                                   </tbody>
                                 </table>
                                 <p>
                            """
                            + $"Expiração do código: {TimeZoneInfo.ConvertTimeFromUtc(dateLimit, TimeZoneInfo.Local):dd/MM/yyyy HH:mm:ss} <br />" +
                            $"UTC: {dateLimit}" +
                            """
                                 </p>
                               </td>
                             </tr>

                             <!-- END MAIN CONTENT AREA -->
                            </table>
                            """),
            IsHtml = true
        };
    }
}