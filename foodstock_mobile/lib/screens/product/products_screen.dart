import 'package:flutter/material.dart';
import 'package:foodstock_mobile/services/product_service.dart';
import 'package:foodstock_mobile/models/product.dart';
import 'package:foodstock_mobile/screens/product/product_view_screen.dart';
import 'package:foodstock_mobile/screens/product/add_product_screen.dart';
import 'package:intl/intl.dart';

class ProductsScreen extends StatefulWidget {
  final String userId;
  const ProductsScreen({super.key, required this.userId});
  @override
  ProductsScreenState createState() => ProductsScreenState();
}

class ProductsScreenState extends State<ProductsScreen> {
  bool _isLoading = true;
  int? _sortColumnIndex;
  bool _sortAscending = true;

  final TextEditingController _searchController = TextEditingController();
  List<Product> _allProducts = [];
  List<Product> _filteredProducts = [];

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _loadProducts();
    });
    _searchController.addListener(_filterProducts);
  }

  _loadProducts() async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      _allProducts = await ProductService().getProducts();
      _filteredProducts = List.from(_allProducts);
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load products: $e')),
      );
    } finally {
      setState(() => _isLoading = false);
    }
  }

  _deleteProduct(String productId) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      bool success = await ProductService().deleteProduct(productId);
      if (success) {
        await _loadProducts();
      } else {
        scaffoldMessenger.showSnackBar(
          const SnackBar(content: Text('Failed to delete the product')),
        );
      }
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('An error occurred: $e')),
      );
    } finally {
      setState(() {});
    }
  }

  void _sort<T>(Comparable<T> Function(Product product) getField,
      int columnIndex, bool ascending) {
    _filteredProducts.sort((Product a, Product b) {
      if (!ascending) {
        final Product c = a;
        a = b;
        b = c;
      }
      final Comparable<T> aValue = getField(a);
      final Comparable<T> bValue = getField(b);
      return Comparable.compare(aValue, bValue);
    });
    setState(() {
      _sortColumnIndex = columnIndex;
      _sortAscending = ascending;
    });
  }

  void _filterProducts() {
    String query = _searchController.text.toLowerCase();
    if (query.isEmpty) {
      setState(() {
        _filteredProducts = List.from(_allProducts);
      });
    } else {
      setState(() {
        _filteredProducts = _allProducts
            .where((product) => product.name.toLowerCase().contains(query))
            .toList();
      });
    }
  }

  @override
  void dispose() {
    _searchController.removeListener(_filterProducts);
    _searchController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    double screenWidth = MediaQuery.of(context).size.width;

    return Scaffold(
      appBar: AppBar(
        title: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: <Widget>[
            const Text('Products'),
            Expanded(
              child: SizedBox(
                height: 35,
                child: Padding(
                  padding: const EdgeInsets.only(left: 35),
                  child: TextField(
                    controller: _searchController,
                    decoration: InputDecoration(
                      hintText: 'Search...',
                      border: InputBorder.none,
                      filled: true,
                      fillColor: Colors.white.withAlpha(235),
                      contentPadding: const EdgeInsets.fromLTRB(10, 2, 0, 0),
                      suffixIcon: const Icon(Icons.search),
                    ),
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
      body: Align(
        alignment: Alignment.topCenter,
        child: Padding(
          padding: const EdgeInsets.all(1.0),
          child: _isLoading
              ? const CircularProgressIndicator()
              : DataTable(
                  columnSpacing: 12.0,
                  sortColumnIndex: _sortColumnIndex,
                  sortAscending: _sortAscending,
                  columns: [
                    DataColumn(
                      label: SizedBox(
                        width: screenWidth * 0.2,
                        child: const Align(
                          alignment: Alignment.centerLeft,
                          child: Text('Product'),
                        ),
                      ),
                      onSort: (columnIndex, ascending) => _sort<String>(
                          (product) => product.name, columnIndex, ascending),
                    ),
                    DataColumn(
                      label: SizedBox(
                        width: screenWidth * 0.05,
                        child: const Text('Qty.'),
                      ),
                      onSort: (columnIndex, ascending) => _sort<num>(
                          (product) => product.quantity,
                          columnIndex,
                          ascending),
                    ),
                    DataColumn(
                      label: SizedBox(
                        width: screenWidth * 0.15,
                        child: const Text('Exp. Date'),
                      ),
                      onSort: (columnIndex, ascending) => _sort<DateTime>(
                          (product) => DateTime.parse(product.expirationDate),
                          columnIndex,
                          ascending),
                    ),
                    DataColumn(
                      label: SizedBox(
                        width: screenWidth * 0.3,
                        child: const Center(
                          child: Text('Actions'),
                        ),
                      ),
                    ),
                  ],
                  rows: _filteredProducts.map((product) {
                    DateTime parsedExpirationDate =
                        DateTime.parse(product.expirationDate);
                    return DataRow(
                      cells: [
                        DataCell(
                          InkWell(
                            onTap: () {
                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                  builder: (context) => ProductViewScreen(
                                      productId: product.id,
                                      userId: widget.userId),
                                ),
                              ).then((result) {
                                if (result == 'refresh') {
                                  _loadProducts();
                                }
                              });
                            },
                            child: Text(product.name),
                          ),
                        ),
                        DataCell(Text(product.quantity.toString())),
                        DataCell(Text(DateFormat('yyyy-MM-dd')
                            .format(parsedExpirationDate))),
                        DataCell(
                          Row(
                            mainAxisAlignment: MainAxisAlignment.end,
                            children: [
                              TextButton(
                                onPressed: () {
                                  Navigator.push(
                                    context,
                                    MaterialPageRoute(
                                      builder: (context) => ProductViewScreen(
                                          productId: product.id,
                                          userId: widget.userId),
                                    ),
                                  ).then((result) {
                                    if (result == 'refresh') {
                                      _loadProducts();
                                    }
                                  });
                                },
                                child: const Text('Details'),
                              ),
                              IconButton(
                                icon: const Icon(Icons.delete),
                                color: const Color.fromARGB(255, 187, 52, 43),
                                onPressed: () {
                                  showDialog(
                                    context: context,
                                    builder: (BuildContext context) {
                                      return AlertDialog(
                                        title: const Text("Confirm"),
                                        content: const Text(
                                            "Are you sure you want to delete the product?"),
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
                                              await _deleteProduct(product.id);
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
                          ),
                        ),
                      ],
                    );
                  }).toList(),
                ),
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          Navigator.push(
            context,
            MaterialPageRoute(
              builder: (context) => AddProductScreen(
                userId: widget.userId,
              ),
            ),
          ).then((value) {
            _loadProducts();
          });
        },
        child: const Icon(Icons.add),
      ),
    );
  }
}
