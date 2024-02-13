import 'package:flutter/material.dart';
import 'package:foodstock_mobile/models/product.dart';
import 'package:foodstock_mobile/models/order.dart';
import 'package:foodstock_mobile/models/order_item.dart';
import 'package:foodstock_mobile/models/category.dart';
import 'package:foodstock_mobile/models/producent.dart';
import 'package:foodstock_mobile/models/supplier.dart';
import 'package:foodstock_mobile/models/user.dart';
import 'package:foodstock_mobile/services/order_service.dart';
import 'package:foodstock_mobile/services/order_item_service.dart';
import 'package:foodstock_mobile/services/product_service.dart';
import 'package:foodstock_mobile/services/category_service.dart';
import 'package:foodstock_mobile/services/user_service.dart';
import 'package:foodstock_mobile/services/supplier_service.dart';
import 'package:foodstock_mobile/services/producent_service.dart';

class ProductViewScreen extends StatefulWidget {
  final String productId;
  final String userId;

  const ProductViewScreen({
    super.key,
    required this.productId,
    required this.userId,
  });

  @override
  ProductViewScreenState createState() => ProductViewScreenState();
}

class ProductViewScreenState extends State<ProductViewScreen> {
  final OrderItemService _orderItemService = OrderItemService();
  final OrderService _orderService = OrderService();
  late Product product;
  late List<Category> categories;
  late List<Producent> producents;
  late List<Supplier> suppliers;
  late User currentUser;
  List<Order> _orders = [];

  bool isEditing = false;
  bool isLoading = true;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      loadData();
      _loadOrders();
    });
  }

  Future<void> loadData() async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      product = await ProductService().getProductById(widget.productId);
      categories = await CategoryService().getCategories();
      producents = await ProducentService().getProducents();
      suppliers = await SupplierService().getSuppliers();
      currentUser = await UserService().getUser(widget.userId);
      setState(() {
        isLoading = false;
      });
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load data: $e')),
      );
    }
  }

  void _loadOrders() async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      _orders = await _orderService.getOrders();
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load orders: $e')),
      );
    }
  }

  Future<void> saveChanges() async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      await ProductService().updateProduct(product);
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to save changes: $e')),
      );
    }
  }

  void _showOrderProductDialog() {
    final TextEditingController quantityController = TextEditingController();
    String? selectedOrderId;

    showDialog(
      context: context,
      builder: (BuildContext dialogContext) {
        return AlertDialog(
          title: Text('Order ${product.name}'),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                TextFormField(
                  controller: quantityController,
                  decoration: const InputDecoration(hintText: 'Quantity'),
                  keyboardType: TextInputType.number,
                ),
                DropdownButtonFormField<String>(
                  value: selectedOrderId,
                  onChanged: (String? newValue) {
                    selectedOrderId = newValue;
                  },
                  items: _orders.map<DropdownMenuItem<String>>((Order order) {
                    return DropdownMenuItem<String>(
                      value: order.id,
                      child: Text(order.supplier.name),
                    );
                  }).toList(),
                  decoration: const InputDecoration(labelText: 'Select Order'),
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
              child: const Text('Add to Order'),
              onPressed: () async {
                if (selectedOrderId != null &&
                    quantityController.text.isNotEmpty) {
                  await _addOrderItem(
                    product.name,
                    int.parse(quantityController.text),
                    product.categoryId!,
                    product.producentId!,
                    product.barCode,
                    selectedOrderId!,
                  );
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
      if (success && mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text('Successfully added'),
            duration: Duration(seconds: 2),
          ),
        );
      } else {}
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to add product: $e')),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
          title: const Text('Product Details'),
          leading: IconButton(
              icon: const Icon(Icons.arrow_back),
              onPressed: () {
                Navigator.pop(context, 'refresh');
              })),
      body: isLoading
          ? const Center(child: CircularProgressIndicator())
          : Padding(
              padding: const EdgeInsets.all(8.0),
              child: Card(
                child: Padding(
                  padding: const EdgeInsets.all(16.0),
                  child: ListView(
                    children: [
                      ListTile(
                        leading: const Icon(Icons.shopping_bag_outlined),
                        title: isEditing
                            ? TextField(
                                controller:
                                    TextEditingController(text: product.name),
                                onChanged: (value) {
                                  product.name = value;
                                },
                                decoration: const InputDecoration(
                                    labelText: 'Product Name'),
                              )
                            : Text(product.name),
                      ),
                      ListTile(
                        leading:
                            const Icon(Icons.format_list_numbered_outlined),
                        title: isEditing
                            ? TextField(
                                keyboardType: TextInputType.number,
                                controller: TextEditingController(
                                    text: product.quantity.toString()),
                                onChanged: (value) {
                                  product.quantity = int.parse(value);
                                },
                                decoration: const InputDecoration(
                                    labelText: 'Quantity'),
                              )
                            : Text("${product.quantity}"),
                      ),
                      ListTile(
                        leading: const Icon(Icons.calendar_today),
                        title:
                            Text("Expiration Date: ${product.expirationDate}"),
                        trailing: isEditing
                            ? IconButton(
                                icon: const Icon(Icons.edit_calendar),
                                onPressed: () async {
                                  DateTime initialDate = DateTime.tryParse(
                                          product.expirationDate) ??
                                      DateTime.now();
                                  final DateTime? pickedDate =
                                      await showDatePicker(
                                    context: context,
                                    initialDate: initialDate,
                                    firstDate: DateTime(2000),
                                    lastDate: DateTime(2101),
                                  );

                                  setState(
                                    () {
                                      product.expirationDate =
                                          "$pickedDate".split(' ')[0];
                                    },
                                  );
                                },
                              )
                            : null,
                      ),
                      ListTile(
                        leading: const Icon(Icons.category_outlined),
                        title: isEditing
                            ? DropdownButton<String>(
                                value: product.categoryId,
                                items: categories.map((Category category) {
                                  return DropdownMenuItem<String>(
                                    value: category.id,
                                    child: Text(category.categoryName),
                                  );
                                }).toList(),
                                onChanged: (String? newValue) {
                                  setState(() {
                                    product.categoryId = newValue;
                                  });
                                },
                              )
                            : Text(categories
                                .firstWhere((category) =>
                                    category.id == product.categoryId)
                                .categoryName),
                      ),
                      ListTile(
                        leading: const Icon(Icons.business),
                        title: isEditing
                            ? DropdownButton<String>(
                                value: product.producentId.toString(),
                                items: producents.map((Producent producent) {
                                  return DropdownMenuItem<String>(
                                    value: producent.id.toString(),
                                    child: Text(producent.name),
                                  );
                                }).toList(),
                                onChanged: (String? newValue) {
                                  setState(() {
                                    product.producentId = newValue;
                                  });
                                },
                              )
                            : Text(producents
                                .firstWhere((producent) =>
                                    producent.id == product.producentId)
                                .name),
                      ),
                      ListTile(
                        leading: const Icon(Icons.qr_code),
                        title: isEditing
                            ? TextField(
                                keyboardType: TextInputType.number,
                                controller: TextEditingController(
                                    text: product.barCode),
                                onChanged: (value) {
                                  product.barCode = value;
                                },
                                decoration: const InputDecoration(
                                    labelText: 'Bar Code'),
                              )
                            : Text(product.barCode),
                      ),
                      ListTile(
                        leading: const Icon(Icons.local_shipping_outlined),
                        title: isEditing
                            ? DropdownButton<String>(
                                value: product.supplierId.toString(),
                                items: suppliers.map((Supplier supplier) {
                                  return DropdownMenuItem<String>(
                                    value: supplier.id.toString(),
                                    child: Text(supplier.name),
                                  );
                                }).toList(),
                                onChanged: (String? newValue) {
                                  setState(() {
                                    product.supplierId = newValue;
                                  });
                                },
                              )
                            : Text(suppliers
                                .firstWhere((supplier) =>
                                    supplier.id == product.supplierId)
                                .name),
                      ),
                      ListTile(
                        leading: const Icon(Icons.calendar_today),
                        title: Text("Delivery Date: ${product.deliveryDate}"),
                        trailing: isEditing
                            ? IconButton(
                                icon: const Icon(Icons.edit_calendar),
                                onPressed: () async {
                                  DateTime initialDate =
                                      DateTime.tryParse(product.deliveryDate) ??
                                          DateTime.now();
                                  final DateTime? pickedDate =
                                      await showDatePicker(
                                    context: context,
                                    initialDate: initialDate,
                                    firstDate: DateTime(2000),
                                    lastDate: DateTime(2101),
                                  );

                                  setState(() {
                                    product.deliveryDate =
                                        "$pickedDate".split(' ')[0];
                                  });
                                },
                              )
                            : null,
                      ),
                      ListTile(
                        leading: const Icon(Icons.person),
                        title: Text(
                            "${product.userFirstName} ${product.userSurname}"),
                      ),
                      Padding(
                        padding: const EdgeInsets.symmetric(vertical: 8.0),
                        child: ElevatedButton.icon(
                          style: ElevatedButton.styleFrom(
                              backgroundColor:
                                  const Color.fromARGB(255, 40, 121, 42)),
                          onPressed: () async {
                            if (isEditing) {
                              bool confirm = await showDialog(
                                    context: context,
                                    builder: (context) => AlertDialog(
                                      title: const Text('Confirm'),
                                      content: const Text(
                                          'Do you want to save all changes?'),
                                      actions: <Widget>[
                                        TextButton(
                                          child: const Text('Cancel'),
                                          onPressed: () =>
                                              Navigator.of(context).pop(false),
                                        ),
                                        TextButton(
                                            child: const Text('Yes'),
                                            onPressed: () {
                                              product.userId = widget.userId;
                                              Navigator.of(context).pop(true);
                                            }),
                                      ],
                                    ),
                                  ) ??
                                  false;

                              if (confirm) {
                                await saveChanges();
                              }
                            }

                            setState(() {
                              isEditing = !isEditing;
                            });
                          },
                          icon: Icon(isEditing ? Icons.save : Icons.edit),
                          label: Text(isEditing ? "Save" : "Edit"),
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.symmetric(vertical: 6.0),
                        child: ElevatedButton(
                          onPressed: _showOrderProductDialog,
                          child: Text("Order ${product.name}"),
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.symmetric(vertical: 8.0),
                        child: ElevatedButton(
                          style: ElevatedButton.styleFrom(
                              backgroundColor: Colors.black54),
                          onPressed: () {
                            Navigator.pop(context, 'refresh');
                          },
                          child: const Text("Back to Products"),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            ),
    );
  }
}
