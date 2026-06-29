using TimeProject.Application.Dtos.Emails;

namespace TimeProject.Application.Factories;

public static class RegisterEmailFactory
{
    public static EmailPayloadDto Create(string email, string url, DateTime dateLimit)
    {
        return new EmailPayloadDto
        {
            To = email,
            Subject = "Confirmação de E-mail - Registra meu tempo aí!",
            Body = EmailFactory
                .InjectBody("""
                            <span class="preheader">Bem vindo(a) ao Registra meu tempo aí...</span>
                            <table role="presentation" border="0" cellpadding="0" cellspacing="0" class="main">

                             <!-- START MAIN CONTENT AREA -->
                             <tr>
                               <td class="wrapper">
                                 <p>Bem vindo(a) ao Registra meu tempo aí!</p>
                                 <p>Para utilizar nossa ferramenta, verifique seu email.</p>
                                 <table role="presentation" border="0" cellpadding="0" cellspacing="0" class="btn btn-primary">
                                   <tbody>
                                     <tr>
                                       <td align="left">
                                         <table role="presentation" border="0" cellpadding="0" cellspacing="0">
                                           <tbody>
                                             <tr>
                                               <td>
                            """
                            + $"<a href='{url}' target=\"_blank\">Verificar e-mail</a>" +
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
                            + $"Válido até: {TimeZoneInfo.ConvertTimeFromUtc(dateLimit, TimeZoneInfo.Local):dd/MM/yyyy HH:mm:ss} <br />" +
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