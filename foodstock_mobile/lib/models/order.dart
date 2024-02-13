import 'package:foodstock_mobile/models/organization.dart';
import 'package:foodstock_mobile/models/supplier.dart';
import 'package:foodstock_mobile/models/order_item.dart';

class Order {
  final String id;
  final String orderName;
  final DateTime orderDate;
  final DateTime? acceptanceOfTheOrderDate;
  final String orderBy;
  final String? acceptedBy;
  final String orderStatus;
  final Organization organization;
  final Supplier supplier;
  final List<OrderItem> orderItems;

  Order({
    this.id = '',
    required this.orderName,
    required this.orderDate,
    this.acceptanceOfTheOrderDate,
    required this.orderBy,
    this.acceptedBy,
    required this.orderStatus,
    required this.organization,
    required this.supplier,
    required this.orderItems,
  });

  factory Order.fromJson(Map<String, dynamic> json) {
    var orderItemsJson = json['orderItems'] as List;
    List<OrderItem> orderItemsList =
        orderItemsJson.map((i) => OrderItem.fromJson(i)).toList();

    DateTime? parseDateTime(String? dateTimeStr) {
      if (dateTimeStr == null) return null;
      return DateTime.parse(dateTimeStr);
    }

    return Order(
      id: json['id'],
      orderName: json['orderName'],
      orderDate: parseDateTime(json['orderDate'])!,
      acceptanceOfTheOrderDate: parseDateTime(json['acceptanceOfTheOrderDate']),
      orderBy: json['orderBy'],
      acceptedBy: json['acceptedBy'],
      orderStatus: json['orderStatus'],
      organization: Organization.fromJson(json['organization']),
      supplier: Supplier.fromJson(json['supplier']),
      orderItems: orderItemsList,
    );
  }

  Map<String, dynamic> toJson() => {
        'orderStatus': orderStatus,
      };
}
