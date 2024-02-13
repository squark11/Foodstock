import 'package:flutter/material.dart';
import 'package:foodstock_mobile/screens/login_screen.dart';

void main() {
  runApp(const FoodStockApp());
}

class FoodStockApp extends StatelessWidget {
  const FoodStockApp({super.key});
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'FoodStock Mobile',
      theme: ThemeData(
        primarySwatch: Colors.indigo,
        visualDensity: VisualDensity.adaptivePlatformDensity,
      ),
      home: const LoginScreen(),
    );
  }
}
