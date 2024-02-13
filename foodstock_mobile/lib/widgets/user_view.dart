import 'package:flutter/material.dart';
import 'package:foodstock_mobile/models/user.dart';
import 'package:foodstock_mobile/models/role.dart';
import 'package:foodstock_mobile/services/role_service.dart';
import 'package:foodstock_mobile/services/user_service.dart';

class UserView extends StatefulWidget {
  const UserView({super.key});

  @override
  UserViewState createState() => UserViewState();
}

class UserViewState extends State<UserView> {
  List<User> _users = [];
  List<Role> _roles = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _loadData();
    });
  }

  Future<void> _loadData() async {
    setState(() {
      _isLoading = true;
    });

    final scaffoldMessenger = ScaffoldMessenger.of(context);

    try {
      _users = await UserService().getUsers();
      _roles = await RoleService().getRoles();
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load data: $e')),
      );
    } finally {
      setState(() {
        _isLoading = false;
      });
    }
  }

  String _getRoleName(String roleId) {
    return _roles
        .firstWhere((role) => role.id == roleId,
            orElse: () => Role(id: '', name: 'Unknown'))
        .name;
  }

  void _showAddUserDialog() {
    TextEditingController emailController = TextEditingController();
    TextEditingController passwordController = TextEditingController();
    TextEditingController confirmPasswordController = TextEditingController();
    TextEditingController firstNameController = TextEditingController();
    TextEditingController surnameController = TextEditingController();
    String selectedRoleId = _roles.isNotEmpty ? _roles.first.id : '';

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Add New User'),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                TextField(
                  controller: emailController,
                  decoration: const InputDecoration(hintText: 'Email'),
                ),
                TextField(
                  controller: passwordController,
                  decoration: const InputDecoration(hintText: 'Password'),
                  obscureText: true,
                ),
                TextField(
                  controller: confirmPasswordController,
                  decoration:
                      const InputDecoration(hintText: 'Confirm Password'),
                  obscureText: true,
                ),
                TextField(
                  controller: firstNameController,
                  decoration: const InputDecoration(hintText: 'First Name'),
                ),
                TextField(
                  controller: surnameController,
                  decoration: const InputDecoration(hintText: 'Surname'),
                ),
                DropdownButtonFormField<String>(
                  value: selectedRoleId,
                  items: _roles.map((Role role) {
                    return DropdownMenuItem(
                      value: role.id,
                      child: Text(role.name),
                    );
                  }).toList(),
                  onChanged: (String? newValue) {
                    selectedRoleId = newValue ?? '';
                  },
                  decoration: const InputDecoration(labelText: 'Role'),
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
                if (passwordController.text != confirmPasswordController.text) {
                  ScaffoldMessenger.of(context).showSnackBar(
                      const SnackBar(content: Text("Passwords do not match")));
                  return;
                }

                _addUser(
                  emailController.text,
                  passwordController.text,
                  firstNameController.text,
                  surnameController.text,
                  selectedRoleId,
                );

                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  void _showEditUserDialog(User user) {
    TextEditingController emailController =
        TextEditingController(text: user.email);
    TextEditingController firstNameController =
        TextEditingController(text: user.firstName);
    TextEditingController surnameController =
        TextEditingController(text: user.surname);
    String selectedRoleId = user.roleId;

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Edit User'),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                TextField(
                  controller: emailController,
                  decoration: const InputDecoration(hintText: 'Email'),
                ),
                TextField(
                  controller: firstNameController,
                  decoration: const InputDecoration(hintText: 'First Name'),
                ),
                TextField(
                  controller: surnameController,
                  decoration: const InputDecoration(hintText: 'Surname'),
                ),
                DropdownButtonFormField<String>(
                  value: selectedRoleId,
                  items: _roles.map((Role role) {
                    return DropdownMenuItem(
                      value: role.id,
                      child: Text(role.name),
                    );
                  }).toList(),
                  onChanged: (String? newValue) {
                    selectedRoleId = newValue ?? '';
                  },
                  decoration: const InputDecoration(labelText: 'Role'),
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
                _editUser(
                    user.id,
                    emailController.text,
                    firstNameController.text,
                    surnameController.text,
                    selectedRoleId);
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  Future<void> _addUser(String email, String password, String firstName,
      String surname, String roleId) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    User newUser = User(
      email: email,
      password: password,
      firstName: firstName,
      surname: surname,
      roleId: roleId,
    );

    bool success = await UserService().addUser(newUser);
    if (success) {
      scaffoldMessenger.showSnackBar(
          const SnackBar(content: Text("User added successfully")));
      _loadData();
    } else {
      scaffoldMessenger
          .showSnackBar(const SnackBar(content: Text("Failed to add user")));
    }
  }

  Future<void> _editUser(String id, String email, String firstName,
      String surname, String roleId) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    User updatedUser = User(
      id: id,
      email: email,
      firstName: firstName,
      surname: surname,
      roleId: roleId,
    );

    bool success = await UserService().updateUser(updatedUser);
    if (success) {
      scaffoldMessenger.showSnackBar(
          const SnackBar(content: Text("User updated successfully")));
      _loadData();
    } else {
      scaffoldMessenger
          .showSnackBar(const SnackBar(content: Text("Failed to update user")));
    }
  }

  Future<void> _deleteUser(String userId) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    bool success = await UserService().patchUser(userId);
    if (success) {
      scaffoldMessenger.showSnackBar(
          const SnackBar(content: Text("User deleted successfully")));
      _loadData();
    } else {
      scaffoldMessenger
          .showSnackBar(const SnackBar(content: Text("Failed to delete user")));
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
                columns: const [
                  DataColumn(label: Text('Email')),
                  DataColumn(label: Text('First Name')),
                  DataColumn(label: Text('Surname')),
                  DataColumn(label: Text('Role')),
                  DataColumn(label: Text('Actions')),
                ],
                rows: _users
                    .map((user) => DataRow(
                          cells: [
                            DataCell(Text(user.email)),
                            DataCell(Text(user.firstName)),
                            DataCell(Text(user.surname)),
                            DataCell(Text(_getRoleName(user.roleId))),
                            DataCell(Row(
                              children: <Widget>[
                                IconButton(
                                  icon: const Icon(Icons.edit,
                                      color: Color.fromARGB(255, 21, 121, 12)),
                                  onPressed: () {
                                    _showEditUserDialog(user);
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
                                              "Are you sure you want to delete the user?"),
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
                                                _deleteUser(user.id);
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
              onPressed: _showAddUserDialog,
              child: const Text('Add New User'),
            ),
          ),
        ],
      ),
    );
  }
}
