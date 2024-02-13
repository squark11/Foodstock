import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:foodstock_mobile/models/category.dart';
import 'package:flutter/foundation.dart' show kIsWeb;
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class CategoryService {
  late String _baseUrl;
  final _storage = const FlutterSecureStorage();

  CategoryService() {
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

// ____________________________________CATEGORY_________________________________
  // GET:
  Future<List<Category>> getCategories() async {
    String? token = await _getToken();
    final response = await http.get(Uri.parse('$_baseUrl/categories'),
        headers: {"Authorization": "Bearer $token"});
    if (response.statusCode == 200) {
      Iterable list = json.decode(response.body);
      return list.map((category) => Category.fromJson(category)).toList();
    } else {
      throw Exception('Failed to load categories');
    }
  }

  // POST:
  Future<bool> addCategory(Category category) async {
    String? token = await _getToken();
    final response = await http.post(
      Uri.parse('$_baseUrl/categories'),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer $token"
      },
      body: json.encode(category.toJson()),
    );
    return response.statusCode == 200;
  }

  // PUT:
  Future<bool> editCategory(String id, Category category) async {
    String? token = await _getToken();
    final response = await http.put(
      Uri.parse('$_baseUrl/categories/$id'),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer $token"
      },
      body: json.encode(category.toJson()),
    );
    return response.statusCode == 204;
  }

  // DELETE:
  Future<bool> deleteCategory(String id) async {
    String? token = await _getToken();
    final response = await http.delete(Uri.parse('$_baseUrl/categories/$id'),
        headers: {"Authorization": "Bearer $token"});
    return response.statusCode == 204;
  }
}
