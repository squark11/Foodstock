import 'package:flutter/material.dart';
import 'package:foodstock_mobile/models/role.dart';
import 'package:foodstock_mobile/services/role_service.dart';

class RoleView extends StatefulWidget {
  const RoleView({super.key});

  @override
  RoleViewState createState() => RoleViewState();
}

class RoleViewState extends State<RoleView> {
  List<Role> _roles = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _loadRoles();
    });
  }

  void _showAddRoleDialog() {
    TextEditingController nameController = TextEditingController();

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Add New Role'),
          content: TextField(
            controller: nameController,
            decoration: const InputDecoration(hintText: 'Role Name'),
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
                _addRole(nameController.text);
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  Future<void> _addRole(String name) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    Role newRole = Role(name: name);
    bool success = await RoleService().addRole(newRole);
    if (success) {
      scaffoldMessenger.showSnackBar(
          const SnackBar(content: Text("Role added successfully")));
      _loadRoles();
    } else {
      scaffoldMessenger
          .showSnackBar(const SnackBar(content: Text("Failed to add role")));
    }
  }

  void _showEditRoleDialog(Role role) {
    TextEditingController nameController =
        TextEditingController(text: role.name);

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Edit Role'),
          content: TextField(
            controller: nameController,
            decoration: const InputDecoration(hintText: 'Role Name'),
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
                _editRole(role.id, nameController.text);
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  Future<void> _editRole(String id, String name) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    Role updatedRole = Role(id: id, name: name);
    bool success = await RoleService().updateRole(updatedRole);
    if (success) {
      scaffoldMessenger.showSnackBar(
          const SnackBar(content: Text("Role updated successfully")));
      _loadRoles();
    } else {
      scaffoldMessenger
          .showSnackBar(const SnackBar(content: Text("Failed to update role")));
    }
  }

  Future<void> _deleteRole(String id) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    bool success = await RoleService().deleteRole(id);
    if (success) {
      scaffoldMessenger.showSnackBar(
          const SnackBar(content: Text("Role deleted successfully")));
      _loadRoles();
    } else {
      scaffoldMessenger
          .showSnackBar(const SnackBar(content: Text("Failed to delete role")));
    }
  }

  Future<void> _loadRoles() async {
    setState(() {
      _isLoading = true;
    });
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    try {
      List<Role> roles = await RoleService().getRoles();
      setState(() {
        _roles = roles;
      });
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load roles: $e')),
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
                  rows: _roles
                      .map((role) => DataRow(
                            cells: [
                              DataCell(Text(role.name)),
                              DataCell(Row(
                                children: <Widget>[
                                  IconButton(
                                    icon: const Icon(Icons.edit,
                                        color:
                                            Color.fromARGB(255, 21, 121, 12)),
                                    onPressed: () {
                                      _showEditRoleDialog(role);
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
                                                "Are you sure you want to delete the role?"),
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
                                                  _deleteRole(role.id);
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
              )),
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: ElevatedButton(
              onPressed: _showAddRoleDialog,
              child: const Text('Add New Role'),
            ),
          ),
        ],
      ),
    );
  }
}
