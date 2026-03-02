using DrugEmpire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.interfaces
{
    public interface IPaymentTransactionService
    {
        Task<IEnumerable<PaymentTransactionDTOResponse>> GetAllPaymentTransactions();
        Task<PaymentTransactionDTOResponse> GetPaymentTransactionById(int id);
        Task<PaymentTransactionDTOResponse> CreatePaymentTransaction(PaymentTransactionDTORequest paymentTransactionDtoRequest);
        Task<PaymentTransactionDTOResponse> UpdatePaymentTransaction(int id, PaymentTransactionDTORequest paymentTransactionDtoRequest);
        Task<bool> DeletePaymentTransaction(int id);
    }
}
