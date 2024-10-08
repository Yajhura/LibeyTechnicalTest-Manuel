namespace LibeyTechnicalTestDomain.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class TextValidator : Attribute
    {
        public bool _validSQL { get; }
        public bool _validXSS { get; }
        public bool _isRequeridFiled { get; }
        public int _minLength { get; set; }
        public int _maxLength { get; set; }
        public string _messageEntity { get; set; }

        public string _action { get; set; }


        public TextValidator(bool validSQL = false, bool validXSS = false, bool isRequeridFiled = false, int minLength = 0, int maxLength = 0, string messageEntity = "", string action = "")
        {
            _validSQL = validSQL;
            _validXSS = validXSS;
            _isRequeridFiled = isRequeridFiled;
            _minLength = minLength;
            _maxLength = maxLength;
            _messageEntity = messageEntity;
            _action = action;
        }
    }
}
