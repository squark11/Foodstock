import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:foodstock_mobile/models/user.dart';
import 'package:foodstock_mobile/models/role.dart';
import 'package:foodstock_mobile/models/login_response.dart';
import 'package:flutter/foundation.dart' show kIsWeb;
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class UserService {
  late String _baseUrl;
  final _storage = const FlutterSecureStorage();

  UserService() {
    if (kIsWeb) {
      _baseUrl = "http://localhost/api";
    } else {
      if (Platform.isAndroid) {
        _baseUrl = "http://10.0.2.2/api";
      } else if (Platform.isIOS || Platform.isWindows) {
        _baseUrl = "http://localhost/api";
      }
    }
  }

  Future<String?> _getToken() async {
    return await _storage.read(key: 'token');
  }

// ____________________________________USER_____________________________________
  // GET:
  Future<User> getUser(String id) async {
    String? token = await _getToken();
    final response = await http.get(
      Uri.parse('$_baseUrl/accounts/$id'),
      headers: {
        'Authorization': 'Bearer $token',
        'Content-Type': 'application/json',
      },
    );

    if (response.statusCode == 200) {
      return User.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to load profile');
    }
  }

  // GET CURRENT:
  Future<User> getCurrentUser() async {
    String? token = await _getToken();
    String? userId = await _storage.read(key: 'userId');
    if (userId == null) {
      throw Exception('No user ID found');
    }

    final response = await http.get(
      Uri.parse('$_baseUrl/accounts/$userId'),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer $token",
      },
    );

    if (response.statusCode == 200) {
      return User.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to load user data');
    }
  }

  // LOGIN USER:
  Future<LoginResponse> loginUser(String email, String password) async {
    final url = Uri.parse('$_baseUrl/accounts/login');

    try {
      final response = await http.post(
        url,
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: jsonEncode(<String, String>{
          'email': email,
          'password': password,
        }),
      );

      if (response.statusCode == 200) {
        final responseData = json.decode(response.body);

        return LoginResponse.fromJson(responseData);
      } else {
        const errorMessage = 'Invalid username or password';
        throw (errorMessage);
      }
    } catch (e) {
      throw Exception(e);
    }
  }

  // GET ROLE by ID
  Future<Role> getRoleById(String roleId) async {
    String? token = await _getToken();
    final response = await http.get(
      Uri.parse('$_baseUrl/roles/$roleId'),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer $token",
      },
    );

    if (response.statusCode == 200) {
      return Role.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to load role');
    }
  }

  //CHANGE PASSWORD
  Future<bool> changePassword(
      String userId, String newPassword, String confirmPassword) async {
    String? token = await _getToken();
    final response = await http.patch(
      Uri.parse('$_baseUrl/accounts/changePassword/$userId'),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer $token",
      },
      body: jsonEncode({
        "id": userId,
        "password": newPassword,
        "confirmPassword": confirmPassword,
      }),
    );

    return response.statusCode == 204;
  }

  // GET Users List:
  Future<List<User>> getUsers() async {
    String? token = await _getToken();
    final response = await http.get(
      Uri.parse('$_baseUrl/accounts'),
      headers: {'Authorization': 'Bearer $token'},
    );

    if (response.statusCode == 200) {
      List<dynamic> data = json.decode(response.body);
      return data.map((json) => User.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load users');
    }
  }

  // POST:
  Future<bool> addUser(User user) async {
    String? token = await _getToken();
    final response = await http.post(
      Uri.parse('$_baseUrl/accounts/registerUser'),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
      body: json.encode(user.toJson()),
    );

    return response.statusCode == 200;
  }

  // PATCH:
  Future<bool> updateUser(User user) async {
    String? token = await _getToken();
    final response = await http.patch(
      Uri.parse('$_baseUrl/accounts/${user.id}'),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
      body: json.encode(user.toJson()),
    );

    return response.statusCode == 204;
  }

  // PATCH - inactive user:
  Future<bool> patchUser(String id) async {
    String? token = await _getToken();
    final response = await http.patch(
      Uri.parse('$_baseUrl/accounts/deleteUser/$id'),
      headers: {'Authorization': 'Bearer $token'},
    );

    return response.statusCode == 204;
  }
}
