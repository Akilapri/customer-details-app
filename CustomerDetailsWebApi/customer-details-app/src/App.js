import React, { useState } from 'react';
import CustomerList from './components/CustomerList';
import AddCustomer from './components/AddCustomer';

const App = () => {
    const [updatedCustomers, setUpdatedCustomers] = useState([]);

    const handleAddCustomer = (newCustomer) => {
        setUpdatedCustomers([...updatedCustomers, newCustomer]);
    };

    return (
        <div>
            <h1>Customer Details App</h1>
            <AddCustomer onAddCustomer={handleAddCustomer} />
            <CustomerList customers={updatedCustomers} />
        </div>
    );
};

export default App;
