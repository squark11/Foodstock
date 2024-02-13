class Category {
  final String id;
  final String categoryName;

  Category({
    required this.id,
    required this.categoryName,
  });

  // JSON
  factory Category.fromJson(Map<String, dynamic> json) {
    return Category(
      id: json['id'],
      categoryName: json['categoryName'],
    );
  }
  Map<String, dynamic> toJson() {
    return {
      'categoryName': categoryName,
    };
  }
}
