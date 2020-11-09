using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Data.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Payment Received")]
        PaymentReceived,
        [EnumMember(Value = "Payment Faild")]
        PaymentFaild
    }
}
