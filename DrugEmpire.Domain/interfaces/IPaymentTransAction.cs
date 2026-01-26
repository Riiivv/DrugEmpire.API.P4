using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.interfaces
{
    public interface IPaymentTransAction
    {
        Task<List<PaymentTransaction>> GetPaymentTransactionsAsync();
        Task<PaymentTransaction> GetPaymentTransactionByIdAsync(int id);
        Task<PaymentTransaction> CreatePaymentTransactionAsync(PaymentTransaction paymentTransaction);
        Task<PaymentTransaction> UpdatePaymentTransActionAsync(int id, PaymentTransaction paymentTransaction);
        Task<bool> DeletePaymentTransActionAsync(int id);
    }
}
