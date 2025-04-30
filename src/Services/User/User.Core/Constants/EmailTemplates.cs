namespace User.Core.Constants;

public static class EmailTemplates
{
    public static string ConfirmationTemplate(string confirmLink) =>
        $$"""
              <!DOCTYPE html>
              <html>
              <head>
                  <style>
                      body { font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px; }
                      .email-container { background-color: white; padding: 20px; border-radius: 5px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }
                      h1 { color: #4CAF50; }
                      p { color: #111111; }
                      .btn { background-color: #4CAF50; color: white; padding: 10px 20px; border-radius: 5px; text-decoration: none; }
                  </style>
              </head>
              <body>
                  <div class='email-container'>
                      <h1>Подтверждение регистрации</h1>
                      <p>Здравствуйте, спасибо за регистрацию. Пожалуйста, подтвердите свой email, перейдя по следующей ссылке:</p>
                      <a href='{{confirmLink}}' class='btn'>Подтвердить Email</a>
                  </div>
              </body>
              </html>
          """;

    public static string ForgotPasswordTemplate(string userName, string resetLink) =>
        $$"""
              <!DOCTYPE html>
              <html>
              <head>
                  <style>
                      body { font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px; }
                      .email-container { background-color: white; padding: 20px; border-radius: 5px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }
                      h1 { color: #f44336; }
                      p { color: #111111; }
                      .btn { background-color: #f44336; color: white; padding: 10px 20px; border-radius: 5px; text-decoration: none; }
                  </style>
              </head>
              <body>
                  <div class='email-container'>
                      <h1>Сброс пароля</h1>
                      <p>Здравствуйте, {{userName}}!</p>
                      <p>Мы получили запрос на сброс пароля. Чтобы задать новый пароль, пожалуйста, перейдите по ссылке ниже:</p>
                      <a href='{{resetLink}}' class='btn'>Сбросить пароль</a>
                      <p>Если вы не запрашивали сброс пароля, просто проигнорируйте это сообщение.</p>
                  </div>
              </body>
              </html>
          """;
}