class OrderItem {
  final String id;
  final String name;
  final DateTime? expirationDate;
  final String? orderId;
  final int quantity;
  final String categoryId;
  final String? categoryName;
  final String producentId;
  final String? producentName;
  final String? barCode;
  final String? supplierId;
  final String? supplierName;

  OrderItem({
    this.id = '',
    required this.name,
    this.expirationDate,
    this.orderId,
    required this.quantity,
    required this.categoryId,
    this.categoryName,
    required this.producentId,
    this.producentName,
    this.barCode,
    this.supplierId,
    this.supplierName,
  });

  factory OrderItem.fromJson(Map<String, dynamic> json) {
    DateTime? parseDateTime(String? dateTimeStr) {
      if (dateTimeStr == null) return null;
      return DateTime.parse(dateTimeStr);
    }

    return OrderItem(
      id: json['id'],
      name: json['name'],
      expirationDate: parseDateTime(json['expirationDate']),
      orderId: json['orderId'],
      quantity: json['quantity'],
      categoryId: json['categoryId'],
      categoryName: json['categoryName'],
      producentId: json['producentId'],
      producentName: json['producentName'],
      barCode: json['barCode'],
      supplierId: json['supplierId'],
      supplierName: json['supplierName'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'orderId': orderId,
      'quantity': quantity,
      'categoryId': categoryId,
      'producentId': producentId,
      'barCode': barCode,
    };
  }
}
