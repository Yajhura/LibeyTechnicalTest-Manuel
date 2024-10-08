using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Domain
{
    public class LibeyUser
    {
        public string DocumentNumber { get; private set; }
        public int DocumentTypeId { get; private set; }
        public string Name { get; private set; }
        public string FathersLastName { get; private set; }
        public string MothersLastName { get; private set; }
        public string Address { get; private set; }
        public string UbigeoCode { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; private set; }

        private LibeyUser() { }


        private LibeyUser(string documentNumber, int documentTypeId, string name, string fathersLastName, string mothersLastName, string address,
        string ubigeoCode, string phone, string email, string password)
        {
            DocumentNumber = documentNumber;
            DocumentTypeId = documentTypeId;
            Name = name;
            FathersLastName = fathersLastName;
            MothersLastName = mothersLastName;
            Address = address;
            UbigeoCode = ubigeoCode;
            Phone = phone;
            Email = email;
            Password = password;
            Active = true;
        }


        public static LibeyUser Create(UserUpdateorCreateCommand command)
        {
            return new LibeyUser(command.DocumentNumber!, command.DocumentTypeId!, command.Name!, command.FathersLastName!, command.MothersLastName!, command.Address!,
            command.UbigeoCode!, command.Phone!, command.Email!, command.Password!);
        }


        public static LibeyUser Create(LibeyUserResponse command)
        {
            return new LibeyUser(command.DocumentNumber!, command.DocumentTypeId!, command.Name!, command.FathersLastName!, command.MothersLastName!, command.Address!,
            command.UbigeoCode!, command.Phone!, command.Email!, command.Password!);
        }

        public void Update(UserUpdateorCreateCommand command)
        {
            DocumentNumber = command.DocumentNumber!;
            Name = command.Name!;
            FathersLastName = command.FathersLastName!;
            MothersLastName = command.MothersLastName!;
            Address = command.Address!;
            UbigeoCode = command.UbigeoCode!;
            Phone = command.Phone!;
            Email = command.Email!;
            Password = command.Password!;
        }

        public void Delete() => Active = false;

        public void UpdatePassword(string password) => Password = password;

    }
}