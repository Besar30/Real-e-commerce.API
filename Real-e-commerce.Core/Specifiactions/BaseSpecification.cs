using Real_e_commerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Specifiactions
{
    public class BaseSpecification<T>(Expression<Func<T,bool>>? criteria) : ISpecifiaction<T>
    {
        protected BaseSpecification() : this(null)
        {

        }
        public Expression<Func<T, bool>>? Criteria => criteria;

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public bool IsDistinct { get; private set; }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) {
            OrderBy = orderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
        protected void ApplyDistinct()
        {
            IsDistinct = true;
        }
    }
    public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria) : BaseSpecification<T>(criteria), ISpecifiaction<T, TResult> {
        public Expression<Func<T, TResult>>? Select { get; private set; }
        protected BaseSpecification() : this(null!)
        {

        }
        protected void AddSelect(Expression<Func<T,TResult>> selectExpression)
        {
            Select=selectExpression;
        }
    }
}
