using System;
using System.IO;
using System.Threading;
using BusinessLayer.Parsers;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer
{
    public class FileHandler
    {
        private readonly string _fileName;
        private readonly ReaderWriterLockSlim _locker;
        private readonly IParser<CsvLine> _parser;

        public FileHandler(string fileName, IParser<CsvLine> parser)
        {
            _fileName = fileName;
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _locker = new ReaderWriterLockSlim();
        }

        public void StartHandle()
        {
            var lines = _parser.ParseFile(_fileName);
            foreach (var line in lines)
                using (var uow = new UnitOfWork())
                {
                    try
                    {
                        _locker.EnterWriteLock();
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
                    finally
                    {
                        _locker.ExitWriteLock();
                    }
                }

            File.Move(_fileName, _fileName + "_");
        }

        ~FileHandler()
        {
            _locker?.Dispose();
        }
    }
}