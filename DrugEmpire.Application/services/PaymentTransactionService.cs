using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.services
{
    public class PaymentTransactionService : IPaymentTransactionService
    {
        private readonly IPaymentTransAction _PaymentTransactionRepository;

        public PaymentTransactionService(IPaymentTransAction paymentTransactionRepository)
        {
            _PaymentTransactionRepository = paymentTransactionRepository;
        }

        public async Task<IEnumerable<PaymentTransactionDTOResponse>> GetAllPaymentTransactions()
        {
            var transactions = await _PaymentTransactionRepository.GetPaymentTransactionsAsync();

            return transactions.Select(t => new PaymentTransactionDTOResponse
            {
                PaymentTransactionId = t.PaymentTransactionId,
                OrderId = t.OrderId,
                Amount = t.Amount,
                Status = t.Status,
                Provider = t.Provider,
                ProviderReference = t.ProviderReference,
                CreatedAt = t.CreatedAt
            });
        }

        public async Task<PaymentTransactionDTOResponse> GetPaymentTransactionById(int id)
        {
            var transaction = await _PaymentTransactionRepository.GetPaymentTransactionByIdAsync(id);
            if (transaction == null)
                throw new Exception("Payment transaction not found");

            return new PaymentTransactionDTOResponse
            {
                PaymentTransactionId = transaction.PaymentTransactionId,
                OrderId = transaction.OrderId,
                Amount = transaction.Amount,
                Status = transaction.Status,
                Provider = transaction.Provider,
                ProviderReference = transaction.ProviderReference,
                CreatedAt = transaction.CreatedAt
            };
        }

        public async Task<PaymentTransactionDTOResponse> CreatePaymentTransaction(PaymentTransactionDTORequest paymentTransactionDtoRequest)
        {
            if (paymentTransactionDtoRequest == null)
                throw new ArgumentNullException(nameof(paymentTransactionDtoRequest));

            if (paymentTransactionDtoRequest.OrderId <= 0)
                throw new Exception("OrderId is required");

            if (paymentTransactionDtoRequest.Amount <= 0)
                throw new Exception("Amount must be greater than 0");

            if (string.IsNullOrWhiteSpace(paymentTransactionDtoRequest.Status))
                throw new Exception("Status is required");

            if (string.IsNullOrWhiteSpace(paymentTransactionDtoRequest.Provider))
                throw new Exception("Provider is required");

            var entity = new PaymentTransaction
            {
                OrderId = paymentTransactionDtoRequest.OrderId,
                Amount = paymentTransactionDtoRequest.Amount,
                Status = paymentTransactionDtoRequest.Status,
                Provider = paymentTransactionDtoRequest.Provider,
                ProviderReference = paymentTransactionDtoRequest.ProviderReference,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _PaymentTransactionRepository.CreatePaymentTransactionAsync(entity);

            return new PaymentTransactionDTOResponse
            {
                PaymentTransactionId = created.PaymentTransactionId,
                OrderId = created.OrderId,
                Amount = created.Amount,
                Status = created.Status,
                Provider = created.Provider,
                ProviderReference = created.ProviderReference,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task<PaymentTransactionDTOResponse> UpdatePaymentTransaction(int id, PaymentTransactionDTORequest paymentTransactionDtoRequest)
        {
            if (paymentTransactionDtoRequest == null)
                throw new ArgumentNullException(nameof(paymentTransactionDtoRequest));

            if (paymentTransactionDtoRequest.Amount <= 0)
                throw new Exception("Amount must be greater than 0");

            if (string.IsNullOrWhiteSpace(paymentTransactionDtoRequest.Status))
                throw new Exception("Status is required");

            if (string.IsNullOrWhiteSpace(paymentTransactionDtoRequest.Provider))
                throw new Exception("Provider is required");

            var existing = await _PaymentTransactionRepository.GetPaymentTransactionByIdAsync(id);
            if (existing == null)
                throw new Exception("Payment transaction not found");

            // Whitelist felter (typisk ændrer man status/reference, sjældnere amount/provider)
            existing.Amount = paymentTransactionDtoRequest.Amount;
            existing.Status = paymentTransactionDtoRequest.Status;
            existing.Provider = paymentTransactionDtoRequest.Provider;
            existing.ProviderReference = paymentTransactionDtoRequest.ProviderReference;

            var updated = await _PaymentTransactionRepository.UpdatePaymentTransActionAsync(id, existing);

            return new PaymentTransactionDTOResponse
            {
                PaymentTransactionId = updated.PaymentTransactionId,
                OrderId = updated.OrderId,
                Amount = updated.Amount,
                Status = updated.Status,
                Provider = updated.Provider,
                ProviderReference = updated.ProviderReference,
                CreatedAt = updated.CreatedAt
            };
        }

        public async Task<bool> DeletePaymentTransaction(int id)
        {
            var existing = await _PaymentTransactionRepository.GetPaymentTransactionByIdAsync(id);
            if (existing == null)
                throw new Exception("Payment transaction not found");

            await _PaymentTransactionRepository.DeletePaymentTransActionAsync(id);
            return true;
        }
    }
}