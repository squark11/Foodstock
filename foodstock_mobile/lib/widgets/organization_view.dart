import 'package:flutter/material.dart';
import 'package:foodstock_mobile/models/organization.dart';
import 'package:foodstock_mobile/services/organization_service.dart';

class OrganizationView extends StatefulWidget {
  const OrganizationView({super.key});

  @override
  OrganizationViewState createState() => OrganizationViewState();
}

class OrganizationViewState extends State<OrganizationView> {
  bool _isLoading = true;
  Organization? _organization;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _loadOrganization();
    });
  }

  void _loadOrganization() async {
    final scaffoldMessenger = ScaffoldMessenger.of(context);
    try {
      var organization = await OrganizationService().getOrganization();
      setState(() {
        _organization = organization;
      });
    } catch (e) {
      scaffoldMessenger.showSnackBar(
        SnackBar(content: Text('Failed to load organization: $e')),
      );
    } finally {
      setState(() {
        _isLoading = false;
      });
    }
  }

  void _showAddOrganizationDialog() {
    TextEditingController nameController = TextEditingController();
    TextEditingController ownerNameController = TextEditingController();
    TextEditingController ownerSurameController = TextEditingController();
    TextEditingController nipController = TextEditingController();
    TextEditingController countryController = TextEditingController();
    TextEditingController cityController = TextEditingController();
    TextEditingController cityCodeController = TextEditingController();
    TextEditingController streetController = TextEditingController();
    TextEditingController streetNumberController = TextEditingController();

    showDialog(
      context: context,
      builder: (BuildContext dialogContext) {
        return AlertDialog(
          title: const Text('Add New Organization'),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                TextField(
                  controller: nameController,
                  decoration: const InputDecoration(hintText: 'Name'),
                ),
                TextField(
                  controller: ownerNameController,
                  decoration: const InputDecoration(hintText: 'Owner Name'),
                ),
                TextField(
                  controller: ownerSurameController,
                  decoration: const InputDecoration(hintText: 'Owner Surname'),
                ),
                TextField(
                  controller: nipController,
                  decoration: const InputDecoration(hintText: 'NIP'),
                ),
                TextField(
                  controller: countryController,
                  decoration: const InputDecoration(hintText: 'Country'),
                ),
                TextField(
                  controller: cityController,
                  decoration: const InputDecoration(hintText: 'City'),
                ),
                TextField(
                  controller: cityCodeController,
                  decoration: const InputDecoration(hintText: 'City Code'),
                ),
                TextField(
                  controller: streetController,
                  decoration: const InputDecoration(hintText: 'Street'),
                ),
                TextField(
                  controller: streetNumberController,
                  decoration: const InputDecoration(hintText: 'Street Number'),
                ),
              ],
            ),
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
                _addOrganization(
                    dialogContext,
                    nameController.text,
                    ownerNameController.text,
                    ownerSurameController.text,
                    nipController.text,
                    countryController.text,
                    cityController.text,
                    cityCodeController.text,
                    streetController.text,
                    streetNumberController.text);
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  void _addOrganization(
    BuildContext dialogContext,
    String name,
    String ownerName,
    String ownerSurname,
    String nip,
    String country,
    String city,
    String cityCode,
    String street,
    String streetNumber,
  ) async {
    Organization newOrganization = Organization(
        name: name,
        ownerName: ownerName,
        ownerSurname: ownerSurname,
        nip: nip,
        country: country,
        city: city,
        cityCode: cityCode,
        street: street,
        streetNumber: streetNumber);

    final scaffoldMessenger = ScaffoldMessenger.of(dialogContext);

    bool success = await OrganizationService().addOrganization(newOrganization);
    if (mounted) {
      if (success) {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Organization added successfully")));
        _loadOrganization();
      } else {
        scaffoldMessenger.showSnackBar(
            const SnackBar(content: Text("Failed to add organization")));
      }
    }
  }

  void _showEditOrganizationDialog(Organization organization) {
    TextEditingController nameController =
        TextEditingController(text: organization.name);
    TextEditingController ownerNameController =
        TextEditingController(text: organization.ownerName);
    TextEditingController ownerSurameController =
        TextEditingController(text: organization.ownerSurname);
    TextEditingController nipController =
        TextEditingController(text: organization.nip);
    TextEditingController countryController =
        TextEditingController(text: organization.country);
    TextEditingController cityController =
        TextEditingController(text: organization.city);
    TextEditingController cityCodeController =
        TextEditingController(text: organization.cityCode);
    TextEditingController streetController =
        TextEditingController(text: organization.street);
    TextEditingController streetNumberController =
        TextEditingController(text: organization.streetNumber);

    showDialog(
      context: context,
      builder: (BuildContext dialogContext) {
        return AlertDialog(
          title: const Text('Edit Organization'),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                TextField(
                  controller: nameController,
                  decoration: const InputDecoration(hintText: 'Name'),
                ),
                TextField(
                  controller: ownerNameController,
                  decoration: const InputDecoration(hintText: 'Owner Name'),
                ),
                TextField(
                  controller: ownerSurameController,
                  decoration: const InputDecoration(hintText: 'Owner Surname'),
                ),
                TextField(
                  controller: nipController,
                  decoration: const InputDecoration(hintText: 'NIP'),
                ),
                TextField(
                  controller: countryController,
                  decoration: const InputDecoration(hintText: 'Country'),
                ),
                TextField(
                  controller: cityController,
                  decoration: const InputDecoration(hintText: 'City'),
                ),
                TextField(
                  controller: cityCodeController,
                  decoration: const InputDecoration(hintText: 'City Code'),
                ),
                TextField(
                  controller: streetController,
                  decoration: const InputDecoration(hintText: 'Street'),
                ),
                TextField(
                  controller: streetNumberController,
                  decoration: const InputDecoration(hintText: 'Street Number'),
                ),
              ],
            ),
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
                _updateOrganization(
                    dialogContext,
                    organization.id,
                    nameController.text,
                    ownerNameController.text,
                    ownerSurameController.text,
                    nipController.text,
                    countryController.text,
                    cityController.text,
                    cityCodeController.text,
                    streetController.text,
                    streetNumberController.text);
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  void _updateOrganization(
    BuildContext dialogContext,
    String id,
    String name,
    String ownerName,
    String ownerSurname,
    String nip,
    String country,
    String city,
    String cityCode,
    String street,
    String streetNumber,
  ) async {
    Organization updatedOrganization = Organization(
        id: id,
        name: name,
        ownerName: ownerName,
        ownerSurname: ownerSurname,
        nip: nip,
        country: country,
        city: city,
        cityCode: cityCode,
        street: street,
        streetNumber: streetNumber);

    final scaffoldMessenger = ScaffoldMessenger.of(dialogContext);

    bool success =
        await OrganizationService().updateOrganization(updatedOrganization);
    if (success) {
      scaffoldMessenger.showSnackBar(
          const SnackBar(content: Text("Organization updated successfully")));
      _loadOrganization();
    } else {
      scaffoldMessenger.showSnackBar(
          const SnackBar(content: Text("Failed to update organization")));
    }
  }

  @override
  Widget build(BuildContext context) {
    if (_isLoading) {
      return const Center(child: CircularProgressIndicator());
    }

    return SingleChildScrollView(
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: _organization != null
            ? _buildOrganizationCard()
            : _buildAddButton(),
      ),
    );
  }

  Widget _buildOrganizationCard() {
    return Card(
      elevation: 4.0,
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            _buildOrganizationDetailRow(
                Icons.business, 'Name', _organization!.name),
            _buildOrganizationDetailRow(Icons.person, 'Owner',
                '${_organization!.ownerName} ${_organization!.ownerSurname}'),
            _buildOrganizationDetailRow(
                Icons.fingerprint, 'NIP', _organization!.nip),
            _buildOrganizationDetailRow(
                Icons.flag, 'Country', _organization!.country),
            _buildOrganizationDetailRow(
                Icons.location_city, 'City', _organization!.city),
            _buildOrganizationDetailRow(
                Icons.code, 'City Code', _organization!.cityCode),
            _buildOrganizationDetailRow(
                Icons.streetview, 'Street', _organization!.street),
            _buildOrganizationDetailRow(Icons.format_list_numbered,
                'Street Number', _organization!.streetNumber),
            const SizedBox(height: 20),
            ElevatedButton(
              onPressed: () => _showEditOrganizationDialog(_organization!),
              child: const Text('Edit Organization'),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildAddButton() {
    return Center(
      child: ElevatedButton(
        onPressed: _showAddOrganizationDialog,
        child: const Text('Add New Organization'),
      ),
    );
  }

  Widget _buildOrganizationDetailRow(
      IconData icon, String label, String value) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8.0),
      child: Row(
        children: <Widget>[
          Icon(icon, color: Theme.of(context).primaryColor),
          const SizedBox(width: 10),
          Text('$label: ', style: const TextStyle(fontWeight: FontWeight.bold)),
          Flexible(child: Text(value)),
        ],
      ),
    );
  }
}
