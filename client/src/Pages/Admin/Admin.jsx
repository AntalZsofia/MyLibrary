import React, { useEffect, useState } from 'react'
import './Admin.css';
import { NavLink } from 'react-router-dom';

export function convertDate(timestamp) {
  const date = new Date(timestamp);

  const year = date.getFullYear();
  const month = date.toLocaleString('en-US', { month: 'long' });
  const day = date.toLocaleString('en-US', { day: 'numeric' });
  const hours = date.getHours();
  const minutes = date.getMinutes();
  const seconds = date.getSeconds();

  const formattedDate = `${year}. ${month}. ${day}. ${hours}:${minutes}:${seconds}`;

  return formattedDate;
}

export default function Admin() {
    const [isLoading, setIsLoading] = useState(true);
    const [users, setUsers] = useState([]);

useEffect(() => {
    setIsLoading(true);
    fetch('https://localhost:7276/users', {credentials: 'include'})
    .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setUsers(data);
        
        setIsLoading(false);
      })
      .catch((err) => console.error("Error fetching users", err));
  }, [])
if(isLoading){
    return <div>Loading...</div>
}

  return (
    <div className='admin-table-container'>
      <h2>Users</h2>
      <table className='admin-table-users'>
        <thead>
          <tr>
            <th>Email</th>
            <th>Username</th>
            <th>Date of Profile Creation</th>
            <th>All Posts</th>
            <th>All books</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody className='admin-table-data'>
          {users.map((user) => (
            <tr key={user.id}>
              <td>{user.email}</td>
              <td>{user.user.userName}</td>
              <td>{convertDate(user.profileCreationDate)}</td>
              <td>
                <NavLink to={`/admin/posts/${user.user.id}`}>
                  <button className='admin-table-button'>View</button>
                </NavLink>
              </td>
              <td>
                <NavLink to={`/admin/books/${user.user.id}`}>
                  <button className='admin-table-button'>View</button>
                </NavLink>
              </td>
              <td>
                <NavLink to={`/admin/delete/${user.id}`}>
                  <button className='admin-table-button'>Delete</button>
                </NavLink>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
  
}
