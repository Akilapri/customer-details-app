import React, { useState } from 'react';
import axios from 'axios';

const AddCustomer = ({ onAddCustomer }) => {
    const [id, setId] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');

    const addCustomer = async () => {
        try {
            console.log('Adding customer:', {id, firstName, lastName });

            const response = await axios.post('https://localhost:5000/api/customerdetails', {id, firstName, lastName });
            const newCustomer = response.data;
            onAddCustomer(newCustomer);
            setId('');
            setFirstName('');
            setLastName('');
        } catch (error) {
            console.error('Error adding customer:', error);
        }
    };

    return (
        <div>
            <h2>Add Customer</h2>
            <label>ID:</label>
            <input type="text" value={id} onChange={(e) => setId(e.target.value)} />
            <br />
            <label>First Name:</label>
            <input type="text" value={firstName} onChange={(e) => setFirstName(e.target.value)} />
            <br />
            <label>Last Name:</label>
            <input type="text" value={lastName} onChange={(e) => setLastName(e.target.value)} />
            <br />
            <button onClick={addCustomer}>Add Customer</button>
        </div>
    );
};

export default AddCustomer;
