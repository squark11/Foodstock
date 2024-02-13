import 'package:flutter/material.dart';
import 'package:foodstock_mobile/models/order.dart';
import 'package:foodstock_mobile/models/organization.dart';
import 'package:foodstock_mobile/models/supplier.dart';
import 'package:foodstock_mobile/services/order_service.dart';
import 'package:foodstock_mobile/services/organization_service.dart';
import 'package:foodstock_mobile/services/supplier_service.dart';
import 'package:foodstock_mobile/models/user.dart';
import 'package:foodstock_mobile/screens/order/order_details_screen.dart';

class OrderScreen extends StatefulWidget {
  final User user;
  const OrderScreen({super.key, required this.user});
  @override
  OrderScreenState createState() => OrderScreenState();
}

class OrderScreenState extends State<OrderScreen> {
  final OrderService _orderService = OrderService();
  final SupplierService _supplierService = SupplierService();
  final OrganizationService _organizationService = OrganizationService();
  List<Order> _orders = [];
  List<Supplier> _suppliers = [];
  Organization? _organization;
  bool _isLoading = true;
  bool isOrganization() {
    return _organization != null;
  }

  bool isSupplier(User user) {
    return user.roleName.toLowerCase() == 'supplier';
  }

  bool isAdmin(User user) {
    return user.roleName.toLowerCase() == 'admin';
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _loadOrders();
      _loadSuppliers();
      if (!isSupplier(widget.user)) {
        _loadOrganization();
      }
    });
  }

  void _loadOrders() async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      _orders = await _orderService.getOrders();
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load orders: $e')),
      );
    } finally {
      setState(() => _isLoading = false);
    }
  }

  void _loadSuppliers() async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      _suppliers = await _supplierService.getSuppliers();
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load orders: $e')),
      );
    }
  }

  void _loadOrganization() async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      _organization = await _organizationService.getOrganization();
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load organization: $e')),
      );
    }
  }

  void _showAddOrderDialog() {
    String? selectedSupplierId;
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    showDialog(
      context: context,
      builder: (BuildContext dialogContext) {
        return AlertDialog(
          title: const Text('Add New Order'),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                DropdownButtonFormField<String>(
                  value: selectedSupplierId,
                  onChanged: (String? newValue) {
                    selectedSupplierId = newValue;
                  },
                  items: _suppliers
                      .map<DropdownMenuItem<String>>((Supplier supplier) {
                    return DropdownMenuItem<String>(
                      value: supplier.id,
                      child: Text(supplier.name),
                    );
                  }).toList(),
                  decoration: const InputDecoration(labelText: 'Supplier'),
                ),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              child: const Text('Cancel'),
              onPressed: () => Navigator.of(dialogContext).pop(),
            ),
            TextButton(
              child: const Text('Add'),
              onPressed: () async {
                if (selectedSupplierId != null) {
                  await _createOrder(selectedSupplierId!);
                  if (mounted) {
                    Navigator.of(dialogContext).pop();
                  }
                } else {
                  scaffoldMessenger.showSnackBar(
                    const SnackBar(content: Text('Failed selected supplier')),
                  );
                }
              },
            ),
          ],
        );
      },
    );
  }

  Future<void> _createOrder(String supplierId) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    if (!isOrganization()) {
      scaffoldMessenger.showSnackBar(
        const SnackBar(content: Text('Must add organization')),
      );
      return;
    }
    try {
      Supplier newSupplier = Supplier(id: supplierId, name: "");
      await _orderService.createOrder(newSupplier);
      _loadOrders();
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to add order: $e')),
      );
    }
  }

  void _confirmDeleteOrder(String orderId) {
    showDialog(
      context: context,
      builder: (BuildContext dialogContext) {
        return AlertDialog(
          title: const Text("Confirm Delete"),
          content: const Text("Are you sure you want to delete this order?"),
          actions: <Widget>[
            TextButton(
              child: const Text("Cancel"),
              onPressed: () => Navigator.of(dialogContext).pop(),
            ),
            TextButton(
              child: const Text("Delete"),
              onPressed: () async {
                await _deleteOrder(orderId);
                if (mounted) {
                  Navigator.of(dialogContext).pop();
                }
              },
            ),
          ],
        );
      },
    );
  }

  Future<void> _deleteOrder(String orderId) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      await _orderService.deleteOrder(orderId);
      _loadOrders();
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to delete order: $e')),
      );
    }
  }

  void _showEditStatusDialog(Order order) {
    showDialog(
      context: context,
      builder: (BuildContext dialogContext) {
        String? selectedStatus = order.orderStatus;
        return AlertDialog(
          title: const Text('Edit Order Status'),
          content: DropdownButtonFormField<String>(
            value: selectedStatus,
            onChanged: (String? newValue) {
              selectedStatus = newValue;
            },
            items: <String>['New', 'In Progress', 'Completed', 'Cancelled']
                .map<DropdownMenuItem<String>>((String value) {
              return DropdownMenuItem<String>(
                value: value,
                child: Text(value),
              );
            }).toList(),
          ),
          actions: <Widget>[
            TextButton(
              child: const Text('Cancel'),
              onPressed: () => Navigator.of(dialogContext).pop(),
            ),
            TextButton(
              child: const Text('Save'),
              onPressed: () async {
                if (selectedStatus != null &&
                    selectedStatus != order.orderStatus) {
                  await _updateOrderStatus(order.id, selectedStatus!);
                  if (mounted) {
                    Navigator.of(dialogContext).pop();
                  }
                }
              },
            ),
          ],
        );
      },
    );
  }

  Future<void> _updateOrderStatus(String orderId, String newStatus) async {
    try {
      bool success = await _orderService.updateOrderStatus(orderId, newStatus);
      if (!mounted) return;

      if (success) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Order status updated successfully')),
        );
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Failed to update order status')),
        );
      }
    } catch (e) {
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Error updating order status: $e')),
      );
    }

    if (mounted) _loadOrders();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Orders'),
      ),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : ListView.builder(
              itemCount: _orders.length,
              itemBuilder: (context, index) {
                final order = _orders[index];
                return Card(
                  margin: const EdgeInsets.all(8.0),
                  child: ListTile(
                    title: RichText(
                      text: TextSpan(
                        text: '${order.orderName} ',
                        style: const TextStyle(
                          fontWeight: FontWeight.normal,
                          color: Colors.black,
                          fontSize: 14,
                        ),
                        children: <TextSpan>[
                          TextSpan(
                            text: '(${order.orderStatus})',
                            style: const TextStyle(
                              fontWeight: FontWeight.bold,
                              color: Colors.black,
                            ),
                          ),
                        ],
                      ),
                    ),
                    subtitle: Text.rich(
                      TextSpan(
                        text: 'Supplier: ',
                        style: const TextStyle(fontWeight: FontWeight.normal),
                        children: <TextSpan>[
                          TextSpan(
                            text: order.supplier.name,
                            style: const TextStyle(fontWeight: FontWeight.bold),
                          ),
                        ],
                      ),
                    ),
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => OrderDetailsScreen(
                              orderId: order.id, user: widget.user),
                        ),
                      );
                    },
                    trailing: Row(
                      mainAxisSize: MainAxisSize.min,
                      children: <Widget>[
                        if (isSupplier(widget.user) || isAdmin(widget.user))
                          IconButton(
                            icon: const Icon(Icons.edit,
                                color: Color.fromARGB(255, 21, 121, 12)),
                            onPressed: () {
                              _showEditStatusDialog(order);
                            },
                          ),
                        if (isAdmin(widget.user) ||
                            (isSupplier(widget.user) &&
                                (order.orderStatus.toLowerCase() ==
                                        'completed' ||
                                    isSupplier(widget.user) &&
                                        order.orderStatus.toLowerCase() ==
                                            'cancelled')))
                          IconButton(
                            icon: const Icon(Icons.delete,
                                color: Color.fromARGB(255, 146, 17, 8)),
                            onPressed: () => _confirmDeleteOrder(order.id),
                          ),
                      ],
                    ),
                  ),
                );
              },
            ),
      floatingActionButton: !isSupplier(widget.user)
          ? FloatingActionButton(
              onPressed: _showAddOrderDialog,
              child: const Icon(Icons.add),
            )
          : null,
    );
  }
}
