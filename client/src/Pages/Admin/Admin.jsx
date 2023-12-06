import React, { useEffect, useState } from 'react'
import './Admin.css';
import { NavLink, useNavigate } from 'react-router-dom';
import Modal from '../../Components/Modal/Modal';
import { ThemeContext } from '../../Context/ThemeProvider';
import { useContext } from 'react';

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
    const [showDeleteConfirmation, setShowDeleteConfirmation] = useState(false);
    const [userIdToDelete, setUserIdToDelete] = useState(null);
    const navigate = useNavigate();
    const { darkMode } = useContext(ThemeContext);

const fetchUsers = () => {
    setIsLoading(true);
    fetch('https://localhost:7276/users', {credentials: 'include'})
    .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setUsers(data);
        
        setIsLoading(false);
      })
      .catch((err) => console.error("Error fetching users", err));
  };

  useEffect(() => {
    fetchUsers();
  }, []);


if(isLoading){
    return <div>Loading...</div>
}
const handleDeleteClick = (userId) => {
    setShowDeleteConfirmation(true);
    setUserIdToDelete(userId);
  };

  const closeDeleteConfirmation = () => {
    setShowDeleteConfirmation(false);
  };
  const handleDeleteUser = async (userId) => {
    try{
      const userToDelete = users.find(user => user.user.id === userIdToDelete)
      const response = await fetch(`https://localhost:7276/deleteuser/${userIdToDelete}`, {
        method: 'DELETE',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(userToDelete),
      });
      if (response.ok) {
        const responseData = await response.json();
        fetchUsers();
        setShowDeleteConfirmation(false);
        console.log('User deleted from the collection:', responseData);
        navigate('/admin');
      } else {
        console.error('Error deleting user from collection:', response.statusText);
      }
    } catch (err) {
      console.error('Error deleting user from collection', err);
    }
    closeDeleteConfirmation();
  }
  return (
    <div className={`admin-table-container ${darkMode ? 'dark-mode' : ''}`}>
      <h2>Users</h2>
      <table className={`admin-table-users ${darkMode ? 'dark-mode' : ''}`}>
        <thead>
          <tr>
            <th>Email</th>
            <th>Username</th>
            <th>Date of Profile Creation</th>
            <th>All Posts</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody className={`admin-table-data ${darkMode ? 'dark-mode' : ''}`}>
          {users.map((user, index) => (
            <tr key={index}>
              <td>{user.email}</td>
              <td>{user.user.userName}</td>
              <td>{convertDate(user.profileCreationDate)}</td>
              <td>
                <NavLink to={`/admin/posts/${user.user.id}`}>
                  <button className={`admin-table-button ${darkMode ? 'dark-mode' : ''}`}>View</button>
                </NavLink>
              </td>
              <td>
                  <button className={`admin-table-button ${darkMode ? 'dark-mode' : ''}`} onClick={() => handleDeleteClick(user.user.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      {showDeleteConfirmation && (
        <Modal onClose={closeDeleteConfirmation}>
        <h3>Are you sure you want to delete this user?</h3>
        <button className="yesButton" onClick={handleDeleteUser}>Yes</button>
        <button className="noButton" onClick={closeDeleteConfirmation}>No</button>
      </Modal>
      )}
    </div>
  );
  
}
