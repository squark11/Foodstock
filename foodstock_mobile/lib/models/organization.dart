class Organization {
  final String id;
  final String name;
  final String ownerName;
  final String ownerSurname;
  final String nip;
  final String country;
  final String city;
  final String cityCode;
  final String street;
  final String streetNumber;

  Organization({
    this.id = "",
    required this.name,
    required this.ownerName,
    required this.ownerSurname,
    required this.nip,
    required this.country,
    required this.city,
    required this.cityCode,
    required this.street,
    required this.streetNumber,
  });

  factory Organization.fromJson(Map<String, dynamic> json) {
    return Organization(
      id: json['id'],
      name: json['name'],
      ownerName: json['ownerName'],
      ownerSurname: json['ownerSurname'],
      nip: json['nip'],
      country: json['country'],
      city: json['city'],
      cityCode: json['cityCode'],
      street: json['street'],
      streetNumber: json['streetNumber'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'ownerName': ownerName,
      'ownerSurname': ownerSurname,
      'nip': nip,
      'country': country,
      'city': city,
      'cityCode': cityCode,
      'street': street,
      'streetNumber': streetNumber,
    };
  }
}
