namespace ACME.Helper;

// Esta clase servira para métodos y variables de uso global
public class Constants
{
    public const string Agregar = "Agregar";
    public const string Modificar = "Modificar";
    public const string Eliminar = "Eliminar";
    public const string MensajeAgregar = "Ingrese los datos para crear un registro";
    public const string MensajeEditar = "Modifique los campos necesarios.";
    public const string MensajeEliminar = "Está seguro de eliminar el registro.";
    public const string CampoRequerido = "Ingrese información solicitada";
    public static int IdUsuarioAutenticado;

    public static string MantoBoton(string TipoManto)
    {
        string mensaje = string.Empty;

        switch (TipoManto)
        {
            case Constants.Agregar: mensaje   = "Grabar";break;
            case Constants.Modificar: mensaje = "Modificar";break;
            case Constants.Eliminar: mensaje  = "Eliminar";break;
        }

        return mensaje;
    }

    public static DateTime DateNow(int? parametroFecha = -6)
    {
        return DateTime.UtcNow.AddHours((double)parametroFecha);
    }
}
