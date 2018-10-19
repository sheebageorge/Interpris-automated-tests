namespace Automation.UI.Core.APILibraries
{
    public class LoginToken
    {
        public string Access_Token { get; set; }
        public string Id_Token { get; set; }
        public string Scope { get; set; }
        public int Expires_In { get; set; }
        public string Token_Type { get; set; }

        public override string ToString()
        {
            return string.Format("AccessToken:{0}\nIdToken:{1}\nScope:{2}\nExpiresIn:{3}\nExpiresIn:{4}",
                Access_Token, Id_Token, Scope, Expires_In, Token_Type);
        }
    }
}
