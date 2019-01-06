using System;
using DataAccessLayer.Configurations;
using DataAccessLayer.Repositories;
using NHibernate;

namespace BusinessLayer.UnitsOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly bool _isPassedSession;
        private BookingRepository _bookingRepository;
        private ClientRepository _clientRepository;
        private EmployeeRepository _employeeRepository;
        private bool _isDisposed;
        private ProductRepository _productRepository;
        private ISession _session;
        private ITransaction _transaction;

        public UnitOfWork()
        {
            _session = NHibernateConfiguration.OpenSession();
        }

        public UnitOfWork(ISession session)
        {
            if (session != null)
            {
                _session = session.IsOpen ? session : throw new ObjectDisposedException(nameof(session));
                _isPassedSession = true;
            }
            else
            {
                _session = NHibernateConfiguration.OpenSession();
            }
        }

        public ClientRepository ClientRepository =>
            _clientRepository ?? (_clientRepository = new ClientRepository(_session));

        public EmployeeRepository EmployeeRepository =>
            _employeeRepository ?? (_employeeRepository = new EmployeeRepository(_session));

        public ProductRepository ProductRepository =>
            _productRepository ?? (_productRepository = new ProductRepository(_session));

        public BookingRepository BookingRepository =>
            _bookingRepository ?? (_bookingRepository = new BookingRepository(_session));

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(_session));

            _transaction = _session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction == null) throw new ArgumentNullException(nameof(_transaction));

            _transaction.Commit();
            CloseTransaction();
        }

        public void RollbackTransaction()
        {
            if (_transaction == null) throw new ArgumentNullException(nameof(_transaction));

            _transaction.Rollback();
            CloseTransaction();
            CloseSession();
        }

        private void CloseTransaction()
        {
            _transaction.Dispose();
            _transaction = null;
        }

        private void CloseSession()
        {
            _session.Close();
            _session.Dispose();
            _session = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transaction != null) CommitTransaction();

                _session?.Flush();

                if (!_isPassedSession && _session != null) CloseSession();
            }

            _isDisposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}