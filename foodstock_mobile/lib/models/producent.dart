class Producent {
  final String id;
  final String name;

  Producent({
    this.id = "",
    required this.name,
  });

  // JSON
  factory Producent.fromJson(Map<String, dynamic> json) {
    return Producent(
      id: json['id'],
      name: json['name'],
    );
  }
  Map<String, dynamic> toJson() {
    return {
      'name': name,
    };
  }
}
