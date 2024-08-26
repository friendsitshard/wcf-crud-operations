using MidDAL;
using MidtermWcfService.DataContracts;
using MidtermWcfService.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Transactions;

namespace MidtermWcfService
{
    public class MidService : IMidService
    {
        public List<UserDTO> showUsers()
        {
            try
            {
                using (var context = new MidModel())
                {
                    var data = context.Users.ToList();
                    return AutoMapperConfig.Mapper.Map<List<UserDTO>>(data);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error: {ex.Message}");
            }
        }

        public void updateUser(int id, string email, string password)
        {
            try
            {
                using (var db = new MidModel())
                {
                    User user = db.Users.Find(id);

                    user.email = email;
                    user.password = password;

                    db.SaveChanges();
                }
            }
            catch (FaultException ex)
            {
                throw new FaultException($"Error: {ex.Message}");
            }
        }

        public void insertUser(string email, string password)
        {
            try
            {
                using (var db = new MidModel())
                {
                    User user = new User();

                    user.email = email;
                    user.password = password;
                    user.orders_quantity = 0;
                    db.Users.Add(user);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new FaultException($"Error: {ex.Message}");
            }

        }

        public void deleteUser(int id)
        {
            try
            {
                using (var db = new MidModel())
                {
                    User user = db.Users.Find(id);

                    db.Users.Remove(user);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error: {ex.Message}");
            }
        }

        public List<OrderDTO> showOrders()
        {
            try
            {
                using (var db = new MidModel())
                {
                    var data = db.Orders.ToList();
                    return AutoMapperConfig.Mapper.Map<List<OrderDTO>>(data);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error: {ex.Message}");
            }
            
        }

        public void updateOrder(int id, string name, string price, int user_id)
        {
            try
            {
                using (var db = new MidModel())
                {
                    Order order = db.Orders.Find(id);

                    order.name = name;
                    order.price = price;
                    order.user_id = user_id;

                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                throw new FaultException($"Error: {ex.Message}");
            }
        }

        public void deleteOrder(int id)
        {
            try
            {
                using (var db = new MidModel())
                {
                    Order order = db.Orders.Find(id);

                    db.Orders.Remove(order);

                    db.SaveChanges();
                }
                showUsers();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error: {ex.Message}");
            }
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void insertOrder(string name, string price, int user_id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var db = new MidModel())
                    {
                        Order order = new Order();

                        order.name = name;
                        order.price = price;
                        order.user_id = user_id;
                        db.Orders.Add(order);

                        db.SaveChanges();
                    }
                    updateUsersOrdersQty(user_id);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                throw new FaultException($"Error: {ex.Message}");
            }
        }

        public void updateUsersOrdersQty(int id)
        {
            try
            {
                using (var db = new MidModel())
                {
                    User user = db.Users.Find(id);
                    user.orders_quantity++;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error: {ex.Message}");
            }
        }

        public UserDTO getUser(int id) 
        {
            try
            {
                using (var db = new MidModel())
                {
                    var data = db.Users.Find(id);
                    return AutoMapperConfig.Mapper.Map<UserDTO>(data);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error: {ex.Message}");
            }
        }

        public OrderDTO getOrder(int id)
        {
            try
            {
                using (var db = new MidModel())
                {
                    var data = db.Orders.Find(id);
                    return AutoMapperConfig.Mapper.Map<OrderDTO>(data);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error: {ex.Message}");
            }
        }
    }
}
