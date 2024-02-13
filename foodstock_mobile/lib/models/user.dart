class User {
  final String id;
  final String email;
  final String? password;
  final String firstName;
  final String surname;
  final String roleId;
  final String roleName;

  User({
    this.id = "",
    required this.email,
    this.password,
    required this.firstName,
    required this.surname,
    required this.roleId,
    this.roleName = "",
  });

  // JSON
  factory User.fromJson(Map<String, dynamic> json) {
    return User(
        id: json['id'],
        email: json['email'],
        password: json['password'],
        firstName: json['firstName'],
        surname: json['surname'],
        roleId: json['roleId'] ?? 'Unknown',
        roleName: json['roleName']);
  }
  Map<String, dynamic> toJson() {
    return {
      'email': email,
      'password': password,
      'confirmPassword': password,
      'firstName': firstName,
      'surname': surname,
      'roleId': roleId,
    };
  }
}
