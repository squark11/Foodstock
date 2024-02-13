class Supplier {
  final String id;
  final String name;

  Supplier({
    this.id = "",
    required this.name,
  });

  // JSON
  factory Supplier.fromJson(Map<String, dynamic> json) {
    return Supplier(
      id: json['id'],
      name: json['name'],
    );
  }
  Map<String, dynamic> toJson() {
    return {
      'name': name,
    };
  }

  Map<String, dynamic> toJsonOrder() {
    return {
      'supplierId': id,
    };
  }
}
