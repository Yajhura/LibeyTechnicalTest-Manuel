using LibeyTechnicalTestDomain.Abstractions;
using LibeyTechnicalTestDomain.Shared;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LibeyTechnicalTestDomain.Validations
{
    public sealed class ValidatorService
    {
        private readonly HashSet<string> _errores = new HashSet<string>();
        private static readonly Regex SqlInjectionRegex = new Regex(@"(INSERT(\s+INTO)?|UPDATE|DELETE|SELECT|DROP|ALTER|CREATE|TRUNCATE|EXEC(UTE)?|UNION|CAST|CONVERT|SUBSTRING|CHAR|UNICODE|FROM|WHERE|AND|OR|--|;|'|""|\*|\/\*|\*\/|GRANT|REVOKE|DENY|DBCC|SERVER ROLE|EVENT SESSION|LOGIN|USER)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex XssRegex = new Regex(@"(<script.*?>.*?<\/script>|<.*?on\w+\s*=.*?>|javascript:|<.*?>|&lt;|&gt;|&amp;|['""]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public void ValidateEntity<T>(T enitity, string accion) where T : class, new()
        {
            if (enitity == null) return;

            var type = typeof(T);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                ValidProperties(property, enitity, accion);
            }
        }

        private void ValidProperties<T>(PropertyInfo property, T entidad, string accion) where T : class
        {
            var atributos = property.GetCustomAttributes(true);

            foreach (var atributo in atributos)
            {
                if (atributo is TextValidator validadorTexto)
                {
                    ValidTextProperties(property, entidad, validadorTexto, accion);
                }
            }
        }

        private void ValidTextProperties<T>(PropertyInfo property, T entidad, TextValidator validador, string accion) where T : class
        {
            if (property.PropertyType != typeof(string)) return;

            var value = property.GetValue(entidad) as string;
            if (string.IsNullOrEmpty(value)) return;


            bool isRequeridFiled = validador._isRequeridFiled;

            if (string.IsNullOrEmpty(validador._action))
            {
                isRequeridFiled = true;
            }
            else
            {
                if (validador._action != accion)
                {
                    isRequeridFiled = false;
                }
            }


            string messageEntity = string.Empty;

            var getValueMessages = typeof(Messages).GetProperty(validador._messageEntity);

            if (getValueMessages != null)
            {
                var message = getValueMessages.GetValue(null, null);
                if (message != null)
                {
                    messageEntity = message.ToString()!;
                }
                else
                {
                    messageEntity = FormatProperty(property.Name);
                }
            }


            ValidRequiredField(property, value, validador._isRequeridFiled, messageEntity, isRequeridFiled);
            ValidLength(property, value, validador._minLength, validador._maxLength, messageEntity);
            ValidSqlInjection(property, value, validador._validSQL, entidad, messageEntity);
            ValidXss(property, value, validador._validXSS, entidad, messageEntity);
        }

        private void ValidRequiredField(PropertyInfo property, string value, bool esObligatorio, string messageEntity, bool accion)
        {
            if (accion)
            {
                if (esObligatorio && string.IsNullOrEmpty(value))
                {
                    AddError(string.Format(Messages.RequiredField, messageEntity));
                }

            }

        }

        private void ValidLength(PropertyInfo property, string value, int minLongitud, int maxLongitud, string messageEntity)
        {
            if (minLongitud > 0 && value.Length < minLongitud)
            {
                AddError(string.Format(Messages.MinLength, messageEntity, minLongitud));
            }

            if (maxLongitud > 0 && value.Length > maxLongitud)
            {
                AddError(string.Format(Messages.MaxLength, messageEntity, maxLongitud));
            }
        }

        private void ValidSqlInjection<T>(PropertyInfo propiedad, string value, bool validSQL, T entidad, string messageEntity) where T : class
        {
            if (!validSQL) return;

            var cleanedValue = value.Replace(" ", string.Empty);
            if (SqlInjectionRegex.IsMatch(cleanedValue))
            {
                AddError(string.Format(Messages.InvalidFormat, messageEntity));
                var sanitizedValue = SqlInjectionRegex.Replace(value, string.Empty);
                propiedad.SetValue(entidad, sanitizedValue);
            }
        }

        private void ValidXss<T>(PropertyInfo property, string value, bool validXSS, T entidad, string messageEntity) where T : class
        {
            if (!validXSS) return;

            var cleanedValue = value.Replace(" ", string.Empty);
            if (XssRegex.IsMatch(cleanedValue))
            {
                AddError(string.Format(Messages.InvalidFormat, messageEntity));
                var sanitizedValue = XssRegex.Replace(value, string.Empty);
                property.SetValue(entidad, sanitizedValue);
            }
        }

        public Result GetFinalStatus()
        {
            if (_errores.Count > 0)
            {
                return Result.Failure(string.Join(" ", _errores));
            }

            CleanErrors();

            return Result.Success(string.Empty);
        }

        public void AddError(string error) => _errores.Add(error);

        public void CleanErrors() => _errores.Clear();

        public bool HasErrors() => _errores.Count > 0;

        public string ValidText(string texto, string nombreCampo)
        {
            if (string.IsNullOrEmpty(texto)) return string.Empty;

            if (SqlInjectionRegex.IsMatch(texto))
            {
                AddError(string.Format(Messages.InvalidFormat, nombreCampo));
                texto = SqlInjectionRegex.Replace(texto, string.Empty);
            }
            else if (XssRegex.IsMatch(texto))
            {
                AddError(string.Format(Messages.InvalidFormat, nombreCampo));
                texto = XssRegex.Replace(texto, string.Empty);
            }

            return texto;
        }

        public Result ValidateId(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                AddError(string.Format(Messages.RequiredField, Messages.DocumentNumber));
            }
            else if (SqlInjectionRegex.IsMatch(id) || XssRegex.IsMatch(id))
            {
                AddError(string.Format(Messages.InvalidFormat, Messages.DocumentNumber));
                id = SqlInjectionRegex.Replace(XssRegex.Replace(id, string.Empty), string.Empty);
            }

            return GetFinalStatus();
        }

        public Result ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                AddError(string.Format(Messages.RequiredField, Messages.DocumentNumber));
            }
            else if (!EmailRegex.IsMatch(email))
            {
                AddError(string.Format(Messages.InvalidEmail, Messages.DocumentNumber));
            }

            return GetFinalStatus();
        }

        private static string FormatProperty(string property) =>
            char.ToUpper(property[0]) + property.Substring(1);

    }
}
