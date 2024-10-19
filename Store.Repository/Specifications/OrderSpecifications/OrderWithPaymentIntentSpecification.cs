using Store.Data.Entities.OrderEntities;
using System.Linq.Expressions;

namespace Store.Repository.Specifications.OrderSpecifications
{
    public class OrderWithPaymentIntentSpecification : BaseSpecification<Order>
    {
        public OrderWithPaymentIntentSpecification(string? paymentintentId)
            : base(order => order.PaymentIntentId == paymentintentId)
        {
        }
    }
}
