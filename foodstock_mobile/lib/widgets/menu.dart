import 'package:flutter/material.dart';
import 'package:foodstock_mobile/config/constanst.dart';
import 'package:foodstock_mobile/screens/login_screen.dart';
import 'package:foodstock_mobile/models/user.dart';

class Menu extends StatelessWidget {
  final Function(int) onItemSelected;
  final User currentUser;
  final String currentRoleName;

  const Menu(
      {required this.onItemSelected,
      required this.currentUser,
      required this.currentRoleName,
      super.key});

  @override
  Widget build(BuildContext context) {
    List<Widget> menuItems = [
      const DrawerHeader(
        decoration: BoxDecoration(
          color: secondaryColor,
        ),
        child: Center(
          child: Text(
            'Food Stack\n Menu',
            textAlign: TextAlign.center,
            style: TextStyle(
              color: Colors.white,
              fontSize: 25,
            ),
          ),
        ),
      ),
    ];

    menuItems.add(menuItem(0, Icons.person, 'Profile', context: context));

    if (currentRoleName == 'Employee' || currentRoleName == 'Admin') {
      menuItems.add(menuItem(menuItems.length - 1, Icons.food_bank, 'Products',
          context: context));
      menuItems.add(menuItem(menuItems.length - 1, Icons.category, 'Category',
          context: context));
    }

    if (currentRoleName == 'Supplier' ||
        currentRoleName == 'Employee' ||
        currentRoleName == 'Admin') {
      menuItems.add(menuItem(menuItems.length - 1, Icons.list_alt, 'Order',
          context: context));
    }

    if (currentRoleName == 'Admin') {
      menuItems.add(menuItem(
          menuItems.length - 1, Icons.admin_panel_settings, 'Admin Console',
          context: context));
    }

    menuItems.add(menuItem(menuItems.length - 1, Icons.logout, 'Logout',
        context: context));

    return Drawer(
      child: Container(
        color: primaryColor,
        child: ListView(
          padding: EdgeInsets.zero,
          children: menuItems,
        ),
      ),
    );
  }

  Widget menuItem(int index, IconData icon, String title,
      {required BuildContext context}) {
    return ListTile(
      leading: Icon(
        icon,
        color: Colors.white,
      ),
      title: Text(
        title,
        style: const TextStyle(color: Colors.white),
      ),
      onTap: () {
        if (title == 'Logout') {
          Navigator.pushAndRemoveUntil(
            context,
            MaterialPageRoute(builder: (context) => const LoginScreen()),
            (route) => false,
          );
        } else {
          Navigator.pop(context);
          onItemSelected(index);
        }
      },
    );
  }
}
