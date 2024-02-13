import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:flutter/foundation.dart' show kIsWeb;
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:foodstock_mobile/models/order.dart';
import 'package:foodstock_mobile/models/supplier.dart';

class OrderService {
  late String _baseUrl;
  final _storage = const FlutterSecureStorage();

  OrderService() {
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

// ____________________________________ORDER____________________________________
//GET:
  Future<List<Order>> getOrders() async {
    String? token = await _getToken();
    final response = await http.get(
      Uri.parse('$_baseUrl/orders'),
      headers: {'Authorization': 'Bearer $token'},
    );

    if (response.statusCode == 200) {
      List<dynamic> data = json.decode(response.body);
      return data.map((json) => Order.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load orders');
    }
  }

//GET by Id:
  Future<Order> getOrderById(String id) async {
    String? token = await _getToken();
    final response = await http.get(
      Uri.parse('$_baseUrl/orders/$id'),
      headers: {
        'Authorization': 'Bearer $token',
        'Content-Type': 'application/json',
      },
    );

    if (response.statusCode == 200) {
      return Order.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to load order');
    }
  }

//POST:
  Future<bool> createOrder(Supplier order) async {
    String? token = await _getToken();
    final response = await http.post(
      Uri.parse('$_baseUrl/orders'),
      headers: {
        'Authorization': 'Bearer $token',
        'Content-Type': 'application/json',
      },
      body: json.encode(order.toJsonOrder()),
    );

    return response.statusCode == 200;
  }

//PATCH:
  Future<bool> updateOrderStatus(String orderId, String newStatus) async {
    String? token = await _getToken();
    final response = await http.patch(
      Uri.parse('$_baseUrl/orders/$orderId'),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
      body: jsonEncode({'orderStatus': newStatus}),
    );

    return response.statusCode == 204;
  }

//DELETE:
  Future<bool> deleteOrder(String id) async {
    String? token = await _getToken();
    final response = await http.delete(
      Uri.parse('$_baseUrl/orders/$id'),
      headers: {
        'Authorization': 'Bearer $token',
        'Content-Type': 'application/json',
      },
    );

    return response.statusCode == 204;
  }
}
