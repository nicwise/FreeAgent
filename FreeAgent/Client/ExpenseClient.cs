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

        /// <summary>
        /// All the specified view, from_date and to_date.
        /// </summary>
        /// <param name='view'>
        /// View. - recent or recurring
        /// </param>
        /// <param name='from_date'>
        /// From_date.
        /// </param>
        /// <param name='to_date'>
        /// To_date.
        /// </param>
        /// 
        public IEnumerable<Expense> All(string view = "", string from_date = "", string to_date = "")
        {
            return All((r) => {
                if (!string.IsNullOrEmpty(from_date))
                {
                    r.AddParameter("from_date", from_date, ParameterType.GetOrPost);
                }
                if (!string.IsNullOrEmpty(to_date))
                {
                    r.AddParameter("to_date", to_date, ParameterType.GetOrPost);
                }
                if (!string.IsNullOrEmpty("view"))
                {
                    r.AddParameter("view", view, ParameterType.GetOrPost);
                }
            });

        }
        
        
    }
}

