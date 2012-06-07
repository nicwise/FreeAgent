using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using FreeAgent.Models;

namespace FreeAgent
{
    public class ExpenseClient : ResourceClient<ExpenseWrapper, ExpensesWrapper, Expense>
    {
        public ExpenseClient(FreeAgentClient client) : base(client) {}

        public override string ResouceName { get { return "expenses"; } } 

        public override ExpenseWrapper WrapperFromSingle(Expense single)
        {
            return new ExpenseWrapper { expense = single };
        }
        public override IEnumerable<Expense> ListFromWrapper(ExpensesWrapper wrapper)
        {
            return wrapper.expenses;
        }

        public override Expense SingleFromWrapper(ExpenseWrapper wrapper)
        {
            return wrapper.expense;
        }

        
        
        
    }
}

