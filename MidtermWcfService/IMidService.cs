using MidtermWcfService.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MidtermWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMidService" in both code and config file together.
    [ServiceContract]
    public interface IMidService
    {
        [OperationContract]
        List<UserDTO> showUsers();
        [OperationContract]
        void updateUser(int id, string email, string password);
        [OperationContract]
        void insertUser(string email, string password);
        [OperationContract]
        void deleteUser(int id);

        [OperationContract]
        List<OrderDTO> showOrders();
        [OperationContract]
        void updateOrder(int id, string name, string price, int user_id);
        [OperationContract]
        void deleteOrder(int id);
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void insertOrder(string name, string price, int user_id);
        [OperationContract]
        void updateUsersOrdersQty(int id);
        [OperationContract]
        UserDTO getUser(int id);
        [OperationContract]
        OrderDTO getOrder(int id);
    }
}
