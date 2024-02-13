import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:foodstock_mobile/models/organization.dart';
import 'package:flutter/foundation.dart' show kIsWeb;
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class OrganizationService {
  late String _baseUrl;
  final _storage = const FlutterSecureStorage();

  OrganizationService() {
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

// ____________________________________ORGANIZATION_____________________________
// GET:
  Future<Organization?> getOrganization() async {
    String? token = await _getToken();
    final response = await http.get(
      Uri.parse('$_baseUrl/organizations'),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
    );

    if (response.statusCode == 200) {
      List<dynamic> data = json.decode(response.body);
      if (data.isNotEmpty) {
        return Organization.fromJson(data[0]);
      }
      return null;
    } else {
      throw Exception('Failed to load organization');
    }
  }

// POST:
  Future<bool> addOrganization(Organization organization) async {
    String? token = await _getToken();
    final response = await http.post(
      Uri.parse('$_baseUrl/organizations'),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
      body: json.encode(organization.toJson()),
    );

    return response.statusCode == 200;
  }

// PUT:
  Future<bool> updateOrganization(Organization organization) async {
    String? token = await _getToken();
    final response = await http.put(
      Uri.parse('$_baseUrl/organizations/${organization.id}'),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
      body: json.encode(organization.toJson()),
    );

    return response.statusCode == 204;
  }
}
