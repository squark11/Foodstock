import 'package:flutter/material.dart';
import 'package:foodstock_mobile/models/user.dart';
import 'package:foodstock_mobile/screens/category/category_screen.dart';
import 'package:foodstock_mobile/screens/order/order_screen.dart';
import 'package:foodstock_mobile/screens/profile_screen.dart';
import 'package:foodstock_mobile/screens/product/products_screen.dart';
import 'package:foodstock_mobile/screens/login_screen.dart';
import 'package:foodstock_mobile/screens/admin_console_screen.dart';
import 'package:foodstock_mobile/widgets/menu.dart';

class HomeScreen extends StatefulWidget {
  final User user;
  final String roleName;

  const HomeScreen({super.key, required this.user, required this.roleName});

  @override
  HomeScreenState createState() => HomeScreenState();
}

class HomeScreenState extends State<HomeScreen> {
  int _selectedScreenIndex = 0;
  late List<Widget> _screens;

  @override
  void initState() {
    super.initState();
    _screens = [
      ProfileScreen(user: widget.user),
      if (widget.roleName == 'Admin' || widget.roleName == 'Employee')
        ProductsScreen(userId: widget.user.id),
      if (widget.roleName == 'Admin' || widget.roleName == 'Employee')
        CategoryScreen(user: widget.user),
      if (widget.roleName == 'Admin' ||
          widget.roleName == 'Employee' ||
          widget.roleName == 'Supplier')
        OrderScreen(user: widget.user),
      if (widget.roleName == 'Admin') const AdminConsoleScreen(),
      const LoginScreen(),
    ];
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Food Stock Mobile App'),
      ),
      drawer: Menu(
          onItemSelected: _onMenuItemSelected,
          currentUser: widget.user,
          currentRoleName: widget.roleName),
      body: _screens[_selectedScreenIndex],
    );
  }

  void _onMenuItemSelected(int index) {
    setState(() {
      _selectedScreenIndex = index;
    });
  }
}
