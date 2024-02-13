import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:foodstock_mobile/models/product.dart';
import 'package:flutter/foundation.dart' show kIsWeb;
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class ProductService {
  late String _baseUrl;
  final _storage = const FlutterSecureStorage();

  ProductService() {
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

  // ____________________________________PRODUCT_________________________________
  // GET:
  Future<List<Product>> getProducts() async {
    String? token = await _getToken();
    final response = await http.get(
      Uri.parse('$_baseUrl/products'),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer $token",
      },
    );

    if (response.statusCode == 200) {
      Iterable list = json.decode(response.body);
      return list.map((product) => Product.fromJson(product)).toList();
    } else {
      throw Exception('Failed to load products');
    }
  }

  // POST:
  Future<bool> addProduct(Product product) async {
    String? token = await _getToken();
    final response = await http.post(
      Uri.parse('$_baseUrl/products'),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer $token",
      },
      body: json.encode(product.toJson()),
    );

    return response.statusCode == 200;
  }

  // GET by ID:
  Future<Product> getProductById(String id) async {
    String? token = await _getToken();
    final response = await http.get(
      Uri.parse('$_baseUrl/products/$id'),
      headers: {
        "Authorization": "Bearer $token",
      },
    );

    if (response.statusCode == 200) {
      return Product.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to load product');
    }
  }

  // PUT:
  Future<bool> updateProduct(Product product) async {
    String? token = await _getToken();
    final response = await http.put(
      Uri.parse('$_baseUrl/products/${product.id}'),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer $token",
      },
      body: jsonEncode(product.toJsonPut()),
    );

    if (response.statusCode == 204) {
      return true;
    } else {
      throw Exception('Failed to update product');
    }
  }

  // DELETE:
  Future<bool> deleteProduct(String id) async {
    String? token = await _getToken();
    final response = await http.delete(
      Uri.parse('$_baseUrl/products/$id'),
      headers: {
        "Authorization": "Bearer $token",
      },
    );

    return response.statusCode == 204;
  }

  // GET by Category ID:
  Future<List<Product>> getProductsByCategoryId(String categoryId) async {
    String? token = await _getToken();
    final response = await http.get(
      Uri.parse('$_baseUrl/products/byCategoryId?categoryId=$categoryId'),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
    );

    if (response.statusCode == 200) {
      List<dynamic> data = json.decode(response.body);
      return data.map((json) => Product.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load products by category');
    }
  }
}
