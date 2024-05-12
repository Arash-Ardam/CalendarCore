namespace CalendarRestApi.IdentityConfigs
{
    public class AuthorizationConsts
    {
        public const string AdministrationPolicy = "Administrator";
        public const string AdminRole = "Administrator";
        public const string AdminScope = "TadbirPay.Administrator.Api.Scope";

        public const string GatewayClient = "gateway-client";
        public const string GatewayScope = "TadbirPay.Gateway.Api.Scope";


        public const string WithdrawalClientPolicy = "withdrawal-client";
        public const string WithdrawalScope = "TadbirPay.Withdrawal.Api.Scope";
        public const string WithdrawalClientRole = "Withdrawal.client.role";




        public const string CartableAdminPolicy = "CartableAdmin";
        public const string CartableApproverPolicy = "CartableApprover";
        public const string CartableUserPolicy = "CartableUser";


        public const string CartableAdminRole = "cartableadmin.role";
        public const string CartableApproverRole = "cartableapprover.role";

        public const string CartableApiScope = "TadbirPay.Cartable.Api.Scope";
    }
}
