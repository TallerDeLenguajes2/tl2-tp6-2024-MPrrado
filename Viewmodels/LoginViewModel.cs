namespace EspacioViewModels
{
    public class LoginViewModel
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }

        public string MensajeError { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsLoged { get; set; }
        public LoginViewModel()
        {
        }
    }
}