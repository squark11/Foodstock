import 'package:flutter/material.dart';
import 'package:foodstock_mobile/models/order_item.dart';
import 'package:foodstock_mobile/models/producent.dart';
import 'package:foodstock_mobile/services/order_item_service.dart';
import 'package:foodstock_mobile/models/category.dart';
import 'package:foodstock_mobile/models/order.dart';
import 'package:foodstock_mobile/models/user.dart';
import 'package:foodstock_mobile/services/order_service.dart';
import 'package:foodstock_mobile/services/category_service.dart';
import 'package:foodstock_mobile/services/producent_service.dart';

class OrderDetailsScreen extends StatefulWidget {
  final String orderId;
  final User user;

  const OrderDetailsScreen(
      {super.key, required this.orderId, required this.user});

  @override
  OrderDetailScreenState createState() => OrderDetailScreenState();
}

class OrderDetailScreenState extends State<OrderDetailsScreen> {
  final OrderItemService _orderItemService = OrderItemService();
  final OrderService _orderService = OrderService();
  final CategoryService _categoryService = CategoryService();
  final ProducentService _producentService = ProducentService();
  List<OrderItem> _orderItems = [];
  List<Category> _categories = [];
  List<Producent> _producents = [];
  bool _isLoading = true;

  bool isSupplier(User user) {
    return user.roleName.toLowerCase() == 'supplier';
  }

  // Kontrolery dla formularza edycji OrderItem
  final TextEditingController _itemNameController = TextEditingController();
  final TextEditingController _quantityController = TextEditingController();
  final TextEditingController _barCodeController = TextEditingController();
  String? _selectedCategoryId;
  String? _selectedProducentId;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _loadOrderItems();
      _loadCategories();
      _loadProducents();
    });
  }

  void _loadOrderItems() async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      String orderId = widget.orderId;
      Order order = await _orderService.getOrderById(orderId);

      setState(() {
        _orderItems = order.orderItems;
        _isLoading = false;
      });
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load order items: $e')),
      );
      setState(() => _isLoading = false);
    }
  }

  void _loadCategories() async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      _categories = await _categoryService.getCategories();
      setState(() {});
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load categories: $e')),
      );
    }
  }

  void _loadProducents() async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      _producents = await _producentService.getProducents();
      setState(() {});
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load producents: $e')),
      );
    }
  }

  void _showAddOrderItemDialog() {
    final TextEditingController nameController = TextEditingController();
    final TextEditingController quantityController = TextEditingController();
    final TextEditingController barCodeController = TextEditingController();
    String? selectedCategoryId;
    String? selectedProducentId;

    List<Category> categories = _categories;
    List<Producent> producents = _producents;

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Add New Order Item'),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                TextFormField(
                  controller: nameController,
                  decoration: const InputDecoration(hintText: 'Item Name'),
                ),
                TextFormField(
                  controller: quantityController,
                  decoration: const InputDecoration(hintText: 'Quantity'),
                  keyboardType: TextInputType.number,
                ),
                TextFormField(
                  controller: barCodeController,
                  decoration: const InputDecoration(hintText: 'Bar Code'),
                ),
                DropdownButtonFormField<String>(
                  value: selectedCategoryId,
                  onChanged: (String? newValue) {
                    selectedCategoryId = newValue;
                  },
                  items: categories
                      .map<DropdownMenuItem<String>>((Category category) {
                    return DropdownMenuItem<String>(
                      value: category.id,
                      child: Text(category.categoryName),
                    );
                  }).toList(),
                  decoration: const InputDecoration(labelText: 'Category'),
                ),
                DropdownButtonFormField<String>(
                  value: selectedProducentId,
                  onChanged: (String? newValue) {
                    selectedProducentId = newValue;
                  },
                  items: producents
                      .map<DropdownMenuItem<String>>((Producent producent) {
                    return DropdownMenuItem<String>(
                      value: producent.id,
                      child: Text(producent.name),
                    );
                  }).toList(),
                  decoration: const InputDecoration(labelText: 'Producent'),
                ),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              child: const Text('Cancel'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: const Text('Add'),
              onPressed: () {
                if (selectedCategoryId != null && selectedProducentId != null) {
                  _addOrderItem(
                      nameController.text,
                      int.tryParse(quantityController.text) ?? 0,
                      selectedCategoryId!,
                      selectedProducentId!,
                      barCodeController.text,
                      widget.orderId);
                  Navigator.of(context).pop();
                } else {}
              },
            ),
          ],
        );
      },
    );
  }

  void _showEditOrderItemDialog(OrderItem orderItem) {
    _itemNameController.text = orderItem.name;
    _quantityController.text = orderItem.quantity.toString();
    _barCodeController.text = orderItem.barCode.toString();
    _selectedCategoryId = orderItem.categoryId;
    _selectedProducentId = orderItem.producentId;

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Edit Order Item'),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                TextFormField(
                  controller: _itemNameController,
                  decoration: const InputDecoration(hintText: 'Item Name'),
                ),
                TextFormField(
                  controller: _quantityController,
                  decoration: const InputDecoration(hintText: 'Quantity'),
                  keyboardType: TextInputType.number,
                ),
                TextFormField(
                  controller: _barCodeController,
                  decoration: const InputDecoration(hintText: 'Bar Code'),
                  keyboardType: TextInputType.number,
                ),
                DropdownButtonFormField<String>(
                  value: _selectedCategoryId,
                  onChanged: (String? newValue) {
                    setState(() {
                      _selectedCategoryId = newValue;
                    });
                  },
                  items: _categories
                      .map<DropdownMenuItem<String>>((Category category) {
                    return DropdownMenuItem<String>(
                      value: category.id,
                      child: Text(category.categoryName),
                    );
                  }).toList(),
                  decoration: const InputDecoration(labelText: 'Category'),
                ),
                DropdownButtonFormField<String>(
                  value: _selectedProducentId,
                  onChanged: (String? newValue) {
                    setState(() {
                      _selectedProducentId = newValue;
                    });
                  },
                  items: _producents
                      .map<DropdownMenuItem<String>>((Producent producent) {
                    return DropdownMenuItem<String>(
                      value: producent.id,
                      child: Text(producent.name),
                    );
                  }).toList(),
                  decoration: const InputDecoration(labelText: 'Producent'),
                ),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              child: const Text('Cancel'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: const Text('Save'),
              onPressed: () {
                _updateOrderItem(orderItem.id);
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  void _updateOrderItem(String orderItemId) async {
    OrderItem updatedItem = OrderItem(
        id: orderItemId,
        name: _itemNameController.text,
        orderId: widget.orderId,
        quantity: int.tryParse(_quantityController.text) ?? 0,
        categoryId: _selectedCategoryId!,
        producentId: _selectedProducentId!,
        barCode: _barCodeController.text);

    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      await _orderItemService.updateOrderItem(updatedItem);
      _loadOrderItems();
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to update order item: $e')),
      );
    }
  }

  Future<void> _deleteOrderItem(String itemId) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      await _orderItemService.deleteOrderItem(itemId);
      _loadOrderItems();
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to delete order item: $e')),
      );
    }
  }

  Future<void> _addOrderItem(
    String name,
    int quantity,
    String categoryId,
    String producentId,
    String barCode,
    String orderId,
  ) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      OrderItem newItem = OrderItem(
        name: name,
        quantity: quantity,
        categoryId: categoryId,
        producentId: producentId,
        barCode: barCode,
        orderId: orderId,
      );

      bool success = await _orderItemService.createOrderItem(newItem);
      if (success) {
        _loadOrderItems();
      } else {}
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to add order item: $e')),
      );
    }
  }

  void _confirmDeleteDialog(BuildContext context, OrderItem item) {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text("Confirm"),
          content: const Text("Are you sure you want to delete the item?"),
          actions: [
            TextButton(
              child: const Text("Cancel"),
              onPressed: () => Navigator.of(context).pop(),
            ),
            TextButton(
              child: const Text("Delete"),
              onPressed: () {
                _deleteOrderItem(item.id);
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Order Details'),
      ),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : Align(
              alignment: Alignment.topCenter,
              child: SingleChildScrollView(
                scrollDirection: Axis.horizontal,
                child: SingleChildScrollView(
                  scrollDirection: Axis.vertical,
                  child: DataTable(
                    columnSpacing: 15,
                    columns: const [
                      DataColumn(label: Text('Name')),
                      DataColumn(label: Text('Quantity')),
                      DataColumn(label: Text('Category')),
                      DataColumn(label: Text('Actions')),
                    ],
                    rows: _orderItems.map((item) {
                      return DataRow(
                        cells: [
                          DataCell(Text(item.name)),
                          DataCell(Text(item.quantity.toString())),
                          DataCell(Text(item.categoryName!)),
                          DataCell(
                            Row(
                              mainAxisSize: MainAxisSize.min,
                              children: !isSupplier(widget.user)
                                  ? [
                                      IconButton(
                                        icon: const Icon(Icons.edit,
                                            color: Color.fromARGB(
                                                255, 21, 121, 12)),
                                        onPressed: () =>
                                            _showEditOrderItemDialog(item),
                                      ),
                                      IconButton(
                                        icon: const Icon(Icons.delete,
                                            color: Color.fromARGB(
                                                255, 146, 17, 8)),
                                        onPressed: () =>
                                            _confirmDeleteDialog(context, item),
                                      ),
                                    ]
                                  : [const Text('No permissions')],
                            ),
                          ),
                        ],
                      );
                    }).toList(),
                  ),
                ),
              ),
            ),
      floatingActionButton: !isSupplier(widget.user)
          ? FloatingActionButton(
              onPressed: _showAddOrderItemDialog,
              child: const Icon(Icons.add),
            )
          : null,
    );
  }
}
