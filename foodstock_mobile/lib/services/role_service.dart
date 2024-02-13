import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:foodstock_mobile/models/role.dart';
import 'package:flutter/foundation.dart' show kIsWeb;
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class RoleService {
  late String _baseUrl;
  final _storage = const FlutterSecureStorage();

  RoleService() {
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

// ____________________________________ROLE_____________________________________
  // GET:
  Future<List<Role>> getRoles() async {
    String? token = await _getToken();
    final response = await http.get(
      Uri.parse('$_baseUrl/roles'),
      headers: {'Authorization': 'Bearer $token'},
    );

    if (response.statusCode == 200) {
      List<dynamic> data = json.decode(response.body);
      return data.map((json) => Role.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load roles');
    }
  }

  // POST:
  Future<bool> addRole(Role role) async {
    String? token = await _getToken();
    final response = await http.post(
      Uri.parse('$_baseUrl/roles'),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
      body: json.encode(role.toJson()),
    );

    return response.statusCode == 200;
  }

  // PUT:
  Future<bool> updateRole(Role role) async {
    String? token = await _getToken();
    final response = await http.put(
      Uri.parse('$_baseUrl/roles/${role.id}'),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
      body: json.encode(role.toJson()),
    );

    return response.statusCode == 204;
  }

  // DELETE:
  Future<bool> deleteRole(String id) async {
    String? token = await _getToken();
    final response = await http.delete(
      Uri.parse('$_baseUrl/roles/$id'),
      headers: {'Authorization': 'Bearer $token'},
    );

    return response.statusCode == 204;
  }
}
