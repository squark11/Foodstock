class Validators {
  static String? validateNotEmpty(String? value) {
    if (value == null || value.isEmpty) {
      return 'The field cannot be empty';
    }
    return null;
  }

  static String? validateEmail(String? value) {
    if (value == null || value.isEmpty) {
      return 'E-mail cannot be empty';
    }
    const pattern = r'^[^@\s]+@[^@\s]+\.[^@\s]+$';
    final regExp = RegExp(pattern);
    if (!regExp.hasMatch(value)) {
      return 'Enter a valid email address';
    }
    return null;
  }

  static String? validatePassword(String? value) {
    if (value == null || value.isEmpty) {
      return 'Password cannot be empty';
    }
    if (value.length < 6) {
      return 'Password must be at least 6 characters long';
    }
    return null;
  }

  static String? validateBarcode(String? value) {
    if (value == null || value.isEmpty) {
      return 'Barcode cannot be empty';
    }
    const pattern = r'^\d{13}$';
    final regExp = RegExp(pattern);
    if (!regExp.hasMatch(value)) {
      return 'Barcode must contain exactly 13 digits';
    }
    return null;
  }

  static String? validateQuantity(String? value) {
    if (value == null || value.isEmpty) {
      return 'Quantity cannot be empty';
    }
    if (int.tryParse(value) == null) {
      return 'Enter a valid numeric value';
    }
    return null;
  }

  static String? validateDate(String? value, {DateTime? minDate}) {
    if (value == null || value.isEmpty) {
      return 'Date cannot be empty';
    }

    DateTime? date;
    try {
      date = DateTime.parse(value);
    } catch (e) {
      return 'Invalid date format';
    }

    if (minDate != null && date.isBefore(minDate)) {
      return 'Date should not be earlier than ${minDate.toLocal()}';
    }

    return null;
  }

  static String? validateCategory(String? value) {
    if (value == null || value.isEmpty) {
      return 'Category cannot be empty';
    }
    return null;
  }

  static String? validateProducent(String? value) {
    if (value == null || value.isEmpty) {
      return 'Producent cannot be empty';
    }
    return null;
  }

  static String? validateSupplier(String? value) {
    if (value == null || value.isEmpty) {
      return 'Supplier cannot be empty';
    }
    return null;
  }
}
