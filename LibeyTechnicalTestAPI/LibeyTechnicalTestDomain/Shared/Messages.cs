
namespace LibeyTechnicalTestDomain.Shared
{
    internal class Messages
    {

        #region Commons
        public static string ErrorInternalServer => "Error interno del servidor.";
        public static string NotFoundFemale => "No se encontró la {0}.";
        public static string NotFoundMale => "No se encontró el {0}.";

        public const string RequiredField = "El campo {0} es requerido.";

        public const string MaxLength = "El campo {0} no puede tener más de {1} caracteres.";

        public const string MinLength = "El campo {0} no puede tener menos de {1} caracteres.";

        public const string ExactLength = "El campo {0} debe tener {1} caracteres.";

        public const string InvalidFormat = "El campo {0} tiene un formato inválido.";

        public const string InvalidEmail = "El campo {0} no tiene un formato de correo válido.";

        public const string InvalidPhone = "El campo {0} no tiene un formato de teléfono válido.";

        public const string InvalidDateTime = "El campo {0} no tiene un formato de fecha y hora válido.";

        public static string NotFoundAllFemale => "No se encontraron {0}.";
        public static string NotFoundAllMale => "No se encontraron {0}.";
        #endregion


        #region Transactions
        public const string SavedSuccessfullyFemale = "Se guardó correctamente la {0}.";
        public const string SavedSuccessfullyMale = "Se guardó correctamente el {0}.";
        public const string UpdatedSuccessfullyFemale = "Se actualizó correctamente la {0}.";
        public const string UpdatedSuccessfullyMale = "Se actualizó correctamente el {0}.";
        public const string DeletedSuccessfullyFemale = "Se eliminó correctamente la {0}.";
        public const string DeletedSuccessfullyMale = "Se eliminó correctamente el {0}.";


        public const string ErrorCreate = "Error al crear {0}.";
        public const string ErrorUpdate = "Error al actualizar {0}.";
        public const string ErrorDelete = "Error al eliminar {0}.";
        #endregion

        #region Tablas
        public const string LibeyUser = "LibeyUser";
        public const string Departamento = "Departamento";
        public const string Provincia = "Provincia";
        public const string Distrito = "Distrito";
        public const string Ubigeo = "Ubigeo";
        #endregion

        #region Actions

        public const string Insert = "Crear";
        public const string Update = "Actualizar";

        #endregion

        #region Campos

        public const string DocumentNumber = "N\u00famero de documento";
        public const string Codigo = "C\u00f3digo";
        public const string Name = "Nombre";
        public const string FathersLastName = "Apellido paterno";
        public const string MothersLastName = "Apellido materno";
        public const string Address = "Direcci\u00f3n";
        public const string UbigeoCode = "C\u00f3digo de ubigeo";
        public const string Phone = "Tel\u00e9fono";
        public const string Email = "Correo electr\u00f3nico";
        public const string Password = "Contrase\u00f1a";
        public const string DocumentTypeId = "Tipo de documento";

        #endregion
    }
}
