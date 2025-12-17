using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Utility
{
    public static class SD
    {
        // Roles that different users can have.
        
        // Customer is a user that registers an account and can make purchases
        public const string Role_Customer = "Customer";

        // Company is a user but is registered by admin and has exceptions regarding purchasing where they can pay within 30 days instead of immediately
        public const string Role_Company = "Company";

        // Admin is user with full permissions for crud operations
        public const string Role_Admin = "Admin";

        // Employee is a user that has limited permissions such as modifying shipping of product and other details
        public const string Role_Employee = "Employee";

        // Constant strings for the status of the order
        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        // Constant strings for the payment status of the order
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
        public const string PaymentStatusRejected = "Rejected";

        // Constant string to note the shopping cart session key.
        public const string SessionCart = "SessionShoppingCart";
    }
}
