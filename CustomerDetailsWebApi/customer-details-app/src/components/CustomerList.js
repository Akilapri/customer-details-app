import React, { useState, useEffect } from 'react';
import axios from 'axios';

const CustomerList = () => {
    const [customers, setCustomers] = useState([]);
    const [editingCustomer, setEditingCustomer] = useState(null);

    useEffect(() => {
        fetchCustomers();
    }, []);

    const fetchCustomers = async () => {
        try {
            const response = await axios.get('https://localhost:5000/api/customerdetails');
            setCustomers(response.data);
        } catch (error) {
            console.error('Error fetching customers:', error);
        }
    };

    const deleteCustomer = async (customerId) => {
        try {
            await axios.delete(`https://localhost:5000/api/customerdetails/${customerId}`);
            fetchCustomers();
        } catch (error) {
            console.error('Error deleting customer:', error);
        }
    };

    const startEditing = (customer) => {
        setEditingCustomer({ ...customer });
    };

    const cancelEditing = () => {
        setEditingCustomer(null);
    };

    const updateCustomer = async () => {
        try {
            await axios.put(`https://localhost:5000/api/customerdetails/${editingCustomer.id}`, editingCustomer);
            setEditingCustomer(null);
            fetchCustomers();
        } catch (error) {
            console.error('Error updating customer:', error);
        }
    };

    const handleInputChange = (e, key) => {
        setEditingCustomer({ ...editingCustomer, [key]: e.target.value });
    };

    return (
        <div>
            <h2>Customer List</h2>
            <ul>
                {customers.map((customer) => (
                    <li key={customer.id}>
                        {editingCustomer && editingCustomer.id === customer.id ? (
                            <>
                                <input type="text" value={editingCustomer.id} onChange={(e) => handleInputChange(e, 'id')} />
                                <input type="text" value={editingCustomer.firstName} onChange={(e) => handleInputChange(e, 'firstName')} />
                                <input type="text" value={editingCustomer.lastName} onChange={(e) => handleInputChange(e, 'lastName')} />
                                <button onClick={updateCustomer}>Save</button>
                                <button onClick={cancelEditing}>Cancel</button>
                            </>
                        ) : (
                            <>
                                {customer.firstName} {customer.lastName}
                                <button onClick={() => deleteCustomer(customer.id)}>Delete</button>
                                <button onClick={() => startEditing(customer)}>Edit</button>
                            </>
                        )}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default CustomerList;
