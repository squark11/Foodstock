import 'package:flutter/material.dart';
import 'package:foodstock_mobile/models/supplier.dart';
import 'package:foodstock_mobile/services/supplier_service.dart';

class SupplierView extends StatefulWidget {
  const SupplierView({super.key});

  @override
  SupplierViewState createState() => SupplierViewState();
}

class SupplierViewState extends State<SupplierView> {
  List<Supplier> _suppliers = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _loadSuppliers();
    });
  }

  void _showAddSupplierDialog() {
    TextEditingController nameController = TextEditingController();

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Add New Supplier'),
          content: TextField(
            controller: nameController,
            decoration: const InputDecoration(hintText: 'Supplier Name'),
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
                Navigator.of(context).pop();
                _addSupplier(nameController.text);
              },
            ),
          ],
        );
      },
    );
  }

  void _addSupplier(String name) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    try {
      Supplier newSupplier = Supplier(name: name);
      bool success = await SupplierService().addSupplier(newSupplier);
      if (success) {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Supplier added successfully")));
        _loadSuppliers();
      } else {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Failed to add supplier")));
      }
    } catch (e) {
      scaffoldMessenger
          .showSnackBar(SnackBar(content: Text("Error: ${e.toString()}")));
    }
  }

  void _showEditSupplierDialog(Supplier supplier) {
    TextEditingController nameController =
        TextEditingController(text: supplier.name);

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Edit Supplier'),
          content: TextField(
            controller: nameController,
            decoration: const InputDecoration(hintText: 'Name'),
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
                Navigator.of(context).pop();
                _updateSupplier(supplier.id, nameController.text);
              },
            ),
          ],
        );
      },
    );
  }

  void _updateSupplier(String supplierId, String newName) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    try {
      Supplier updatedSupplier = Supplier(id: supplierId, name: newName);
      bool success = await SupplierService().updateSupplier(updatedSupplier);
      if (success) {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Supplier updated successfully")));
        _loadSuppliers();
      } else {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Failed to update supplier")));
      }
    } catch (e) {
      scaffoldMessenger
          .showSnackBar(SnackBar(content: Text("Error: ${e.toString()}")));
    }
  }

  void _deleteSupplier(String supplierId) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    try {
      bool success = await SupplierService().deleteSupplier(supplierId);
      if (success) {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Supplier deleted successfully")));
        _loadSuppliers();
      } else {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Failed to delete supplier")));
      }
    } catch (e) {
      scaffoldMessenger
          .showSnackBar(SnackBar(content: Text("Error: ${e.toString()}")));
    }
  }

  Future<void> _loadSuppliers() async {
    setState(() {
      _isLoading = true;
    });
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      List<Supplier> suppliers = await SupplierService().getSuppliers();
      setState(() {
        _suppliers = suppliers;
      });
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load suppliers: $e')),
      );
    } finally {
      setState(() {
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    if (_isLoading) {
      return const CircularProgressIndicator();
    }

    return Center(
      child: Column(
        children: [
          SingleChildScrollView(
            scrollDirection: Axis.horizontal,
            child: SingleChildScrollView(
              scrollDirection: Axis.vertical,
              child: DataTable(
                columnSpacing: 10.0,
                columns: [
                  DataColumn(
                    label: SizedBox(
                      width: MediaQuery.of(context).size.width * 0.55,
                      child: const Text('Name'),
                    ),
                  ),
                  DataColumn(
                    label: SizedBox(
                      width: MediaQuery.of(context).size.width * 0.25,
                      child: const Text('Actions'),
                    ),
                  ),
                ],
                rows: _suppliers
                    .map((supplier) => DataRow(
                          cells: [
                            DataCell(Text(supplier.name)),
                            DataCell(Row(
                              children: <Widget>[
                                IconButton(
                                  icon: const Icon(Icons.edit,
                                      color: Color.fromARGB(255, 21, 121, 12)),
                                  onPressed: () {
                                    _showEditSupplierDialog(supplier);
                                  },
                                ),
                                IconButton(
                                  icon: const Icon(Icons.delete,
                                      color: Color.fromARGB(255, 146, 17, 8)),
                                  onPressed: () {
                                    showDialog(
                                      context: context,
                                      builder: (BuildContext context) {
                                        return AlertDialog(
                                          title: const Text("Confirm"),
                                          content: const Text(
                                              "Are you sure you want to delete the supplier?"),
                                          actions: [
                                            TextButton(
                                              child: const Text("Cancel"),
                                              onPressed: () {
                                                Navigator.of(context).pop();
                                              },
                                            ),
                                            TextButton(
                                              child: const Text("Delete"),
                                              onPressed: () async {
                                                var navigator =
                                                    Navigator.of(context);
                                                _deleteSupplier(supplier.id);
                                                navigator.pop();
                                              },
                                            ),
                                          ],
                                        );
                                      },
                                    );
                                  },
                                ),
                              ],
                            )),
                          ],
                        ))
                    .toList(),
              ),
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: ElevatedButton(
              onPressed: _showAddSupplierDialog,
              child: const Text('Add New Supplier'),
            ),
          ),
        ],
      ),
    );
  }
}
