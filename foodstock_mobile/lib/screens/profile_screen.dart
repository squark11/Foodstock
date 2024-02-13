// ignore_for_file: use_build_context_synchronously

import 'package:flutter/material.dart';
import 'package:foodstock_mobile/models/user.dart';
import 'package:foodstock_mobile/screens/login_screen.dart';
import 'package:foodstock_mobile/services/user_service.dart';

class ProfileScreen extends StatelessWidget {
  final User user;

  const ProfileScreen({super.key, required this.user});

  void _showChangePasswordDialog(BuildContext context) {
    final TextEditingController newPasswordController = TextEditingController();
    final TextEditingController confirmPasswordController =
        TextEditingController();

    showDialog(
      context: context,
      builder: (BuildContext dialogContext) {
        return AlertDialog(
          title: const Text('Change Password'),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                TextField(
                  controller: newPasswordController,
                  decoration: const InputDecoration(hintText: 'New Password'),
                  obscureText: true,
                ),
                TextField(
                  controller: confirmPasswordController,
                  decoration:
                      const InputDecoration(hintText: 'Confirm New Password'),
                  obscureText: true,
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
              child: const Text('Change'),
              onPressed: () {
                _changePassword(dialogContext, newPasswordController.text,
                    confirmPasswordController.text);
              },
            ),
          ],
        );
      },
    );
  }

  void _changePassword(BuildContext dialogContext, String newPassword,
      String confirmPassword) async {
    if (newPassword != confirmPassword) {
      ScaffoldMessenger.of(dialogContext).showSnackBar(
          const SnackBar(content: Text("Passwords do not match")));
      return;
    }

    try {
      bool success = await UserService()
          .changePassword(user.id, newPassword, confirmPassword);
      Navigator.of(dialogContext).pop();

      ScaffoldMessenger.of(dialogContext).showSnackBar(SnackBar(
          content: Text(success
              ? "Password changed successfully"
              : "Failed to change password")));
    } catch (e) {
      ScaffoldMessenger.of(dialogContext)
          .showSnackBar(SnackBar(content: Text("Error: ${e.toString()}")));
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Profile'),
      ),
      body: Center(
        child: SingleChildScrollView(
          child: SizedBox(
            width: 350,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                const Icon(
                  Icons.account_circle,
                  size: 200.0,
                  color: Color.fromARGB(235, 6, 70, 207),
                ),
                Card(
                  margin: const EdgeInsets.all(8.0),
                  child: ListTile(
                    leading: const Icon(Icons.email),
                    title: Text(user.email,
                        style: const TextStyle(fontWeight: FontWeight.bold)),
                  ),
                ),
                Card(
                  margin: const EdgeInsets.all(8.0),
                  child: ListTile(
                    leading: const Icon(Icons.person),
                    title: Text(
                      '${user.firstName} ${user.surname}',
                      style: const TextStyle(fontWeight: FontWeight.bold),
                    ),
                  ),
                ),
                Card(
                  margin: const EdgeInsets.all(8.0),
                  child: ListTile(
                    leading: const Icon(Icons.security),
                    title: Text(user.roleName,
                        style: const TextStyle(fontWeight: FontWeight.bold)),
                  ),
                ),
                const SizedBox(height: 20),
                ElevatedButton(
                  style: ElevatedButton.styleFrom(
                    backgroundColor: const Color.fromARGB(255, 11, 38, 160),
                    padding: const EdgeInsets.symmetric(
                        horizontal: 30, vertical: 15),
                  ),
                  onPressed: () {
                    _showChangePasswordDialog(context);
                  },
                  child: const Text('Change Password'),
                ),
                const SizedBox(height: 20),
                ElevatedButton(
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.red,
                    padding: const EdgeInsets.symmetric(
                        horizontal: 30, vertical: 15),
                  ),
                  onPressed: () => Navigator.pushAndRemoveUntil(
                    context,
                    MaterialPageRoute(
                        builder: (context) => const LoginScreen()),
                    (route) => false,
                  ),
                  child: const Text('Logout'),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
