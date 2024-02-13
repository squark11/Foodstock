import 'package:flutter/material.dart';
import 'package:foodstock_mobile/widgets/supplier_view.dart';
import 'package:foodstock_mobile/widgets/producent_view.dart';
import 'package:foodstock_mobile/widgets/organization_view.dart';
import 'package:foodstock_mobile/widgets/role_view.dart';
import 'package:foodstock_mobile/widgets/user_view.dart';

class AdminConsoleScreen extends StatefulWidget {
  const AdminConsoleScreen({super.key});

  @override
  AdminConsoleScreenState createState() => AdminConsoleScreenState();
}

class AdminConsoleScreenState extends State<AdminConsoleScreen> {
  String _selectedOption = 'Manage Suppliers';

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Admin Console'),
      ),
      body: Column(
        children: [
          DropdownButton<String>(
            value: _selectedOption,
            onChanged: (String? newValue) {
              setState(() {
                _selectedOption = newValue!;
              });
            },
            items: <String>[
              'Manage Suppliers',
              'Manage Producents',
              'Manage Users',
              'Manage Roles',
              'Manage Organizations'
            ].map<DropdownMenuItem<String>>((String value) {
              return DropdownMenuItem<String>(
                value: value,
                child: Text(value),
              );
            }).toList(),
          ),
          Expanded(
            child: _buildSelectedOptionView(_selectedOption),
          ),
        ],
      ),
    );
  }

  Widget _buildSelectedOptionView(String option) {
    switch (option) {
      case 'Manage Suppliers':
        return const SupplierView();
      case 'Manage Producents':
        return const ProducentView();
      case 'Manage Users':
        return const UserView();
      case 'Manage Roles':
        return const RoleView();
      case 'Manage Organizations':
        return const OrganizationView();

      default:
        return Container();
    }
  }
}
