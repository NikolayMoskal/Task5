using System;
using System.IO;
using BusinessLayer.Parsers;
using BusinessLayer.UnitsOfWork;
using DataAccessLayer.Entities;

namespace BusinessLayer
{
    public class FileHandler
    {
        private readonly IParser<CsvLine> _parser;

        public FileHandler(IParser<CsvLine> parser)
        {
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        public string FileName { get; set; }

        public void StartHandle()
        {
            var lines = _parser.ParseFile(FileName);
            foreach (var line in lines)
                using (var uow = new UnitOfWork())
                {
                    try
                    {
                        uow.BeginTransaction();
                        var client = new Client {Name = line.ClientName};
                        uow.ClientRepository.Save(client);

                        var employee = new Employee {Name = line.EmployeeName};
                        uow.EmployeeRepository.Save(employee);

                        var product = new Product {Name = line.ProductName, Price = line.ProductSum};
                        uow.ProductRepository.Save(product);

                        var booking = new Booking
                        {
                            Date = line.Date,
                            Client = client,
                            Employee = employee,
                            Product = product
                        };
                        uow.BookingRepository.Save(booking);
                    }
                    catch
                    {
                        uow.RollbackTransaction();
                    }
                }

            File.Move(FileName, FileName + "_");
        }
    }
}