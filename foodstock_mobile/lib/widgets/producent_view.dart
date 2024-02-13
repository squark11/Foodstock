import 'package:flutter/material.dart';
import 'package:foodstock_mobile/models/producent.dart';
import 'package:foodstock_mobile/services/producent_service.dart';

class ProducentView extends StatefulWidget {
  const ProducentView({super.key});

  @override
  ProducentViewState createState() => ProducentViewState();
}

class ProducentViewState extends State<ProducentView> {
  List<Producent> _producents = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _loadProducents();
    });
  }

  void _showAddProducentDialog() {
    TextEditingController nameController = TextEditingController();

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Add New Producent'),
          content: TextField(
            controller: nameController,
            decoration: const InputDecoration(hintText: 'Producent Name'),
          ),
          actions: <Widget>[
            TextButton(
              child: const Text('Cancel'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: const Text('Add'),
              onPressed: () {
                Navigator.of(context).pop();
                _addProducent(nameController.text);
              },
            ),
          ],
        );
      },
    );
  }

  void _addProducent(String name) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    try {
      Producent newProducent = Producent(name: name);
      bool success = await ProducentService().addProducent(newProducent);
      if (success) {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Producent added successfully")));
        _loadProducents();
      } else {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Failed to add producent")));
      }
    } catch (e) {
      scaffoldMessenger
          .showSnackBar(SnackBar(content: Text("Error: ${e.toString()}")));
    }
  }

  void _showEditProducentDialog(Producent producent) {
    TextEditingController nameController =
        TextEditingController(text: producent.name);

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Edit Producent'),
          content: TextField(
            controller: nameController,
            decoration: const InputDecoration(hintText: 'Name'),
          ),
          actions: <Widget>[
            TextButton(
              child: const Text('Cancel'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: const Text('Save'),
              onPressed: () {
                Navigator.of(context).pop();
                _updateProducent(producent.id, nameController.text);
              },
            ),
          ],
        );
      },
    );
  }

  void _updateProducent(String producentId, String newName) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    try {
      Producent updatedProducent = Producent(id: producentId, name: newName);
      bool success = await ProducentService().updateProducent(updatedProducent);
      if (success) {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Producent updated successfully")));
        _loadProducents();
      } else {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Failed to update producent")));
      }
    } catch (e) {
      scaffoldMessenger
          .showSnackBar(SnackBar(content: Text("Error: ${e.toString()}")));
    }
  }

  void _deleteProducent(String producentId) async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);

    try {
      bool success = await ProducentService().deleteProducent(producentId);
      if (success) {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Producent deleted successfully")));
        _loadProducents();
      } else {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Failed to delete producent")));
      }
    } catch (e) {
      scaffoldMessenger
          .showSnackBar(SnackBar(content: Text("Error: ${e.toString()}")));
    }
  }

  Future<void> _loadProducents() async {
    setState(() {
      _isLoading = true;
    });
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      List<Producent> producents = await ProducentService().getProducents();
      setState(() {
        _producents = producents;
      });
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load producents: $e')),
      );
    } finally {
      setState(() {
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    if (_isLoading) {
      return const CircularProgressIndicator();
    }

    return Center(
      child: Column(
        children: [
          SingleChildScrollView(
            scrollDirection: Axis.horizontal,
            child: SingleChildScrollView(
              scrollDirection: Axis.vertical,
              child: DataTable(
                columnSpacing: 10.0,
                columns: [
                  DataColumn(
                    label: SizedBox(
                      width: MediaQuery.of(context).size.width * 0.55,
                      child: const Text('Name'),
                    ),
                  ),
                  DataColumn(
                    label: SizedBox(
                      width: MediaQuery.of(context).size.width * 0.25,
                      child: const Text('Actions'),
                    ),
                  ),
                ],
                rows: _producents
                    .map((producent) => DataRow(
                          cells: [
                            DataCell(Text(producent.name)),
                            DataCell(Row(
                              children: <Widget>[
                                IconButton(
                                  icon: const Icon(Icons.edit,
                                      color: Color.fromARGB(255, 21, 121, 12)),
                                  onPressed: () {
                                    _showEditProducentDialog(producent);
                                  },
                                ),
                                IconButton(
                                  icon: const Icon(Icons.delete,
                                      color: Color.fromARGB(255, 146, 17, 8)),
                                  onPressed: () {
                                    showDialog(
                                      context: context,
                                      builder: (BuildContext context) {
                                        return AlertDialog(
                                          title: const Text("Confirm"),
                                          content: const Text(
                                              "Are you sure you want to delete the producent?"),
                                          actions: [
                                            TextButton(
                                              child: const Text("Cancel"),
                                              onPressed: () {
                                                Navigator.of(context).pop();
                                              },
                                            ),
                                            TextButton(
                                              child: const Text("Delete"),
                                              onPressed: () async {
                                                var navigator =
                                                    Navigator.of(context);
                                                _deleteProducent(producent.id);
                                                navigator.pop();
                                              },
                                            ),
                                          ],
                                        );
                                      },
                                    );
                                  },
                                ),
                              ],
                            )),
                          ],
                        ))
                    .toList(),
              ),
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: ElevatedButton(
              onPressed: _showAddProducentDialog,
              child: const Text('Add New Producent'),
            ),
          ),
        ],
      ),
    );
  }
}
