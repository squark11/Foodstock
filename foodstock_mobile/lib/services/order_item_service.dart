import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:flutter/foundation.dart' show kIsWeb;
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:foodstock_mobile/models/order_item.dart';

class OrderItemService {
  late String _baseUrl;
  final _storage = const FlutterSecureStorage();

  OrderItemService() {
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

// ____________________________________OrderItem________________________________
//GET:
  Future<List<OrderItem>> getOrderItems() async {
    String? token = await _getToken();
    final response = await http.get(
      Uri.parse('$_baseUrl/orderItems'),
      headers: {
        'Authorization': 'Bearer $token',
        'Content-Type': 'application/json',
      },
    );

    if (response.statusCode == 200) {
      List<dynamic> data = json.decode(response.body);
      return data.map((json) => OrderItem.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load order items');
    }
  }

//POST:
  Future<bool> createOrderItem(OrderItem orderItem) async {
    String? token = await _getToken();
    final response = await http.post(
      Uri.parse('$_baseUrl/orderItems'),
      headers: {
        'Authorization': 'Bearer $token',
        'Content-Type': 'application/json',
      },
      body: json.encode(orderItem.toJson()),
    );

    return response.statusCode == 200;
  }

//POST By Product:
  Future<bool> createOrderItemByProduct(
      String productId, OrderItem orderItem) async {
    String? token = await _getToken();
    final response = await http.post(
      Uri.parse('$_baseUrl/orderItems/byProduct/$productId'),
      headers: {
        'Authorization': 'Bearer $token',
        'Content-Type': 'application/json',
      },
      body: json.encode(orderItem.toJson()),
    );

    return response.statusCode == 200;
  }

//PATCH:
  Future<bool> updateOrderItem(OrderItem orderItem) async {
    String? token = await _getToken();
    final response = await http.patch(
      Uri.parse('$_baseUrl/orderItems/${orderItem.id}'),
      headers: {
        'Authorization': 'Bearer $token',
        'Content-Type': 'application/json',
      },
      body: json.encode(orderItem.toJson()),
    );

    return response.statusCode == 204;
  }

//DELETE:
  Future<bool> deleteOrderItem(String id) async {
    String? token = await _getToken();
    final response = await http.delete(
      Uri.parse('$_baseUrl/orderItems/$id'),
      headers: {
        'Authorization': 'Bearer $token',
        'Content-Type': 'application/json',
      },
    );

    return response.statusCode == 204;
  }
}
