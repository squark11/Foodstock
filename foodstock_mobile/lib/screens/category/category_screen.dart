import 'package:flutter/material.dart';
import 'package:foodstock_mobile/models/user.dart';
import 'package:material_design_icons_flutter/material_design_icons_flutter.dart';
import 'package:foodstock_mobile/services/category_service.dart';
import 'package:foodstock_mobile/config/constanst.dart';
import 'package:foodstock_mobile/models/category.dart';
import 'package:foodstock_mobile/screens/category/category_products_screen.dart';

class CategoryScreen extends StatefulWidget {
  final User user;
  const CategoryScreen({super.key, required this.user});
  @override
  CategoryScreenState createState() => CategoryScreenState();
}

class CategoryScreenState extends State<CategoryScreen> {
  late Future<List<Category>> _categoriesFuture;

  @override
  void initState() {
    super.initState();
    _reloadCategories();
  }

  _reloadCategories() async {
    try {
      _categoriesFuture = CategoryService().getCategories();
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Failed to load categories: $e')),
      );
    }
  }

  _addCategory(String categoryName) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      Category newCategory = Category(id: '', categoryName: categoryName);
      await CategoryService().addCategory(newCategory);
      _reloadCategories();
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to add category: $e')),
      );
    } finally {
      setState(() {});
    }
  }

  _editCategory(String id, String newCategoryName) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      Category category = Category(id: id, categoryName: newCategoryName);
      await CategoryService().editCategory(id, category);
      _reloadCategories();
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to edit category: $e')),
      );
    } finally {
      setState(() {});
    }
  }

  _deleteCategory(String id) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      await CategoryService().deleteCategory(id);
      _reloadCategories();
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to delete category: $e')),
      );
    } finally {
      setState(() {});
    }
  }

  _showAddDialog() {
    TextEditingController categoryNameController = TextEditingController();
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: const Text("Add Category"),
          content: TextField(
            controller: categoryNameController,
            decoration: const InputDecoration(labelText: "Category Name"),
          ),
          actions: [
            TextButton(
              child: const Text("Cancel"),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: const Text("Add"),
              onPressed: () {
                String categoryNameAdd = categoryNameController.text;
                _addCategory(categoryNameAdd);
                Navigator.of(context).pop();
              },
            )
          ],
        );
      },
    );
  }

  _showEditDialog(Category category) {
    TextEditingController categoryNameController =
        TextEditingController(text: category.categoryName);
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: const Text("Edit Category"),
          content: TextField(
            controller: categoryNameController,
            decoration: const InputDecoration(labelText: "Category Name"),
          ),
          actions: [
            TextButton(
              child: const Text("Cancel"),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: const Text("Save"),
              onPressed: () {
                _editCategory(category.id, categoryNameController.text);
                Navigator.of(context).pop();
              },
            )
          ],
        );
      },
    );
  }

  void _showDeleteConfirmationDialog(String categoryId, String categoryName) {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Confirm'),
          content: Text(
              'Are you sure you want to delete the category: $categoryName ?'),
          actions: <Widget>[
            TextButton(
              child: const Text('Cancel'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: const Text('Delete'),
              onPressed: () async {
                var navigator = Navigator.of(context);
                await _deleteCategory(categoryId);
                navigator.pop();
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
        title: const Text('Categories'),
        backgroundColor: primaryColor,
      ),
      body: FutureBuilder<List<Category>>(
        future: _categoriesFuture,
        builder: (context, snapshot) {
          if (snapshot.hasData) {
            return ListView.builder(
              itemCount: snapshot.data!.length,
              itemBuilder: (context, index) {
                var category = snapshot.data![index];
                return Card(
                  color: const Color.fromARGB(255, 230, 230, 230),
                  child: ListTile(
                    leading: getIconForCategory(category.categoryName),
                    title: Text(category.categoryName),
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => CategoryProductsScreen(
                              userId: widget.user.id, categoryId: category.id),
                        ),
                      );
                    },
                    trailing: Row(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        IconButton(
                          icon: const Icon(Icons.edit,
                              color: Color.fromARGB(255, 21, 121, 12)),
                          onPressed: () {
                            _showEditDialog(category);
                          },
                        ),
                        IconButton(
                          icon: const Icon(Icons.delete,
                              color: Color.fromARGB(255, 146, 17, 8)),
                          onPressed: () {
                            _showDeleteConfirmationDialog(
                                category.id, category.categoryName);
                          },
                        ),
                      ],
                    ),
                  ),
                );
              },
            );
          } else if (snapshot.hasError) {
            return const Center(child: Text('An error occurred'));
          }
          return const Center(child: CircularProgressIndicator());
        },
      ),
      floatingActionButton: FloatingActionButton(
        backgroundColor: primaryColor,
        onPressed: _showAddDialog,
        child: const Icon(Icons.add),
      ),
    );
  }

  Icon getIconForCategory(String categoryName) {
    switch (categoryName.toLowerCase()) {
      case 'fruit' || 'owoce':
        return Icon(MdiIcons.fruitWatermelon, color: Colors.green);
      case 'vegetables' || 'warzywa':
        return Icon(MdiIcons.carrot, color: Colors.orange);
      case 'meat' || 'mieso':
        return Icon(MdiIcons.pigVariantOutline, color: Colors.red);
      case 'dairy' || 'nabial':
        return Icon(MdiIcons.cheese,
            color: const Color.fromARGB(255, 216, 195, 10));
      case 'bread' || 'pieczywo':
        return Icon(MdiIcons.breadSlice,
            color: const Color.fromARGB(255, 141, 93, 76));
      case 'confectionery' || 'przetwory':
        return const Icon(Icons.compost_rounded, color: Colors.deepOrange);
      case 'grains and cereals' || 'zboza i produkty zbozowe':
        return Icon(MdiIcons.grain,
            color: const Color.fromARGB(255, 161, 146, 12));
      case 'beverages' || 'napoje':
        return Icon(MdiIcons.bottleSoda, color: Colors.blue);
      case 'candy' || 'przekaski i slodycze':
        return Icon(MdiIcons.candy, color: Colors.pink);
      case 'frozen foods' || 'mrożonki':
        return Icon(MdiIcons.iceCream, color: Colors.cyan);
      case 'gluten-free products' || 'produkty bezglutenowe':
        return Icon(MdiIcons.breadSliceOutline,
            color: const Color.fromARGB(255, 131, 78, 0));
      case 'organic products' || 'produkty ekologiczne':
        return Icon(MdiIcons.rice, color: Colors.lightGreen);
      case 'spices and herbs' || 'przyprawy i ziola':
        return Icon(MdiIcons.grass,
            color: const Color.fromARGB(255, 35, 88, 37));
      case 'oils and fats' || 'oleje i tluszcze':
        return Icon(MdiIcons.oil, color: Colors.amber);
      case 'seafood' || 'produkty morskie':
        return Icon(MdiIcons.fish, color: Colors.blueAccent);
      case 'drink' || 'alkohole':
        return Icon(MdiIcons.bottleWine,
            color: const Color.fromARGB(255, 92, 36, 3));
      default:
        return const Icon(Icons.help_outline, color: Colors.grey);
    }
  }
}
