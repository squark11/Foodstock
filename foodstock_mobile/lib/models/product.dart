class Product {
  final String id;
  late String name;
  late String? categoryId;
  late String? categoryName;
  late String? producentId;
  late String? producentName;
  late String expirationDate;
  late int quantity;
  late String barCode;
  late String? userId;
  late String? userFirstName;
  late String? userSurname;
  late String deliveryDate;
  late String? supplierId;
  late String? supplierName;
  late String? addedDate;

  Product({
    required this.id,
    required this.name,
    this.categoryId,
    this.categoryName,
    this.producentId,
    this.producentName,
    required this.expirationDate,
    required this.quantity,
    required this.barCode,
    this.userId,
    this.userFirstName,
    this.userSurname,
    required this.deliveryDate,
    this.supplierId,
    this.supplierName,
    this.addedDate,
  }) {
    if (barCode.length != 13 || int.tryParse(barCode) == null) {
      throw ArgumentError('barCode must be 13 digits long');
    }
  }

  // JSON
  factory Product.fromJson(Map<String, dynamic> json) {
    return Product(
      id: json['id'],
      name: json['name'],
      categoryId: json['categoryId'],
      categoryName: json['categoryName'],
      producentId: json['producentId'],
      producentName: json['producent'],
      expirationDate: json['expirationDate'],
      quantity: json['quantity'],
      barCode: json['barCode'],
      addedDate: json['addedDate'],
      userFirstName: json['userFirstName'],
      userSurname: json['userSurname'],
      deliveryDate: json['deliveryDate'],
      supplierId: json['supplierId'],
      supplierName: json['supplier'],
    );
  }
  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'categoryId': categoryId!,
      'producentId': producentId!,
      'expirationDate': expirationDate,
      'quantity': quantity,
      'barCode': barCode,
      'userId': userId!,
      'deliveryDate': deliveryDate,
      'supplierId': supplierId!,
    };
  }

  Map<String, dynamic> toJsonPut() {
    return {
      'name': name,
      'categoryId': categoryId,
      'producentId': producentId,
      'expirationDate': expirationDate,
      'quantity': quantity,
      'barCode': barCode,
      'userId': userId,
      'deliveryDate': deliveryDate,
      'supplierId': supplierId,
    };
  }
}
