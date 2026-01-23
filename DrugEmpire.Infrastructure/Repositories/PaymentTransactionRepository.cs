using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DrugEmpire.Infrastructure.Repositories
{
    public class PaymentTransactionRepository:IPaymentTransAction
    {
        private readonly DatabaseContext _context;
        public PaymentTransactionRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<PaymentTransaction>> GetPaymentTransactionsAsync()
        {
            return await _context.PaymentTransactions.ToListAsync();
        }
        public async Task<List<PaymentTransaction>> GetAllPaymentTransactionsAsync(int UserId)
        {
            var paymentTransactions = await _context.PaymentTransactions
                .Where(pt => pt.Order.UserId == UserId)
                .ToListAsync();
            return paymentTransactions;
        }
        public async Task<PaymentTransaction> GetPaymentTransactionByIdAsync(int id)
        {
            var existingTransaction = await _context.PaymentTransactions.FirstOrDefaultAsync();
            if(existingTransaction == null)
            {
                throw new Exception("Transaction not found");
            }
            return existingTransaction;
        }
        public async Task<PaymentTransaction> CreatePaymentTransactionAsync(PaymentTransaction paymentTransaction)
        {
            _context.PaymentTransactions.Add(paymentTransaction);
            await _context.SaveChangesAsync(); 
            return paymentTransaction;
        }
        public async Task<PaymentTransaction> UpdatePaymentTransActionAsync(int id, PaymentTransaction paymentTransaction)
        {
            var existingTransaction = await _context.PaymentTransactions.FindAsync(id);
            if (existingTransaction == null)
            {
                throw new Exception("Transaction not found");
            }
            existingTransaction.PaymentTransactionId = paymentTransaction.OrderId;
            existingTransaction.Amount = paymentTransaction.Amount;
            existingTransaction.Status = paymentTransaction.Status;
            existingTransaction.Provider = paymentTransaction.Provider;
            existingTransaction.CreatedAt = paymentTransaction.CreatedAt;
            await _context.SaveChangesAsync();
            return existingTransaction;
        }

        public async Task<bool> DeletePaymentTransActionAsync(int id)
        {
            var existingTransaction = await _context.PaymentTransactions.FindAsync(id);
            if(existingTransaction == null)
            {
                throw new Exception("Transaction ID not found");
            }
            _context.Remove(existingTransaction);
            return true;
        }
    }
}
